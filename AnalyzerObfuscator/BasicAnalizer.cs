using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzerObfuscator
{
    class BasicAnalizer : TextAnalyzer
    {   
        public double[] AnalyzeText(string text, string obfuscated)
        {
            double wordCount = 0, sentenceCount = 0, lettersCount = 0;
            char dot = '.';
            int index = 0;

            // skip whitespace until first word
            while (index < text.Length && char.IsWhiteSpace(text[index]))
                index++;

            while (index < text.Length)
            {
                // check if current char is part of a word
                while (index < text.Length && !char.IsWhiteSpace(text[index]) && !char.IsPunctuation(dot))
                {
                    index++;
                    lettersCount++;
                }

                wordCount++;

                if (char.IsPunctuation(dot))
                    sentenceCount++;

                // skip whitespace until next word
                while (index < text.Length && (char.IsWhiteSpace(text[index]) || char.IsPunctuation(dot)))
                    index++;
            }

            return new double[3] { lettersCount, wordCount, sentenceCount};
        }
    }
}
