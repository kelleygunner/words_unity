using Com.Ani.AssetManagement;
using Com.Game.Application.Scopes;
using Zenject;

namespace Com.Game.Application.Composition
{
    public class ApplicationScopeInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var applicationScope = new ApplicationScope();
            applicationScope.Create(null);
            Container.BindInterfacesAndSelfTo<ApplicationScope>().FromInstance(applicationScope).NonLazy();
        }
    }
}
