using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzerObfuscator
{
    class SynonymObfuscator : IObfuscator
    {
        private static readonly Random rand = new Random();
        private static readonly double synProp = 0.6;
        public string ObfuscateText(string text)
        {
            string[] sentences = text.Split(".");
            for (int i = 0; i < sentences.Length; i++)
            {
                if (rand.NextDouble() >= synProp) continue;

                List<string> splitSentence = new List<string>(sentences[i].Split(" "));
                foreach (KeyValuePair<string, (string, string)> subject in Vocabulary.synonyms)
                {
                    if (splitSentence.Contains(subject.Key))
                    {
                        int index = splitSentence.IndexOf(subject.Key);
                        if (index > 1 && (splitSentence[index - 1].ToLower() == "a" || splitSentence[index - 1].ToLower() == "an"))
                        {
                            splitSentence[index - 1] = subject.Value.Item1;
                        }
                        splitSentence[index] = subject.Value.Item2;
                        sentences[i] = String.Join(" ", splitSentence);
                    }
                }
            }
            return string.Join(".", sentences);
        }
    }
}
