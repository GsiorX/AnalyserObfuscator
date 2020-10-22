using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalyzerObfuscator
{
    class BasicAnalizer : TextAnalyzer
    {
        public List<(string, double)> AnalyzeSingleText(string text, string name)
        {
            double wordCount = 0, sentenceCount = 0, lettersCount = 0;
            const char dot = '.';
            int index = 0;

            // skip whitespace until first word
            while (index < text.Length && char.IsWhiteSpace(text[index]))
                index++;

            while (index < text.Length)
            {
                // check if current char is part of a word
                while (index < text.Length && !char.IsWhiteSpace(text[index]) && text[index] != dot)
                {
                    index++;
                    lettersCount++;
                }

                wordCount++;

                if (text[index] == dot)
                    sentenceCount++;

                // skip whitespace until next word
                while (index < text.Length && (char.IsWhiteSpace(text[index]) || text[index] == dot))
                    index++;
            }

            return new List<(string, double)>() { ("Liczba liter w tekście " + name, lettersCount), ("Liczba słów w tekście " + name, wordCount), ("Liczba zdań w tekście " + name, sentenceCount) };
        }
        public List<(string, double)> AnalyzeText(string text, string obfuscated)
        {
            List<(string, double)> ares = AnalyzeSingleText(text, "a");
            List<(string, double)> bres = AnalyzeSingleText(obfuscated, "b");

            ares.AddRange(bres);

            return ares;
        }
    }
}
