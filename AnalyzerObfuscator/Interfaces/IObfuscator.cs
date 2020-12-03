using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzerObfuscator
{
    interface IObfuscator
    {
        public static string JoinObf(List<IObfuscator> obfs, string text)
        {
            foreach (IObfuscator obf in obfs)
            {
                text = obf.ObfuscateText(text);
            }

            return text;
        }
        public string ObfuscateText(string text);
    }
}
