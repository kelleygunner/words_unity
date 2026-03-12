using Com.Game.Level.Application;
using Com.Game.Level.Application.UseCases;
using Com.Game.Level.Domain;
using Com.Game.Level.Infrastructure;
using Zenject;

namespace Com.Game.Level.Composition
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ILevelSessionRepository>().To<LevelSessionRepository>().FromNew().AsSingle();
            Container.Bind<ILevelGenerator>().To<LevelGenerator>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelFacade>().FromNew().AsSingle();
            
            // Use cases
            Container.Bind<StartGeneratedLevelUseCase>().FromNew().AsSingle();
        }
    }
}