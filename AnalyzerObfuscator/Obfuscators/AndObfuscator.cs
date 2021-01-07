using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzerObfuscator
{
    class AndObfuscator : IObfuscator
    {
        private static readonly Random rand = new Random();
        private static readonly double andProp = 0.8;
        public string ObfuscateText(string text)
        {
            string[] sentences = text.Split(".");
            List<string> outputSentences = new List<string>();

            // outputSentences.Add(sentences[0]);

            string res = "";
            bool first = true;
            for (int i = 0; i < sentences.Length - 1; i++)
            {
                res += Vocabulary.lower(sentences[i]);

                if (rand.NextDouble() < andProp)
                {
                    res += first ? " and " : ", and ";
                    first = false;
                }
                else
                {
                    res += ". ";
                    first = true;
                }
            }
            return res + Vocabulary.lower(sentences[sentences.Length - 1]) + ".";

            for (int i = 1; i < sentences.Length - 1; i++)
            {
                outputSentences.Add(Vocabulary.lower(sentences[i]));
            }
            string tail = string.Join(" and ", outputSentences);

            if (tail == "") return sentences[0];
            else return sentences[0] + " and " + string.Join(", and ", outputSentences);
        }
    }
}
