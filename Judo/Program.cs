using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Judo
{
    class Program
    {
        const int ConcatonatedWordsLength = 6;
        static void Main(string[] args)
        {


            string fileName;
            if (args.Length == 0)
            {
                Console.WriteLine("Please Enter a fileName and press Enter, or run this with a file name as a parameter");
                fileName = Console.ReadLine();
            }
            else
            {
                fileName = args[0];
            }


            if (!File.Exists(fileName))
            {
                Console.WriteLine("No Such file: {0}", fileName);
                Console.ReadLine();

                return;
            }


            var stream = File.OpenText(fileName);
            var vals = new WordCounter().GetCountsForFile(stream);

            DisplayWordCounts(vals);

            DisplayContatenatedWords(vals);

            // Keep console open so user can see output
            Console.ReadLine();
        }

        private static void DisplayWordCounts(Dictionary<string, int> vals)
        {
            Console.WriteLine("Word\t\t\tValue");

            foreach (var val in vals.Keys.OrderBy(x => x))
            {
                Console.WriteLine("{0}\t\t\t{1}", val, vals[val]);
            }
        }
        private static void DisplayContatenatedWords(Dictionary<string, int> vals)
        {
            Console.WriteLine();
            var words = new WordCounter().GetConcatenatedWords(vals.Keys, ConcatonatedWordsLength);

            if (words.Count == 0)
            {
                Console.WriteLine("No {0} letter words made up of two other words in the list:", ConcatonatedWordsLength);

            }
            else
            {

                Console.WriteLine("The following are {0} letter words made up of two other words in the list:", ConcatonatedWordsLength);

                foreach (var val in words)
                {
                    Console.WriteLine(val);
                }
            }
        }
    }
}
