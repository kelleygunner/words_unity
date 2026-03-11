// ReSharper disable once CheckNamespace
namespace Com.Ani.AssetManagement
{
    public interface IAsset
    {
        public string AssetPath { get; }
        UnityEngine.Object Object { get; }
    }
}