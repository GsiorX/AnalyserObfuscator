using System.Collections.Generic;
using System.Windows.Documents;

namespace AnalyzerObfuscator
{
    class Analyzer
    {
        TextAnalyzer textAnalyzer = new JaccardAnalyzer();

        BasicAnalizer basicAnalyzer = new BasicAnalizer();

        TextAnalyzer lcsAnalyzer = new LCSAnalyzer();

        ITagAnalyzer tagAnalyzer = new TagAnalyzer();

        MostCommonWords mostCommonWordsAnalyzer = new MostCommonWords();

        CosineAnalyser cosineAnalyser = new CosineAnalyser();

        string _documentName;
        string _obfDocumentName;
        FlowDocument document;
        FlowDocument obfDocument;
        string text;
        string obfText;
        Dictionary<string, int> _parameters;

        public Analyzer(string documentName, string obfDocumentName, Dictionary<string, int> parameters)
        {
            _documentName = documentName;
            _obfDocumentName = obfDocumentName;

            document = Helpers.SetRTF(documentName);
            obfDocument = Helpers.SetRTF(obfDocumentName);

            text = new TextRange(document.ContentStart, document.ContentEnd).Text;
            obfText = new TextRange(obfDocument.ContentStart, obfDocument.ContentEnd).Text;

            _parameters = parameters;
        }

        public Dictionary<string, string> AnalyzeDocuments()
        {
            Dictionary<string, string> results = new Dictionary<string, string>();

            results.Add("Jaccart", textAnalyzer.AnalyzeText(text, obfText));
            results.Add("LCS", lcsAnalyzer.AnalyzeText(text, obfText));
            results.Add("Cosine similarity", cosineAnalyser.AnalyzeText(text, obfText));

            return results;
        }

        public List<Difference> GetDifferences()
        {
            List<Difference> differences = new List<Difference>();

            differences.AddRange(basicAnalyzer.AnalyzeText(text, obfText));
            differences.AddRange(tagAnalyzer.AnalyzeXmls(Helpers.GetReader(_documentName), Helpers.GetReader(_obfDocumentName)));

            return differences;
        }

        public List<MostCommonWordsResult> GetMostCommonWords()
        {
            List<MostCommonWordsResult> mostCommonUsedWordsInFiles = new List<MostCommonWordsResult>();

            mostCommonUsedWordsInFiles.AddRange(mostCommonWordsAnalyzer.AnalyzeText(text, obfText, _parameters["numOfWords"]));

            return mostCommonUsedWordsInFiles;
        }
    }
}
