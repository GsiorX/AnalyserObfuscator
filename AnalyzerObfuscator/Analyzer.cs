using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Markup;

namespace AnalyzerObfuscator
{
    class Analyzer
    {
        TextAnalyzer textAnalyzer = new JaccardAnalyzer();

        TextAnalyzer basicAnalyzer = new BasicAnalizer();

        LCSAnalyzer lcsAnalyzer = new LCSAnalyzer();

        ITagAnalyzer tagAnalyzer = new TagAnalyzer();

        public List<(string, string)> AnalyzeDocuments(string documentName, string obfDocumentName)
        {
            List<(string, string)> results = new List<(string, string)>();

            FlowDocument document = Helpers.SetRTF(documentName);
            FlowDocument obfDocument = Helpers.SetRTF(obfDocumentName);

            string text = new TextRange(document.ContentStart, document.ContentEnd).Text;
            string obfText = new TextRange(obfDocument.ContentStart, obfDocument.ContentEnd).Text;

            results.AddRange(textAnalyzer.AnalyzeText(text, obfText));
            results.AddRange(basicAnalyzer.AnalyzeText(text, obfText));
            results.AddRange(lcsAnalyzer.AnalyzeText(text, obfText));
            results.AddRange(tagAnalyzer.AnalyzeXmls(Helpers.GetReader(documentName), Helpers.GetReader(obfDocumentName)));

            return results;
        }
    }
}
