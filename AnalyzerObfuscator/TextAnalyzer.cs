using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzerObfuscator
{
    interface TextAnalyzer
    {
        string AnalyzeText(string text, string obs);
    }
}
