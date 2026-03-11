using System;
using Com.Game.Level.Domain.Definitions;

namespace Com.Game.Level.Domain
{
    internal class LevelSession
    {
        public Guid SessionId { get; private set; }
        private LevelSession()
        {
            SessionId = Guid.NewGuid();
        }
        
        public static LevelSession Start(LevelDefinition levelDefinition)
        {
            return new LevelSession();
        }
    }
}