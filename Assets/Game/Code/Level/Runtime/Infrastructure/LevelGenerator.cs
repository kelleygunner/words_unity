using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Com.Game.Level.Application;
using Com.Game.Level.Contracts;
using Com.Game.Level.Domain;
using Com.Game.Level.Domain.Definitions;

namespace Com.Game.Level.Infrastructure
{
    internal class LevelGenerator : ILevelGenerator
    {
        private readonly IWordDictionary _wordDictionary;

        public LevelGenerator(IWordDictionary wordDictionary)
        {
            _wordDictionary = wordDictionary;
        }
        
        LevelDefinition ILevelGenerator.GenerateLevel(StartLevelCommand command)
        {
            var words = GenerateWords(command.Word, command.BoardSize);
            return LevelDefinition.CreateFromCommand(command, words);
        }

        private IEnumerable<WordDefinition> GenerateWords(string mainWord, Vector2 size)
        {
            var words = new List<WordDefinition>();
            var rawWords = GetAllWords(mainWord).OrderByDescending(t=>t.Length);
            var grid = new Dictionary<Vector2, char>();
            foreach (var w in rawWords)
            {
                if (TryPlaceWord(w, grid, size, out var pos))
                {
                    words.Add(new WordDefinition(w, pos));
                }
            }
            return words;
        }

        private bool TryPlaceWord(string word, Dictionary<Vector2, char> grid, Vector2 size, out Vector2 position)
        {
            position = new Vector2();
            /*
             * Word - word that has to be placed
             * Grid - the table for placing a word
             * Size - size of the Grid
             * Position - position of the first letter
             */
            return false;
        }

        private IEnumerable<string> GetAllWords(string word)
        {
            var allEntriesByWord = new List<string>();
            // Implement finding words in a Dictionary
            return allEntriesByWord;
        }
    }
}