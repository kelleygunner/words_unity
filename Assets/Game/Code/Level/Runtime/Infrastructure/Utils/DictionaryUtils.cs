using System.Collections.Generic;
using System.IO;

namespace Com.Game.Level.Infrastructure.Utils
{
    public static class DictionaryUtils
    {
        public static List<string> LoadDictionary(string path)
        {
            var words = new List<string>();

            foreach (var line in File.ReadLines(path))
            {
                var word = line.Trim();

                if (!string.IsNullOrEmpty(word))
                    words.Add(word);
            }

            return words;
        }
    }
}
