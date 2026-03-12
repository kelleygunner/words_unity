using System.Numerics;
using Com.Game.Level.Contracts;

namespace Com.Game.Level.Domain.Definitions
{
    internal class BoardDefinition
    {
        private readonly BoardType _boardType;
        private readonly Vector2 _size;

        public BoardDefinition(BoardType boardType, Vector2 size)
        {
            _boardType = boardType;
            _size = size;
        }
    }
}