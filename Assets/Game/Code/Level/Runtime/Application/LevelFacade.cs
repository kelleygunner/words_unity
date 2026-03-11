using System;
using Com.Game.Level.Application.UseCases;
using Com.Game.Level.Contracts;
using Zenject;

namespace Com.Game.Level.Application
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class LevelFacade : ILevelFacade
    {
        private readonly DiContainer _diContainer;
        public LevelFacade(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        StartLevelResult ILevelFacade.StartGeneratedLevel(StartLevelCommand command)
        {
            return StartLevel<StartGeneratedLevelUseCase>(command);
        }

        private StartLevelResult StartLevel<T>(StartLevelCommand command) where T : IStartLevelUseCase
        {
            var useCase = _diContainer.Resolve<T>();
            if (useCase == null)
                throw new Exception($"Start Level Use case not found for {typeof(T).Name}");
            var result = useCase.Execute(command);
            return result;
        }
    }
}