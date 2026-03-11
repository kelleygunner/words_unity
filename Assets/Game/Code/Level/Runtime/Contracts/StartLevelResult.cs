using System;

namespace Com.Game.Level.Contracts
{
    public readonly struct StartLevelResult
    {
        public readonly Guid SessionId;

        public StartLevelResult(Guid sessionId)
        {
            SessionId = sessionId;
        }
    }
}