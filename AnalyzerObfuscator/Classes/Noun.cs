using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzerObfuscator
{
    class Noun
    {
        public string Text { get; }
        public string Particle { get; }
        public List<string> Tags { get; }
        public string Plural { get; private set; }
        public string PluralEnd { get; private set; }

        public Noun(string particle, string text, List<string> tags, string pluralEnd)
        {
            Particle = particle;
            Text = text;
            Tags = tags;
            PluralEnd = pluralEnd;
            if (pluralEnd == "s" || pluralEnd == "es")
            {
                Plural = Text + PluralEnd;
            }
            else { 
                Plural = pluralEnd};
        }

        public static List<Noun> GetByTag(Dictionary<string, Noun> nouns, string tag)
        {
            List<Noun> taggedNouns = new List<Noun>();
            foreach (var noun in nouns.Values)
            {
                if (noun.Tags.Contains(tag))
                {
                    taggedNouns.Add(noun);
                }
            }
            return taggedNouns;
        }



        public static Noun ParseNoun(string word)
        {
            foreach (Noun noun in Vocabulary.subjects.Values)
            {
                if (noun.Text.Equals(word))
                {
                    return noun;
                }
            }

            foreach (Noun noun in Vocabulary.nouns.Values)
            {
                if (noun.Text.Equals(word))
                {
                    return noun;
                }
            }
            return null;
        }

    }
}
