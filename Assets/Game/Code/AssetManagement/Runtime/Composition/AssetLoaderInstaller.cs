using UnityEngine;
using Zenject;

// ReSharper disable once CheckNamespace
namespace Com.Ani.AssetManagement.Infrastructure
{
    internal class AssetLoaderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAssetLoader>().To<AssetAddressablesLoader>().FromNew().AsSingle().NonLazy();
        }
    }
}