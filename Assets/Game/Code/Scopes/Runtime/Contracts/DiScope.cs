using System;
using System.Collections.Generic;
using System.Threading;
using Com.Ani.AssetManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Com.Game.Scopes.Contracts
{
    public abstract class DiScope : IInitializable, IDisposable
    {
        private DiScope _parentScope;
        private readonly List<DiScope> _subScopes = new();
        private ScopeState _scopeState = ScopeState.None;
        private CancellationTokenSource _cts;
        private List<GameObject> _gameObjectsInScope = new();
        private List<string> _preloadedAssets = new ();
        
        [Inject] private DiContainer _container;
        [Inject] private IAssetLoader _assetLoader;
        
        protected abstract List<string> AssetsToPreload { get; }

        public void Create(DiScope parentScope)
        {
            _parentScope = parentScope;
            _scopeState = ScopeState.Created;
        }

        protected void DestroyScope()
        {
            _parentScope.DestroySubScope(this);
        }

        public void AddSubScope(DiScope subScope)
        {
            if (subScope == null)
                throw new ArgumentNullException(nameof(subScope));
            if (_scopeState is ScopeState.Disposing or ScopeState.Disposed or ScopeState.Failed)
                throw new Exception("Trying to add sub-scope to disposed scope");
            if (_subScopes.Contains(subScope))
                return;
            
            _subScopes.Add(subScope);
            subScope.Create(this);
            _container.Inject(subScope);
        }

        public void DestroySubScope(DiScope subScope)
        {
            _subScopes.Remove(subScope);
            subScope.Dispose();
        }
        
        public void Initialize()
        {
            if (_scopeState == ScopeState.None)
                throw new Exception("Trying to Initialize Scope that was Not Created");
            if (_scopeState != ScopeState.Created)
                return;
            _scopeState = ScopeState.Initializing;
            _cts = new CancellationTokenSource();
            Preload(_cts.Token).Forget();
        }
        
        private async UniTask Preload(CancellationToken ct)
        {
            if (_scopeState != ScopeState.Initializing)
                throw new Exception("Trying to Preload Scope in wrong State");
            
            var preloadList = new List<UniTask>();
            _preloadedAssets.Clear();
            foreach (var assetName in AssetsToPreload)
            {
                preloadList.Add(PreloadAsset(assetName));
            }
            await UniTask.WhenAll(preloadList);
            if (_scopeState != ScopeState.Initializing)
            {
                Dispose();
                return;
            }
            _scopeState = ScopeState.Initialized;
            OnInitialized();
            
            return;

            async UniTask PreloadAsset(string assetName)
            {
                await _assetLoader.PreloadAssetAsync(assetName, ct);
                _preloadedAssets.Add(assetName);
            }
        }

        public void Dispose()
        {
            if (_scopeState == ScopeState.Disposed)
                return;
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
            foreach (var assetName in _preloadedAssets)
            {
                _assetLoader.UnloadAsset(assetName);
            }

            foreach (var objectToDestroy in _gameObjectsInScope)
            {
                UnityEngine.Object.Destroy(objectToDestroy);
            }
            
            foreach (var subScope in _subScopes)
                subScope.Dispose();
            _subScopes.Clear();
            _scopeState = ScopeState.Disposed;
            OnDisposed();
        }

        protected T InstantiateInScope<T>(GameObject prefab, Transform parent = null) where T : Component
        {
            var instance = _container.InstantiatePrefab(prefab, parent);
            _gameObjectsInScope.Add(instance);
            return instance.GetComponent<T>();
        }

        protected async UniTask<T> InstantiateInScope<T>(string prefabName, CancellationToken ct, Transform parent = null) where T : Component
        {
            var prefab = await _assetLoader.LoadAssetAsync(prefabName, ct);
            return InstantiateInScope<T>((GameObject)prefab.Object, parent);
        }

        protected abstract void OnInitialized();
        protected abstract void OnDisposed();
    }

    internal enum ScopeState
    {
        None = 0,
        Created = 1,
        Initializing = 2,
        Initialized = 3,
        Disposing = 4,
        Disposed = 5,
        Failed = 6
    }
}