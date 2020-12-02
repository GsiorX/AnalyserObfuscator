using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzerObfuscator
{
    class PassiveObfuscator
    {

        string MakePassive(string sentence)
        {
            int verbIdx = -1;
            Verb verb = null;
            List<string> splitSentence = new List<string>(sentence.Split(" "));
            for (int i = 0; i < splitSentence.Count; i++)
            {
                var verbOpt = Verb.ToVerb(splitSentence[i]);
                if (verbOpt != null)
                {
                    verbIdx = i;
                    verb = verbOpt;
                }
            }

            if (verb == null || !verb.IsPassive) return sentence;

            // simple version
            string t = splitSentence[verbIdx - 3];
            splitSentence[verbIdx - 3] = splitSentence[verbIdx + 1];
            splitSentence[verbIdx + 1] = t;

            t = splitSentence[verbIdx - 2];
            splitSentence[verbIdx - 2] = splitSentence[verbIdx + 2];
            splitSentence[verbIdx + 2] = t;

            splitSentence[verbIdx] = "being " + verb.Text + "en by";

            return String.Join(" ", splitSentence);
        }
        public string ObfuscateText(string text)
        {
            string[] sentences = text.Split(".");
            List<string> outputSentences = new List<string>();
            for (int i = 0; i < sentences.Length; i++)
            {
                outputSentences.Add(Vocabulary.capitalize(MakePassive(sentences[i])));
            }
            return string.Join(".", outputSentences.ToArray());
        }
    }
}
