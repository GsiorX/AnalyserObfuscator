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

        public static bool IsParticle(string word)
        {
            return word.ToLower().Equals("a") || word.ToLower().Equals("an") || word.ToLower().Equals("the");
        }

        public static readonly Dictionary<string, Noun> subjects = new Dictionary<string, Noun>() {
            { "animal", new Noun("an", "animal", new List<string>(){ "animal"}, "s") },
            { "dog", new Noun("a", "dog", new List<string>(){ "animal" },"s")},
            { "cat", new Noun("a", "cat", new List<string>(){ "animal" }, "s" )},
            { "fish", new Noun("a", "fish", new List<string>(){ "animal" }, "es" )},
            { "mouse", new Noun("a", "mouse", new List<string>(){ "animal"}, "s" ) },
            { "lion", new Noun("a", "lion", new List<string>(){ "animal" }, "s") },
            { "person", new Noun("a", "person", new List<string>(){ "person" }, "s") },
            { "man", new Noun("a", "man", new List<string>(){ "person" } , "s")},
            { "woman", new Noun("a", "woman", new List<string>(){ "person" }, "s") },
            { "boy", new Noun("a", "boy", new List<string>(){ "person" }, "s") },
            { "girl", new Noun("a", "girl", new List<string>(){ "person" }, "s") },
            { "child", new Noun("a", "child", new List<string>(){ "person" }, "s") },
            { "father", new Noun("a", "father", new List<string>(){ "person" }, "s") },
            { "mother", new Noun("a", "mother", new List<string>(){ "person" }, "s") },
            { "hat", new Noun("a", "hat", new List<string>(){ "object" }, "s") },
            { "book", new Noun("a", "book", new List<string>(){ "object" }, "s") },
            { "student", new Noun("a", "student", new List<string>(){ "person" }, "s") },
            { "surgeon", new Noun("a", "surgeon", new List<string>(){ "person" }, "s") },
            { "monster", new Noun("a", "monster", new List<string>(){ "person" }, "s") },
            { "rat", new Noun("a", "rat", new List<string>(){ "animal" }, "s") },
            { "wolf", new Noun("a", "wolf", new List<string>(){ "animal" }, "s") },
            { "country", new Noun("a", "country", new List<string>(){ "place" }, "es") },
            { "village", new Noun("a", "village", new List<string>(){ "place" }, "es") },
            { "city", new Noun("a", "city", new List<string>(){ "city" }, "s") }
        };

        public static readonly Dictionary<string, Noun> nouns = new Dictionary<string, Noun>() {
            { "apple", new Noun("an", "apple", new List<string>(){ "food" }, "s") },
            { "steak", new Noun("a", "steak", new List<string>(){ "food" }, "s") },
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
            { "red", "a" },
            { "precise", "a" },
            { "caring", "a" },
            { "evil", "an" },
        };

        public static readonly List<Verb> verbs = new List<Verb>() {
            new Verb("eat", new List<string>() { "food" }).SetPassive(),
            new Verb("walk", new List<string>() { "place" }),
            new Verb("run", new List<string>() { "place" }).SetContinuous("running").SetPast("runned"),
            new Verb("talk", new List<string>() { "person" }),
        };

        public static readonly Dictionary<string, string> verbTags = new Dictionary<string, string>() {
            { "eat", "food" },
            { "walk", "place" },
            { "run", "place" },
        };

        public static readonly string[] verbsPassive = {
            "eat",
        };

        public static readonly IDictionary<string, (string, string)> generalizations = new Dictionary<string, (string, string)>() {
            { "dog", ("an", "animal") },
            { "cat", ("an", "animal") },
            { "fish", ("an", "animal") },
            { "mouse", ("an", "animal") },
            { "lion", ("an", "animal") },
            { "man", ("a", "person") },
            { "woman", ("a", "person") },
            { "boy", ("a", "person") },
            { "girl", ("a", "person") },
            { "hat", ("an", "object") },
            { "surgeon", ("a", "person") },
            { "mother", ("a", "person") },
            { "father", ("a", "person") },
            { "child", ("a", "person") },
            { "student", ("a", "person") },
            { "book", ("an", "object") },
            { "business", ("a", "place") },
            { "country", ("a", "place") },
            { "monster", ("an", "creature") },
            { "village", ("a", "place") },
            { "city", ("a", "place") }
        };

        public static readonly Dictionary<string, (string, string)> synonyms = new Dictionary<string, (string, string)>() {
            { "dog", ("a", "pup") },
            { "cat", ("a", "kitty") },
            { "man", ("a", "guy") },
            { "boy", ("a", "lad") },
            { "girl", ("a", "young lady") },
            { "hat", ("a", "titfer") },
            { "surgeon", ("a", "doctor") },
            { "mother", ("a", "mom") },
            { "father", ("a", "dad") },
            { "child", ("a", "kid") },
            { "student", ("a", "graduate") },
            { "book", ("a", "notebook") },
            { "business", ("a", "biz") },
            { "work", ("a", "job") },
            { "task", ("an", "assignement") }
        };

    }
}
