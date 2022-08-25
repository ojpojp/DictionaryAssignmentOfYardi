using DictionaryAssignmentOfYardi.Biz;

namespace DictionaryAssignmentOfYardiTest
{
    [TestClass]
    public class WordBreakerTest
    {
        [DataTestMethod]
        [DataRow("dictionary.txt", 3)]
        [DataRow("dictionary.txt", 4)]
        [DataRow("dictionary.txt", 5)]
        [DataRow("dictionary.txt", 6)]
        public void TestGetCompositeWords(string path, int length)
        {
            var wordBreaker = new WordBreaker(path, length);
            var compositeWords = wordBreaker.GetCompositeWords();
            foreach (var item in compositeWords)
            {
                Assert.AreEqual(item.FinalWord, $"{item.FirstWord}{item.SecondWord}");
            }
        }
    }
}