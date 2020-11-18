using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzerObfuscator
{
    interface TextAnalyzer
    {
        List<(string, string)> AnalyzeText(string text, string obfuscated);
    }
}
