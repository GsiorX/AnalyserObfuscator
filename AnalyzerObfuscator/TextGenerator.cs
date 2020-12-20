using System;
using System.Collections.Generic;
using System.Linq;

namespace AnalyzerObfuscator
{
    public class TextGenerator
    {
        private static readonly Random rand = new Random();
        private static bool GetRandomTruth(double probability)
        {
            return rand.Next(1000) < probability * 1000;
        }

        private static string capitalize(string word)
        {
            if (word.Length < 1) return word;

            return char.ToUpper(word[0]) + word.Substring(1);
        }
        static string generateSentence(Noun previousSubject = null)
        {
            const double adjProb = 0.6;
            const double secAdjProb = 0.4;
            const double continuousProb = 0.3;
            const double theProb = 0.3;
            const double someProb = 0.4;
            const double nextSentenceProb = 0.6;


            int nounId = rand.Next(0, Vocabulary.subjects.Count);
            int verbId = rand.Next(0, Vocabulary.verbs.Count);

            var subParticlePair = Enumerable.ToList(Vocabulary.subjects)[nounId];
            var nextSubject = previousSubject ?? subParticlePair.Value;
            var subject = (previousSubject == null) ? subParticlePair.Key : previousSubject.Text;
            var subjectParticle = (previousSubject == null) ? subParticlePair.Value.Particle : previousSubject.Particle;
            var verb = Vocabulary.verbs[verbId];

            if (verb.Tags.Count > 0)
            {
                string tag = verb.Tags[rand.Next(0, verb.Tags.Count)];
                var nouns = Noun.GetByTag(Vocabulary.nouns, tag);
                if (nouns.Count > 0)
                {
                    int advId = rand.Next(0, Vocabulary.adjectives.Count);
                    var advParticlePair = Enumerable.ToList(Vocabulary.adjectives)[advId];
                    var adv = advParticlePair.Key;
                    var advParticle = advParticlePair.Value;

                    var randomNoun = nouns[(rand.Next(0, nouns.Count))];
                    return String.Format("{0} {1} {2} was {3} {4} {5}.", capitalize(advParticle), adv, subject, verb.Continuous, randomNoun.Particle, randomNoun.Text);
                }
            }

            bool isContinuous = rand.NextDouble() < continuousProb;
            int numberOfAdj = previousSubject == null ? (rand.NextDouble() < adjProb ? (rand.NextDouble() < secAdjProb ? 2 : 1) : 0) : 0;
            bool useParticleThe = rand.NextDouble() < theProb;

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

            if (previousSubject != null)
            {
                List<string> thenLikes = new List<string>() { "then", "after that", "later", "suddenly" };
                genWords.Add(thenLikes[rand.Next(thenLikes.Count)]);
            }

            if (previousSubject == null)
            {
                if (rand.NextDouble() < someProb)
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

            genWords[0] = capitalize(genWords[0]);

            string nextSentence = GetRandomTruth(nextSentenceProb) ? " " + generateSentence(nextSubject) : "";
            return string.Join(" ", genWords) + "." + nextSentence;
        }

        static string generateFirstSentence()
        {
            List<string> introductions = new List<string>();
            introductions.Add("The story happened a long long time ago");
            introductions.Add("The story happened in a land far far away");
            introductions.Add("The story happened a long time ago, in a kingdom far away");
            introductions.Add("The story happened long time ago where upon a hilltop stood a golden castle");
            introductions.Add("Upon a hillside lived three billy goat");
            introductions.Add("The story happened upon a busy roadside edge");
            introductions.Add("Once upon a time, in a land far far away, there was a story");

            int biginningId = rand.Next(0, introductions.Count);
            return introductions[biginningId] + ".";
        }

        public static string generateText(int length)
        {
            if (length < 1)
            {
                return String.Empty;
            }

            const double fairytaleBeginningProb = 0.8;
            string res;

            if (rand.NextDouble() < fairytaleBeginningProb)
            {
                res = generateFirstSentence();
            }
            else
            {
                res = generateSentence();
            }

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