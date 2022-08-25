using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DictionaryAssignmentOfYardi.Model;

namespace DictionaryAssignmentOfYardi.Biz
{
    public class WordBreaker
    {
        /// <summary>
        /// int for word length, list for each word of that length
        /// </summary>
        private Dictionary<int, List<string>> wordsDic;
        private int length;

        public WordBreaker(string path, int maxLength)
        {
            length = maxLength;

            // init wordsDic
            wordsDic = new Dictionary<int, List<string>>();
            for (int i = 1; i <= length; i++)
            {
                wordsDic.Add(i, new List<string>());
            }

            // fill wordsDic
            foreach (var line in File.ReadLines(path))
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string word = line.Trim();
                    if (word.Length <= length)
                    {
                        wordsDic[word.Length].Add(word);
                    }
                }
            }
        }

        public List<CompositeWord> GetCompositeWords(StringComparison stringComparison = StringComparison.Ordinal)
        {
            List<CompositeWord> result = new List<CompositeWord>(wordsDic[length].Count());

            if (wordsDic != null && wordsDic.Any())
            {
                foreach (var finalWord in wordsDic[length])
                {
                    for (int i = 1; i <= length - 1; i++)
                    {
                        foreach (var firstWord in wordsDic[i])
                        {
                            if (finalWord.StartsWith(firstWord, stringComparison))
                            {
                                foreach (var secondWord in wordsDic[length - i])
                                {
                                    if (finalWord.EndsWith(secondWord, stringComparison))
                                    {
                                        result.Add(new CompositeWord(firstWord, secondWord, finalWord));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
