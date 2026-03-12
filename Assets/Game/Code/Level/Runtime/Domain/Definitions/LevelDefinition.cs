using System.Collections.Generic;
using Com.Game.Level.Contracts;

namespace Com.Game.Level.Domain.Definitions
{
    internal sealed class LevelDefinition
    {
        public LevelId LevelId { get; private set; }
        public int Version { get; private set; }
        public LetterSetDefinition LetterSet { get; private set; }
        public WordSetDefinition WordSet { get; private set; }
        public BoardDefinition Board { get; private set; }
        public ObjectiveDefinition Objective { get; private set; }

        private LevelDefinition() { }

        public static LevelDefinition CreateFromCommand(StartLevelCommand command, IEnumerable<WordDefinition> words)
        {
            return new LevelDefinition()
            {
                LevelId = LevelId.New(),
                Version = 1,
                LetterSet = new LetterSetDefinition(command.Word),
                WordSet = new WordSetDefinition(words),
                Board = new BoardDefinition(command.BoardType, command.BoardSize),
                Objective = null
            };
        }
    }

    internal class ObjectiveDefinition
    {
        
    }
}