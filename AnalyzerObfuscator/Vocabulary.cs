using System.Collections.Generic;

namespace AnalyzerObfuscator
{
    class Vocabulary
    {

        public static string capitalize(string word)
        {
            if (word.Length < 1) return word;

            return char.ToUpper(word[0]) + word.Substring(1);
        }

        public static string lower(string word)
        {
            if (word.Length < 1) return word;

            return char.ToLower(word[0]) + word.Substring(1);
        }

        public static readonly Dictionary<string, string> subjects = new Dictionary<string, string>() {
            { "animal", "an" },
            { "dog", "a" },
            { "cat", "a" },
            { "fish", "a" },
            { "person", "a" },
            { "man", "a" },
            { "woman", "a" },
            { "boy", "a" },
            { "girl", "a" },
        };

        public static readonly Dictionary<string, Noun> nouns = new Dictionary<string, Noun>() {
            { "apple", new Noun("an", "apple", new List<string>(){ "food" }) },
            { "steak", new Noun("a", "steak", new List<string>(){ "food" }) },
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
            { "pink", "a" },
        };

        public static readonly string[] verbsOld = {
            "eat",
            "walk",
            "runn",
            "talk",
        };

        public static readonly List<Verb> verbs = new List<Verb>() {
            new Verb("eat", new List<string>() { "food" }).SetPassive(),
            new Verb("walk", new List<string>() { "place" }),
            new Verb("run", new List<string>() { "place" }),
            new Verb("talk", new List<string>() { "person" }),
        };

        public static readonly Dictionary<string, string> verbTags = new Dictionary<string, string>() {
            { "eat", "food" },
            { "walk", "place" },
            { "runn", "place" },
        };

        public static readonly string[] verbsPassive = {
            "eat",
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
