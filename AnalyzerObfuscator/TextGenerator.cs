﻿using System;
using System.Linq;

namespace AnalyzerObfuscator
{
    class TextGenerator
    {

        private static string capitalize(string word)
        {
            if (word.Length < 1) return word;

            return char.ToUpper(word[0]) + word.Substring(1);
        }
        static string generateSentence(Random rand)
        {
            int nounId = rand.Next(0, Vocabulary.nouns.Count);
            int advId = rand.Next(0, Vocabulary.adjectives.Count);
            int verbId = rand.Next(0, Vocabulary.verbs.Length);

            var nounParticlePair = Enumerable.ToList(Vocabulary.nouns)[nounId];
            var advParticlePair = Enumerable.ToList(Vocabulary.adjectives)[advId];
            // var nounParticle = nounParticlePair.Value;
            var noun = nounParticlePair.Key;
            var adv = advParticlePair.Key;
            var advParticle = advParticlePair.Value;
            var verb = Vocabulary.verbs[verbId];

            return String.Format("{0} {1} {2} is {3}ing.", capitalize(advParticle), adv, noun, verb);
        }

        public static string generateText(int length)
        {
            if (length < 1)
            {
                return String.Empty;
            }

            var rand = new Random();
            string res = generateSentence(rand);
            for (int i = 0; i < length; i++)
            {
                res += " " + generateSentence(rand);
            }
            return res;
        }

        public static string generateFlowText(int length)
        {
            return @"<FlowDocument xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
xmlns: local =""clr-namespace:AnalyzerObfuscator.test_documents""
ColumnWidth =""400"" FontSize=""14"" FontFamily=""Georgia"" ColumnGap=""20"" PagePadding=""20"">

<Paragraph>
" + generateText(length) +
@"
</Paragraph>
</FlowDocument>";

        }

        public static void Main(string[] args)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"./output.xaml", false))
            {
                file.WriteLine(generateFlowText(100));
            }
        }
    }
}