using Com.Game.Level.Contracts;

namespace Com.Game.Level.Application
{
    internal interface IStartLevelUseCase
    {
        StartLevelResult Execute(StartLevelCommand command);
    }
}