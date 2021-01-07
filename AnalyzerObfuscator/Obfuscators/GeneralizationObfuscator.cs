using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace AnalyzerObfuscator
{
    class GeneralizationObfuscator : IObfuscator
    {
        private static readonly Random rand = new Random();
        private static readonly double genProp = 0.6;
        public string ObfuscateText(string text)
        {
            string[] sentences = text.Split(".");
            List<string> outputSentences = new List<string>();
            
            for (int i = 0; i < sentences.Length; i++)
            {
                if (rand.NextDouble() >= genProp)
                {
                    outputSentences.Add(sentences[i]);
                    continue;
                }

                string givenSubject = null;
                string sentence = Vocabulary.lower(sentences[i]);
                List<string> splitSentence = new List<string>(sentence.Split(" "));
                splitSentence[0] = Vocabulary.lower(splitSentence[0]);

                List<string> tmpSentences = new List<string>();
                foreach (KeyValuePair<string, (string, string)> subject in Vocabulary.generalizations)
                {
                    if (splitSentence.Contains(subject.Key))
                    {
                        int idxOfSub = splitSentence.IndexOf(subject.Key);
                        if (idxOfSub >= 1 && Vocabulary.IsParticle(splitSentence[idxOfSub - 1]))
                        {
                            splitSentence[idxOfSub - 1] = subject.Value.Item1;
                        } else if (idxOfSub >= 2 && Vocabulary.IsParticle(splitSentence[idxOfSub - 2]))
                        {
                            splitSentence[idxOfSub - 2] = subject.Value.Item1;
                        }
                        splitSentence[idxOfSub] = subject.Value.Item2;

                        givenSubject = subject.Key;

                        Vocabulary.subjects.TryGetValue(subject.Key, out Noun noun);
                        if (noun == null) break;

                        tmpSentences.Add("The " + subject.Value.Item2 + " was " + noun.Particle + " " + subject.Key);
                        break;
                    }
                }
                if (givenSubject == null)
                {
                    foreach (KeyValuePair<string, Noun> subject in Vocabulary.subjects)
                    {
                        if (splitSentence.Contains(subject.Key))
                        {
                            givenSubject = subject.Key;
                            break;
                        }
                    }
                }

                if (givenSubject != null)
                {
                    foreach (KeyValuePair<string, string> adjective in Vocabulary.adjectives)
                    {
                        if (splitSentence.Contains(adjective.Key))
                        {
                            int index = splitSentence.IndexOf(adjective.Key);
                            if (index > 1 && index < splitSentence.Count - 1 && (splitSentence[index - 1].ToLower() == "a" || splitSentence[index - 1].ToLower() == "an") && Vocabulary.subjects.TryGetValue(splitSentence[index + 1], out Noun noun))
                            {
                                splitSentence[index - 1] = noun.Particle;
                            }
                            splitSentence.Remove(adjective.Key);
                            tmpSentences.Add("The " + givenSubject + " was " + adjective.Key);
                        }
                    }
                    sentence = String.Join(" ", splitSentence);
                }

                outputSentences.Add(Vocabulary.capitalize(sentence.Trim()));
                outputSentences.AddRange(tmpSentences.Select(s => Vocabulary.capitalize(s.Trim())));
            }
            return string.Join(".", outputSentences.ToArray());
        }        
    }
}
