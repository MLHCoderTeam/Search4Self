using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Search4Self.Parser.Parsers
{
    public static class VideoSearchHistoryParser
    {
        private const string LinkRegex = @"<td><a href='https:\/\/www.youtube.com\/results\?q=(.+)'>(\w+\s)+\w+<\/a><\/td>";

        public static async Task<IDictionary<string, int>> ParseFile(Stream fileStream)
        {
            string content;
            using (var reader = new StreamReader(fileStream))
            {
                content = await reader.ReadToEndAsync().ConfigureAwait(false);
            }

            if (string.IsNullOrEmpty(content))
            {
                return null;
            }

            return await ParseContent(content).ConfigureAwait(false);
        }

        private static Task<IDictionary<string, int>> ParseContent(string content)
        {
            var wordMap = new Dictionary<string, int>();

            var regex = new Regex(LinkRegex);
            foreach (Match match in regex.Matches(content))
            {
                if (string.IsNullOrEmpty(match.Value))
                {
                    continue;
                }

                var startIndex = match.Value.IndexOf(">", 5, StringComparison.Ordinal) + 1;
                var value = match.Value.Substring(startIndex, match.Value.Length - startIndex);
                value = value.Replace("</a></td>", string.Empty);
                var words = value.Split(' ');

                foreach (var word in words)
                {
                    if (wordMap.ContainsKey(word))
                    {
                        wordMap[word]++;
                    }
                    else
                    {
                        wordMap.Add(word, 1);
                    }
                }
            }

            var orderedMap = wordMap.OrderByDescending(it => it.Value).ToDictionary(it => it.Key, it => it.Value);
            return Task.FromResult<IDictionary<string, int>>(orderedMap);
        }
    }
}
