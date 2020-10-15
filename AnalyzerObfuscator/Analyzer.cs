using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;

namespace AnalyzerObfuscator
{
    class Analyzer
    {
        TextAnalyzer textAnalyzer = new JaccardAnalyzer();
        public (double, double, double) AnalyzeDocuments(FlowDocument document, FlowDocument obfDocument)
        {
            string text = new TextRange(document.ContentStart, document.ContentEnd).Text;
            string obfText = new TextRange(obfDocument.ContentStart, obfDocument.ContentEnd).Text; ;

            double textRes = textAnalyzer.AnalyzeText(text, obfText);
            return (textRes, 0, 0);
        }
    }
}
