using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzerObfuscator
{
    class Noun
    {
        public string text { get; }
        public string particle { get; }
        public List<string> tags { get; }

        public Noun(string particle, string text, List<string> tags)
        {
            this.particle = particle;
            this.text = text;
            this.tags = tags;
        }

        public static List<Noun> GetByTag(Dictionary<string, Noun> nouns, string tag)
        {
            List<Noun> taggedNouns = new List<Noun>();
            foreach (var noun in nouns.Values)
            {
                if (noun.tags.Contains(tag))
                {
                    taggedNouns.Add(noun);
                }
            }
            return taggedNouns;
        }

    }
}
