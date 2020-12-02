﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzerObfuscator
{
    class Verb
    {
        public string Text { get; }
        public List<string> Tags { get; }

        public string Continuous { get; private set; }
        public bool IsPassive { get; private set; }

        public Verb(string text, List<string> tags)
        {
            Text = text;
            Tags = tags;
            Continuous = text + "ing";
            IsPassive = false;
        }

        public Verb SetContinuous(string continuous)
        {
            Continuous = continuous;
            return this;
        }

        public Verb SetPassive()
        {
            IsPassive = true;
            return this;
        }

        public static Verb ToVerb(string word)
        {
            foreach (Verb verb in Vocabulary.verbs)
            {
                if (verb.Text.Equals(word) || verb.Continuous.Equals(word))
                {
                    return verb;
                }
            }
            return null;
        }
    }

}
