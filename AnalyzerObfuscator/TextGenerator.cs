using System;
using System.Linq;

namespace AnalyzerObfuscator
{
    public class TextGenerator
    {

        private static string capitalize(string word)
        {
            if (word.Length < 1) return word;

            return char.ToUpper(word[0]) + word.Substring(1);
        }
        static string generateSentence(Random rand)
        {
            int nounId = rand.Next(0, Vocabulary.subjects.Count);
            int advId = rand.Next(0, Vocabulary.adjectives.Count);
            int verbId = rand.Next(0, Vocabulary.verbs.Count);

            var subParticlePair = Enumerable.ToList(Vocabulary.subjects)[nounId];
            var advParticlePair = Enumerable.ToList(Vocabulary.adjectives)[advId];
            // var nounParticle = nounParticlePair.Value;
            var subject = subParticlePair.Key;
            var adv = advParticlePair.Key;
            var advParticle = advParticlePair.Value;
            var verb = Vocabulary.verbs[verbId];

            if (verb.Tags.Count > 0)
            {
                string tag = verb.Tags[rand.Next(0, verb.Tags.Count)];
                var n = Noun.GetByTag(Vocabulary.nouns, tag);
                if (n.Count > 0 )
                {
                    var cn = n[(rand.Next(0, n.Count))];
                    return String.Format("{0} {1} {2} is {3}ing {4} {5}.", capitalize(advParticle), adv, subject, verb.Text, cn.particle, cn.text);
                }
            }

            return String.Format("{0} {1} {2} is {3}ing.", capitalize(advParticle), adv, subject, verb.Text);
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
xmlns:local=""clr-namespace:AnalyzerObfuscator.test_documents""
ColumnWidth=""400"" FontSize=""14"" FontFamily=""Georgia"" ColumnGap=""20"" PagePadding=""20"">

<Paragraph>
" + generateText(length) +
@"
</Paragraph>
</FlowDocument>";

        }
    }
}
