using System;
using System.Collections.Generic;
using System.Linq;

namespace AnalyzerObfuscator
{
    class CosineAnalyser
    {
        public List<int> getVector(string text)
        {
            List<int> vector = new List<int>();

            Dictionary<string, int> frequencies = text
                .ToLower()
                .Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .GroupBy(w => w)
                .ToDictionary(g => g.Key, g => g.Count());

            foreach (KeyValuePair<string, int> entry in frequencies)
            {
                vector.Add(entry.Value);
            }

            return vector;
        }

        public List<(string, string)> CalculateCosineSimilarity(string text, string obfText)
        {
            List<(string, string)> cosine = new List<(string, string)>();

            List<int> textVector = getVector(text);
            List<int> obfTextVector = getVector(obfText);

            int N = (textVector.Count < obfTextVector.Count) ? textVector.Count : obfTextVector.Count;

            double dot = 0.0d, mag1 = 0.0d, mag2 = 0.0d;

            for (int i = 0; i < N; i++)
            {
                dot += textVector[i] * obfTextVector[i];
                mag1 += Math.Pow(textVector[i], 2);
                mag2 += Math.Pow(obfTextVector[i], 2);
            }

            cosine.Add(("Cosine similarity", (dot / (Math.Sqrt(mag1) * Math.Sqrt(mag2))).ToString()));

            return cosine;
        }
    }
}
