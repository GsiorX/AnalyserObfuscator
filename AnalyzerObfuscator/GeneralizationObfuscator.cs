using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalyzerObfuscator
{
    class GeneralizationObfuscator
    {
        (string, string, string, string)[] subjects = { ("dog", "animal", "a", "an") };
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
                foreach ((string, string, string, string) subject in subjects)
                {
                    if (sentence.Contains(subject.Item1))
                    {
                        givenSubject = subject.Item1;
                        sentence = sentence.Replace(subject.Item1, subject.Item2);
                        tmpSentences.Add("The " + subject.Item2 + " is " + subject.Item4 + " " + subject.Item1);
                    }
                }
                if (givenSubject != null)
                {
                    foreach (string adjective in adjectives)
                    {
                        if (sentence.Contains(adjective))
                        {
                            sentence = sentence.Replace(adjective, "");
                            tmpSentences.Add("The " + givenSubject + " is " + adjective);
                        }
                    }
                }

                outputSentences.Add(sentence);
                outputSentences.AddRange(tmpSentences);
            }
            return string.Join(".", outputSentences.ToArray());
        }        
    }
}
