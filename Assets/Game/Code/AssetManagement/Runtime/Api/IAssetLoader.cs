using System.Threading;
using Cysharp.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Com.Ani.AssetManagement
{
    public interface IAssetLoader
    {
        UniTask<IAsset> LoadAssetAsync(string path, CancellationToken ct);
        void UnloadAsset(string path);
        UniTask PreloadAssetAsync(string path, CancellationToken ct);
    }
}
