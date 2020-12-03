﻿using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Markup;

namespace AnalyzerObfuscator
{
    class Analyzer
    {
        TextAnalyzer textAnalyzer = new JaccardAnalyzer();

        BasicAnalizer basicAnalyzer = new BasicAnalizer();

        TextAnalyzer lcsAnalyzer = new LCSAnalyzer();

        ITagAnalyzer tagAnalyzer = new TagAnalyzer();

        CosineAnalyser cosineAnalyser = new CosineAnalyser();

        string documentName;
        string obfDocumentName;
        FlowDocument document;
        FlowDocument obfDocument;
        string text;
        string obfText;

        public Analyzer(string documentName, string obfDocumentName)
        {
            this.documentName = documentName;
            this.obfDocumentName = obfDocumentName;

            document = Helpers.SetRTF(documentName);
            obfDocument = Helpers.SetRTF(obfDocumentName);

            text = new TextRange(document.ContentStart, document.ContentEnd).Text;
            obfText = new TextRange(obfDocument.ContentStart, obfDocument.ContentEnd).Text;
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
            differences.AddRange(tagAnalyzer.AnalyzeXmls(Helpers.GetReader(documentName), Helpers.GetReader(obfDocumentName)));

            return differences;
        }
    }
}