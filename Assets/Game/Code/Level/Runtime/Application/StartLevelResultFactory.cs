using Com.Game.Level.Contracts;
using Com.Game.Level.Domain;

namespace Com.Game.Level.Application
{
    internal static class StartLevelResultFactory
    {
        internal static StartLevelResult CreateResult(LevelSession levelSession)
        {
            return new StartLevelResult(levelSession.SessionId);
        }
    }
}