using System.Collections.Generic;
using Com.Game.Level.Domain;
using Com.Game.Level.Infrastructure.Utils;

namespace Com.Game.Level.Infrastructure
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class WordDictionary : IWordDictionary
    {
        private List<string> _dictionary;
        
        public void Load(string path)
        {
            _dictionary = DictionaryUtils.LoadDictionary(path);
        }

        public IEnumerator<string> GetWordsByLetterSet(string letterSet)
        {
            return null;
        }
    }
}