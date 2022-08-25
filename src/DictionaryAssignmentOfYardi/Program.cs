// See https://aka.ms/new-console-template for more information

// default parameters
const int DEFAULT_LENGTH = 6;

// command args
string[] _args = Environment.GetCommandLineArgs();
string filePath = _args[1];
string lengthStr = _args[2];
int length = string.IsNullOrWhiteSpace(lengthStr) ? DEFAULT_LENGTH : Convert.ToInt32(lengthStr);

DateTime startTime = DateTime.Now;

var wordBreaker = new DictionaryAssignmentOfYardi.Biz.WordBreaker();
wordBreaker.InitByFile(filePath, length);
DateTime init_endTime = DateTime.Now;

//var compositeWords = wordBreaker.GetCompositeWords(StringComparison.OrdinalIgnoreCase);   // I am not sure if it need ignore case
//var compositeWords = await wordBreaker.GetCompositeWordsAsync();

var compositeWords = wordBreaker.GetCompositeWords();
DateTime break_endTime = DateTime.Now;

compositeWords.OrderBy(t => t.SecondWord).OrderBy(t => t.FirstWord);
DateTime order_endTime = DateTime.Now;


foreach (var item in compositeWords)
{
    Console.WriteLine($"{item.FirstWord} + {item.SecondWord} => {item.FinalWord}");
}
DateTime print_endTime = DateTime.Now;


Console.WriteLine($"List.Count: {compositeWords.Count()}");
Console.WriteLine($"Loading file span: {(init_endTime - startTime).TotalMilliseconds}ms");
Console.WriteLine($"break words span: {(break_endTime - init_endTime).TotalMilliseconds}ms");
Console.WriteLine($"order span: {(order_endTime - break_endTime).TotalMilliseconds}ms");
Console.WriteLine($"print span: {(print_endTime - order_endTime).TotalMilliseconds}ms");
