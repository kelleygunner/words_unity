using System.Numerics;

namespace Com.Game.Level.Contracts
{
    public class StartLevelCommand
    {
        public string Word { get; private set; }
        public BoardType BoardType { get; private set; }
        public Vector2 BoardSize { get; private set; }
    }
}