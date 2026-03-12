using Com.Game.Level.Contracts;
using Com.Game.Level.Domain;
using Com.Game.Level.Domain.Definitions;

namespace Com.Game.Level.Application.UseCases
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class StartGeneratedLevelUseCase : IStartLevelUseCase
    {
        private readonly ILevelSessionRepository _levelSessionRepository;
        private readonly ILevelGenerator _levelGenerator;

        internal StartGeneratedLevelUseCase(ILevelSessionRepository levelSessionRepository, 
            ILevelGenerator levelGenerator)
        {
            _levelSessionRepository = levelSessionRepository;
            _levelGenerator = levelGenerator;
        }
        
        StartLevelResult IStartLevelUseCase.Execute(StartLevelCommand command)
        {
            var levelDefinition = GetLevelDefinition(command);
            var levelSession = LevelSession.Start(levelDefinition);
            _levelSessionRepository.Save(levelSession);
            return StartLevelResultFactory.CreateResult(levelSession);
        }

        private LevelDefinition GetLevelDefinition(StartLevelCommand command)
        {
            return _levelGenerator.GenerateLevel(command);
        }
    }
}
