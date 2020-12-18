using System;
using System.Collections.Generic;
using System.Linq;

namespace AnalyzerObfuscator
{
    public class TextGenerator
    {
        private static readonly Random rand = new Random();
        private static bool GetRandomTruth(double prop)
        {
            return rand.Next(1000) < prop * 1000;
        }

        private static string capitalize(string word)
        {
            if (word.Length < 1) return word;

            return char.ToUpper(word[0]) + word.Substring(1);
        }
        static string generateSentence(Noun psub = null)
        {
            const double adjProp = 0.6;
            const double secAdjProp = 0.4;
            const double continuousProp = 0.3;
            const double theProp = 0.3;
            const double someProp = 0.4;
            const double secondSentenceProp = 1.0;
            const double nextSentenceProp = 0.4;


            int nounId = rand.Next(0, Vocabulary.subjects.Count);
            int verbId = rand.Next(0, Vocabulary.verbs.Count);

            var subParticlePair = Enumerable.ToList(Vocabulary.subjects)[nounId];
            var nsub = psub ?? subParticlePair.Value;
            var subject = (psub == null) ? subParticlePair.Key : psub.Text;
            var subjectParticle = (psub == null) ? subParticlePair.Value.Particle : psub.Particle;
            var verb = Vocabulary.verbs[verbId];


            bool isContinuous = rand.NextDouble() < continuousProp;
            int numberOfAdj = psub == null ? (rand.NextDouble() < adjProp ? (rand.NextDouble() < secAdjProp ? 2 : 1) : 0) : 0;
            bool useParticleThe = rand.NextDouble() < theProp;

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
                List<string> thenLikes = new List<string>() { "then", "after that", "later", "suddenly" };
                genWords.Add(thenLikes[rand.Next(thenLikes.Count)]);
            }

            if (psub == null )
            {
                if (rand.NextDouble() < someProp)
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
                List<string> theLikes = new List<string>() { "that", "the same", "same" };
                genWords.Add(theLikes[rand.Next(theLikes.Count)]);
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

            // If can be passive
            if (verb.Tags.Count > 0)
            {
                string tag = verb.Tags[rand.Next(0, verb.Tags.Count)];
                var n = Noun.GetByTag(Vocabulary.nouns, tag);
                if (n.Count > 0)
                {
                    var cn = n[(rand.Next(0, n.Count))];
                    genWords.Add(cn.Particle);
                    genWords.Add(cn.Text);
                }
            }

            genWords[0] = capitalize(genWords[0]);

            string nextSentence = GetRandomTruth(psub == null ? secondSentenceProp : nextSentenceProp) ? " " + generateSentence(nsub) : "";
            return string.Join(" ", genWords) + "." + nextSentence;            
        }

        public static string generateText(int length)
        {
            if (length < 1)
            {
                return String.Empty;
            }

            string res = generateSentence();
            int sentencesCount = res.Split(".").Length - 1;

            for (int i = 0; sentencesCount < length; i++)
            {
                string nextSentences = generateSentence();
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
