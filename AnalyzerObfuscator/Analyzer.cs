using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;

namespace AnalyzerObfuscator
{
    class Analyzer
    {
        TextAnalyzer textAnalyzer = new JaccardAnalyzer();

        TextAnalyzer basicAnalyzer = new BasicAnalizer();
        public List<(string, double)>  AnalyzeDocuments(FlowDocument document, FlowDocument obfDocument)
        {
            string text = new TextRange(document.ContentStart, document.ContentEnd).Text;
            string obfText = new TextRange(obfDocument.ContentStart, obfDocument.ContentEnd).Text; ;

            List<(string, double)> results = new List<(string, double)>();
            results.AddRange(textAnalyzer.AnalyzeText(text, obfText));
            results.AddRange(basicAnalyzer.AnalyzeText(text, obfText));

            return results;
        }
    }
}
