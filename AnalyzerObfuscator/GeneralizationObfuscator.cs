using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalyzerObfuscator
{
    class GeneralizationObfuscator
    {
        (string, string)[] subjects = { ("pies", "zwierz") };
        string[] adjectives = { "różowy" };
        public string ObfuscateText(string text)
        {
            string[] sentences = text.Split(".");
            List<string> outputSentences = new List<string>();
            string givenSubject = null;
            for (int i = 0; i < sentences.Length; i++)
            {
                string sentence = sentences[i];
                List<string> tmpSentences = new List<string>();
                foreach ((string, string)subject in subjects)
                {
                    if (sentence.Contains(subject.Item1))
                    {
                        givenSubject = subject.Item1;
                        sentence = sentence.Replace(subject.Item1, subject.Item2);
                        tmpSentences.Add(" " + char.ToUpper(subject.Item2[0]) + subject.Item2.Substring(1) + " to jest " + subject.Item1);
                    }
                }
                if (givenSubject != null)
                {
                    foreach (string adjective in adjectives)
                    {
                        if (sentence.Contains(adjective))
                        {
                            sentence = sentence.Replace(adjective, "");
                            tmpSentences.Add(" " + char.ToUpper(givenSubject[0]) + givenSubject.Substring(1) + " jest " + adjective);
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
