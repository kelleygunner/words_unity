using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

// ReSharper disable once CheckNamespace
namespace Com.Ani.AssetManagement.Infrastructure
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class AssetAddressablesLoader : IAssetLoader
    {
        private readonly Dictionary<string, AssetHandle> _assets = new();
        
        public async UniTask PreloadAssetAsync(string path, CancellationToken ct)
        {
            if (_assets.TryGetValue(path, out var existing))
            {
                if (!existing.IsDone)
                    await existing.Task;
                return;
            }

            var handle = new AssetHandle(path);
            _assets[path] = handle;

            var op = Addressables.LoadAssetAsync<Object>(path);
            handle.SetOperation(op);

            try
            {
                await handle.Task.AttachExternalCancellation(ct);
                handle.Complete(op.Result);
            }
            catch
            {
                _assets.Remove(path);
                throw;
            }
        }

        async UniTask<IAsset> IAssetLoader.LoadAssetAsync(string path, CancellationToken ct)
        {
            if (_assets.TryGetValue(path, out var existing))
            {
                existing.Retain();
                if (!existing.IsDone)
                    await existing.Task;

                return existing;
            }

            var handle = new AssetHandle(path);
            _assets[path] = handle;

            var op = Addressables.LoadAssetAsync<Object>(path);
            handle.SetOperation(op);

            try
            {
                var result = await op.ToUniTask(cancellationToken:ct);
                handle.Complete(result);
            }
            catch
            {
                _assets.Remove(path);
                throw;
            }

            return handle;
        }

        void IAssetLoader.UnloadAsset(string path)
        {
            if (!_assets.TryGetValue(path, out var handle))
                return;

            if (!handle.ReleaseRef())
                return;

            Addressables.Release(handle.OperationHandle);
            _assets.Remove(path);
        }

        private sealed class AssetHandle : IAsset
        {
            string IAsset.AssetPath => _path;
            Object IAsset.Object => _object;
            
            private int _refCount = 1;
            private Object _object;
            private readonly string _path;

            internal AsyncOperationHandle<Object> OperationHandle { get; private set; }
            internal UniTask Task { get; private set; }
            internal bool IsDone { get; private set; }

            internal AssetHandle(string path)
            {
                _path = path;
            }

            internal void SetOperation(AsyncOperationHandle<Object> op)
            {
                OperationHandle = op;
                Task = op.ToUniTask();
            }

            internal void Complete(Object obj)
            {
                _object = obj;
                IsDone = true;
            }

            internal void Retain()
            {
                _refCount++;
            }
            
            internal bool ReleaseRef()
            {
                _refCount--;
                return _refCount <= 0;
            }
        }
    }
}
