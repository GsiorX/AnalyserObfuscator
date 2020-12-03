using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalyzerObfuscator
{
    class MostCommonWords
    {
        public Dictionary<string, int> Find(string text, int numOfWords)
        {   var delimiterChars = new char[] { ' ', ',', ':', '\t', '\"', '\r', '{', '}', '[', ']', '=', '/', '.' };

            return text
                 .Split(delimiterChars)
                 .Where(x => x.Length > 0)
                 .Select(x => x.ToLower())
                 .GroupBy(x => x)
                 .Select(x => new { Word = x.Key, Count = x.Count() })
                 .OrderByDescending(x => x.Count)
                 .Take(numOfWords)
                 .ToDictionary(x => x.Word, x => x.Count);
        }

        public List<MostCommonWordsResult> AnalyzeText(string text, string obfuscated, int numOfWords = 5)
        {
            Dictionary<string, int> Ares = Find(text, numOfWords);
            Dictionary<string, int> Bres = Find(obfuscated, numOfWords);

            string resultA = "";
            string resultB = "";


            foreach (KeyValuePair<string, int> pair in Ares)
            {
                resultA += pair.Key.ToString() + " : " + pair.Value.ToString() + "\n";
            }

            foreach (KeyValuePair<string, int> pair in Bres)
            {
                resultB += pair.Key.ToString() + " : " + pair.Value.ToString() + "\n";
            }

            List<MostCommonWordsResult> differencesInWords = new List<MostCommonWordsResult>()
            {
                (new MostCommonWordsResult{ Name ="The most frequently used words \n in document [word : count] ", DocumentValue = resultA, ObfuscatedDocumentValue = resultB})
            };

            return differencesInWords;
        }
    }
}
