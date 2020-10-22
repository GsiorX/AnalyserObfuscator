using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AnalyzerObfuscator
{
    class JaccardAnalyzer : TextAnalyzer
    {
        public List<(string, double)> AnalyzeText(string text, string obs)
        {
            HashSet<string> textWords = new HashSet<string>(Regex.Split(text, "\\s+"));
            HashSet<string> obsWords = new HashSet<string>(Regex.Split(obs, "\\s+"));

            textWords.IntersectWith(obsWords);
            return new List<(string, double)>() { ("Jaccart", (double)textWords.Count) };
        }
    }
}
