using System.Collections.Generic;
using System.Numerics;

namespace Com.Game.Level.Domain.Definitions
{
    internal class WordSetDefinition
    {
        private IEnumerable<WordDefinition> _words;
        public WordSetDefinition(IEnumerable<WordDefinition> words)
        {
            _words = words;
        }
    }

    internal class WordDefinition
    {
        private readonly string _word;
        private readonly Vector2 _position;

        public WordDefinition(string word, Vector2 position)
        {
            _word = word;
            _position = position;
        }
    }
}