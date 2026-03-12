using Com.Game.Level.Contracts;
using Com.Game.Level.Domain.Definitions;

namespace Com.Game.Level.Application
{
    internal interface ILevelGenerator
    {
        LevelDefinition GenerateLevel(StartLevelCommand command);
    }
}