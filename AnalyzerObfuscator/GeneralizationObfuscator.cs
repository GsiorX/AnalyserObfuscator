using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalyzerObfuscator
{
    class GeneralizationObfuscator
    {
        public string ObfuscateText(string text)
        {
            string[] sentences = text.Split(".");
            List<string> outputSentences = new List<string>();
            string givenSubject = null;
            for (int i = 0; i < sentences.Length; i++)
            {
                string sentence = Vocabulary.lower(sentences[i]);
                List<string> splitSentence = new List<string>(sentence.Split(" "));
                List<string> tmpSentences = new List<string>();
                foreach (KeyValuePair<string, (string, string)> subject in Vocabulary.generalizations)
                {
                    if (sentence.Contains(subject.Key))
                    {
                        if (sentence.Contains("a " + subject.Key))
                        {
                            sentence = sentence.Replace("a " + subject.Key, subject.Value.Item1 + " " + subject.Value.Item2);
                        }
                        else if (sentence.Contains("an " + subject.Key))
                        {
                            sentence = sentence.Replace("an " + subject.Key, subject.Value.Item1 + " " + subject.Value.Item2);
                        }
                        else
                        {
                            sentence = sentence.Replace(subject.Key, subject.Value.Item2);
                        }
                        givenSubject = subject.Key;

                        Vocabulary.subjects.TryGetValue(subject.Key, out Noun noun);
                        tmpSentences.Add("The " + subject.Value.Item2 + " is " + noun.Particle + " " + subject.Key);
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
                            tmpSentences.Add("The " + givenSubject + " is " + adjective.Key);
                        }
                    }
                    sentence = String.Join(" ", splitSentence);
                }

                outputSentences.Add(Vocabulary.capitalize(sentence));
                outputSentences.AddRange(tmpSentences);
            }
            return string.Join(".", outputSentences.ToArray());
        }        
    }
}
