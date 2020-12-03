using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzerObfuscator
{
    class PassiveObfuscator : IObfuscator
    {

        string MakePassive(string sentence)
        {
            int verbIdx = -1;
            Verb verb = null;
            List<string> splitSentence = new List<string>(sentence.Split(" "));
            for (int i = splitSentence.Count - 1; i >= 0; i--)
            {
                if (splitSentence[i].Length == 0 || splitSentence[i].Equals(" "))
                {
                    splitSentence.RemoveAt(i);
                }
            }
            sentence = string.Join(" ", splitSentence);

            for (int i = 0; i < splitSentence.Count; i++)
            {
                var verbOpt = Verb.ParseVerb(splitSentence[i]);
                if (verbOpt != null)
                {
                    verbIdx = i;
                    verb = verbOpt;
                }
            }

            if (verb == null || !verb.IsPassive) return sentence;

            int n1Idx = -1;
            Noun n1 = null;
            for (int i = verbIdx - 1; i >= 0; i--)
            {
                var nounOpt = Noun.ParseNoun(splitSentence[i]);
                if (nounOpt != null)
                {
                    n1 = nounOpt;
                    n1Idx = i;
                    break;
                }
            }

            int n2Idx = -1;
            Noun n2 = null;
            for (int i = verbIdx + 1; i < splitSentence.Count; i++)
            {
                var nounOpt = Noun.ParseNoun(splitSentence[i]);
                if (nounOpt != null)
                {
                    n2 = nounOpt;
                    n2Idx = i;
                    break;
                }
            }

            if (n1 == null || n2 == null) return sentence;

            try
            {
                splitSentence[n1Idx - 1] = n2.Particle;
                splitSentence[n1Idx] = n2.Text;

                splitSentence[n2Idx - 1] = n1.Particle;
                splitSentence[n2Idx] = n1.Text;
                splitSentence[verbIdx] = "being " + verb.Text + "en by";

                splitSentence[0] = Vocabulary.capitalize(splitSentence[0]);

                return string.Join(" ", splitSentence);
            }
            catch (Exception)
            {
                return sentence;
            }
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
