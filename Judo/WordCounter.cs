using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Judo
{
    public class WordCounter
    {
        public Dictionary<string, int> GetCountsForFile(TextReader reader)
        {
            var vals = new Dictionary<string, int>();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                AddCountsForLine(vals, line);
            }
            return vals;
        }

        public void AddCountsForLine(IDictionary<string, int> values, string line)
        {
            var alphas = "[a-zA-Z0-9]+";
            var regex = new Regex(alphas);

            foreach (Match match in regex.Matches(line))
            {
                IncrementKey(values, match.Value);
            }
        }

        private void IncrementKey(IDictionary<string, int> values, string match)
        {
            if (match == null)
            {
                return;
            }
            var key = match.ToLower().Trim();
            if (!values.ContainsKey(key))
            {
                values.Add(key, 1);
            }
            else
            {
                values[key] += 1;
            }
        }

        public List<string> GetConcatenatedWords(IEnumerable<string> words, int wordLength ) {
            // intially, determine which words have requisite number of words
            var results = new List<string>();
            var possibleValues = words.Where(x => x.Length == wordLength).ToDictionary( x=>  x.ToLower(), x => x);

            var possibleElements = words.Where(x => x.Length < wordLength);

            foreach (var part1 in possibleElements)
            {
                foreach (var part2 in possibleElements)
                {
                    // this is probably not the best search algorithm, as it is O(N^2). 
                    var theWord = part1.ToLower() + part2.ToLower();
                    if (possibleValues.ContainsKey(theWord)) {
                        results.Add(possibleValues[theWord]);
                    }
                }
            }

            return results.OrderBy(x => x).ToList();
        }
    }
}

