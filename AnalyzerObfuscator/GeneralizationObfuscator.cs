using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalyzerObfuscator
{
    class GeneralizationObfuscator
    {
        string[] adjectives = { "pink" };
        public string ObfuscateText(string text)
        {
            string[] sentences = text.Split(".");
            List<string> outputSentences = new List<string>();
            string givenSubject = null;
            for (int i = 0; i < sentences.Length; i++)
            {
                string sentence = sentences[i];
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

                        Vocabulary.nouns.TryGetValue(subject.Key, out string particle);
                        tmpSentences.Add("The " + subject.Value.Item2 + " is " + particle + " " + subject.Key);
                    }
                }
                if (givenSubject != null)
                {
                    List<string> splitSentence = new List<string>(sentence.Split(" "));
                    foreach (string adjective in adjectives)
                    {
                        if (splitSentence.Contains(adjective))
                        {
                            int index = splitSentence.IndexOf(adjective);
                            if (index > 1 && index < splitSentence.Count - 1 && (splitSentence[index - 1] == "a" || splitSentence[index - 1] == "an") && Vocabulary.nouns.TryGetValue(splitSentence[index + 1], out string particle))
                            {
                                splitSentence[index - 1] = particle;
                            }
                            splitSentence.Remove(adjective);
                            tmpSentences.Add("The " + givenSubject + " is " + adjective);
                        }
                    }
                    sentence = String.Join(" ", splitSentence);
                }

                outputSentences.Add(sentence);
                outputSentences.AddRange(tmpSentences);
            }
            return string.Join(".", outputSentences.ToArray());
        }        
    }
}
