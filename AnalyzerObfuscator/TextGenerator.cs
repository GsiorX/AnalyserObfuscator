using System;
using System.Collections.Generic;
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
        static string generateSentence(Random rand, Noun psub = null)
        {
            int nounId = rand.Next(0, Vocabulary.subjects.Count);
            // int advId = rand.Next(0, Vocabulary.adjectives.Count);
            int verbId = rand.Next(0, Vocabulary.verbs.Count);

            var subParticlePair = Enumerable.ToList(Vocabulary.subjects)[nounId];
            // var advParticlePair = Enumerable.ToList(Vocabulary.adjectives)[advId];
            // var nounParticle = nounParticlePair.Value;
            var nsub = (psub == null) ? subParticlePair.Value : psub;
            var subject = (psub == null) ? subParticlePair.Key : psub.Text;
            var subjectParticle = (psub == null) ? subParticlePair.Value.Particle : psub.Particle;
            // var adv = advParticlePair.Key;
            // var advParticle = advParticlePair.Value;
            var verb = Vocabulary.verbs[verbId];

            if (verb.Tags.Count > 0)
            {
                string tag = verb.Tags[rand.Next(0, verb.Tags.Count)];
                var n = Noun.GetByTag(Vocabulary.nouns, tag);
                if (n.Count > 0)
                {
                    int advId = rand.Next(0, Vocabulary.adjectives.Count);
                    var advParticlePair = Enumerable.ToList(Vocabulary.adjectives)[advId];
                    var adv = advParticlePair.Key;
                    var advParticle = advParticlePair.Value;

                    var cn = n[(rand.Next(0, n.Count))];
                    return String.Format("{0} {1} {2} was {3} {4} {5}.", capitalize(advParticle), adv, subject, verb.Continuous, cn.Particle, cn.Text);
                }
            }

            bool isContinuous = rand.NextDouble() < 0.2;
            int numberOfAdj = psub == null ? (rand.NextDouble() < 0.6 ? (rand.NextDouble() < 0.2 ? 2 : 1) : 0) : 0;
            bool useParticleThe = rand.NextDouble() < 0.3;

            List<string> genWords = new List<string>();

            List<string> adjList = new List<string>();
            string particle = subjectParticle;
            for (int i = 0; i < numberOfAdj; i++)
            {
                int advId = rand.Next(0, Vocabulary.adjectives.Count);
                var advParticlePair = Enumerable.ToList(Vocabulary.adjectives)[advId];

                particle = advParticlePair.Value;

                adjList.Add(advParticlePair.Key);
            }
            adjList.Reverse();

            if (psub != null)
            {
                genWords.AddRange(new List<string>() 
                {
                    "then"
                });
            }

            if (psub == null )
            {
                if (rand.NextDouble() < 0.3)
                {
                    genWords.Add("some");
                }
                else
                {
                    genWords.Add(particle);
                }
            }
            else if (useParticleThe)
            {
                genWords.Add("the");
            }
            else
            {
                genWords.Add("that");
            }
            
            genWords.AddRange(adjList);
            genWords.Add(subject);
            if (isContinuous)
            {
                genWords.Add("was");
                genWords.Add(verb.Continuous);
            } 
            else
            {
                genWords.Add(verb.Past);
            }

            genWords[0] = capitalize(genWords[0]);

            string nextSentence = rand.NextDouble() < 0.8 ? " " + generateSentence(rand, nsub) : "";
            return string.Join(" ", genWords) + "." + nextSentence;
            //if (isContinuous)
            //    return String.Format("{0} {1} {2} was {3}.", capitalize(particle), adjList, subject, verb.Continuous);
            //else
            //    return String.Format("{0} {1}{2} {3}.", capitalize(particle), adjList, subject, verb.Past);
            
        }

        public static string generateText(int length)
        {
            if (length < 1)
            {
                return String.Empty;
            }

            var rand = new Random();

            string res = generateSentence(rand);
            int sentencesCount = res.Split(".").Length - 1;

            for (int i = 0; sentencesCount < length; i++)
            {
                string nextSentences = generateSentence(rand);
                res += " " + nextSentences;
                sentencesCount += nextSentences.Split(".").Length - 1;
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
