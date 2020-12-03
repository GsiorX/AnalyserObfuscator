using System;
using System.Collections.Generic;

namespace AnalyzerObfuscator
{
    class BasicAnalizer
    {
        public Dictionary<string, double> AnalyzeSingleText(string text)
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

            return new Dictionary<string, double>()
            {
                { "lettersCount", lettersCount },
                { "wordCount", wordCount },
                { "sentenceCount", sentenceCount },
                { "wordPerSentResult", Math.Round(wordPerSentResult, 2) },
                { "lettersPerWordResult", Math.Round(lettersPerWordResult, 2) }
            };
        }

        public List<Difference> AnalyzeText(string text, string obfuscated)
        {
            Dictionary<string, double> ares = AnalyzeSingleText(text);
            Dictionary<string, double> bres = AnalyzeSingleText(obfuscated);

            List<Difference> differences = new List<Difference>()
            {
                (new Difference{ Name="Number of letters", DocumentValue=ares["lettersCount"], ObfuscatedDocumentValue=bres["lettersCount"] }),
                (new Difference{ Name="Number of words", DocumentValue=ares["wordCount"], ObfuscatedDocumentValue=bres["wordCount"] }),
                (new Difference{ Name="Number of sentences", DocumentValue=ares["sentenceCount"], ObfuscatedDocumentValue=bres["sentenceCount"] }),
                (new Difference{ Name="Number of words in a sentence", DocumentValue=ares["wordPerSentResult"], ObfuscatedDocumentValue=bres["wordPerSentResult"] }),
                (new Difference{ Name="Number of letters in a word", DocumentValue=ares["lettersPerWordResult"], ObfuscatedDocumentValue=bres["lettersPerWordResult"] }),
            };

            return differences;
        }
    }
}
