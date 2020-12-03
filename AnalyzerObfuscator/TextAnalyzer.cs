using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzerObfuscator
{
    interface TextAnalyzer
    {
        double AnalyzeText(string text, string obs);
    }
}
