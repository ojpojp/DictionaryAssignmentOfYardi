using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryAssignmentOfYardi.Model
{
    public class CompositeWord
    {
        public CompositeWord(string firstWord, string secondWord, string finalWord)
        {
            FirstWord = firstWord;
            SecondWord = secondWord;
            FinalWord = finalWord;
        }

        public string FirstWord { get; set; }
        public string SecondWord { get; set; }
        public string FinalWord { get; set; }
    }
}
