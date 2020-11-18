using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalyzerObfuscator
{
    class BasicAnalizer : TextAnalyzer
    {
        public List<(string, string)> AnalyzeSingleText(string text, string name)
        {
            double wordCount = 0, sentenceCount = 0, lettersCount = 0, wordPerSentResult = 0, lettersPerWordResult = 0; ;
            const char dot = '.', exclamation = '!', question = '?';

            int index = 0, wordPerSentIndex = 0, lettersPerWordIndex = 0; ;
            List<int> wordsPerSentence = new List<int>();
            List<int> lettersPerWord = new List<int>();

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
                    lettersPerWordIndex++;
                }

                wordCount++;
                lettersPerWord.Add(lettersPerWordIndex);
                lettersPerWordIndex = 0;
                wordPerSentIndex++;

                if (text[index] == dot)
                {
                    sentenceCount++;
                    wordsPerSentence.Add(wordPerSentIndex);
                    wordPerSentIndex = 0;
                }

                // skip whitespace until next word
                while (index < text.Length && (char.IsWhiteSpace(text[index]) || text[index] == dot))
                    index++;
            }
            foreach (int count in wordsPerSentence)
            {
                wordPerSentResult += count;
            }

            foreach (int count in lettersPerWord)
            {
                lettersPerWordResult += count;
            }

            wordPerSentResult /= sentenceCount;
            lettersPerWordResult /= wordCount;

            return new List<(string, string)>() {
                ("Number of letters in " + name, lettersCount.ToString()),
                ("Number of words in " + name, wordCount.ToString()),
                ("Number of sentences in " + name, sentenceCount.ToString()),
                ("Average number of words in a sentence in " + name, Math.Round(wordPerSentResult, 2).ToString()),
                ("Average number of letters in a word in " + name, Math.Round(lettersPerWordResult, 2).ToString())};
        }
        public List<(string, string)> AnalyzeText(string text, string obfuscated)
        {
            List<(string, string)> ares = AnalyzeSingleText(text, "a");
            List<(string, string)> bres = AnalyzeSingleText(obfuscated, "b");

            ares.AddRange(bres);

            return ares;
        }
    }
}
