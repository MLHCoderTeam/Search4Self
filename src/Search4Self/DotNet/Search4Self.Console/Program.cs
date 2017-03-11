using System.IO;
using System.Threading.Tasks;
using Search4Self.Parser;
using Search4Self.Parser.Parsers;

namespace Search4Self.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        private static async Task MainAsync()
        {
            var fileReader = new StreamReader(@"..\..\..\..\Data\my data\Takeout - 1\YouTube\history\search-history.html");
            var parseResult = await VideoSearchHistoryParser.ParseFile(fileReader.BaseStream).ConfigureAwait(false);

            if (parseResult == null)
            {
                return;
            }

            foreach (var item in parseResult)
            {
                System.Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }
    }
}
