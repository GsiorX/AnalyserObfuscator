using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzerObfuscator
{
    class AddObfuscator : IObfuscator
    {
        public string ObfuscateText(string text)
        {
            string[] sentences = text.Split(".");
            List<string> outputSentences = new List<string>();

            outputSentences.Add(sentences[0]);

            for (int i = 1; i < sentences.Length - 1; i++)
            {
                outputSentences.Add(Vocabulary.lower(sentences[i]));
            }
            return string.Join(" and ", outputSentences);
        }
    }
}
