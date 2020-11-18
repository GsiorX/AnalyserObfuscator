using System.Collections.Generic;

namespace AnalyzerObfuscator
{
    class Vocabulary
    {
        public static readonly Dictionary<string, string> nouns = new Dictionary<string, string>() {
            { "dog", "a" },
            { "cat", "a" },
            { "fish", "a" },
            { "man", "a" },
            { "woman", "a" },
            { "boy", "a" },
            { "girl", "a" },
        };

        public static readonly Dictionary<string, string> adjectives = new Dictionary<string, string>() {
            { "tall", "a" },
            { "short", "a" },
            { "fat", "a" },
            { "skinny", "a" },
            { "white", "a" },
            { "black", "a" },
            { "american", "an" },
            { "european", "a" },
            { "asian", "an" },
            { "british", "a" },
        };

        public static readonly string[] verbs = {
            "eat",
            "walk",
            "runn",
            "talk",
        };

        public static readonly IDictionary<string, (string, string)> generalizations = new Dictionary<string, (string, string)>() {
            { "dog", ("an", "animal") },
            { "cat", ("an", "animal") },
            { "fish", ("an", "animal") },
            { "man", ("a", "person") },
            { "woman", ("a", "person") },
            { "boy", ("a", "person") },
            { "girl", ("a", "person") },
        };

        public static readonly Dictionary<string, (string, string)> synonyms = new Dictionary<string, (string, string)>() {
            { "dog", ("a", "pup") },
            { "cat", ("a", "kitty") },
            { "man", ("a", "guy") },
            { "boy", ("a", "lad") },
            { "girl", ("a", "young lady") },
        };
    }
}
