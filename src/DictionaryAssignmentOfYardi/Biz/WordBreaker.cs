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

        public void InitByFile(string path, int maxLength)
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
                        // slower solution

                        //var firstWord = finalWord.Substring(0, i);
                        //if (wordsDic[i].Exists(t => t.Equals(firstWord, stringComparison)))
                        //{
                        //    foreach (var secondWord in wordsDic[length - i])
                        //    {
                        //        if (finalWord.EndsWith(secondWord, stringComparison))
                        //        {
                        //            result.Add(new CompositeWord(firstWord, secondWord, finalWord));
                        //        }
                        //    }
                        //}
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

        // slower solution

        public async Task<List<CompositeWord>> GetCompositeWordsAsync(StringComparison stringComparison = StringComparison.Ordinal)
        {
            List<CompositeWord> result = new List<CompositeWord>(wordsDic[length].Count());

            if (wordsDic != null && wordsDic.Any())
            {
                foreach (var finalWord in wordsDic[length])
                {
                    int possible = length / 2;
                    for (int i = 1; i <= possible; i++)
                    {
                        var tasks = wordsDic[i].Select(async firstWord =>
                        {
                            if (finalWord.StartsWith(firstWord, stringComparison))
                            {
                                result.AddRange(await GetCompositeWords(firstWord, finalWord, stringComparison));
                            }
                        });

                        await Task.WhenAll(tasks);
                    }
                }
            }

            return result;
        }

        public async Task<List<CompositeWord>> GetCompositeWords(string firstWord, string finalWord, StringComparison stringComparison)
        {
            List<CompositeWord> result = new List<CompositeWord>();
            foreach (var secondWord in wordsDic[length - firstWord.Length])
            {
                if (finalWord.Equals($"{firstWord}{secondWord}", stringComparison))
                {
                    result.Add(new CompositeWord(firstWord, secondWord, finalWord));
                }
            }
            return result;
        }
    }
}
