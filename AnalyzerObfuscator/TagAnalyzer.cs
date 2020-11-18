using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AnalyzerObfuscator
{
    class TagAnalyzer : ITagAnalyzer
    {
        public List<(string, double)> AnalyzeXmls(System.Xml.XmlReader docReader, System.Xml.XmlReader obfReader)
        {
            List<(string, double)> documentNodeCount = AnalyzeXml(docReader, "document");
            List<(string, double)> obfNodeCount = AnalyzeXml(obfReader, "obfuscated document");

            documentNodeCount.AddRange(obfNodeCount);

            return documentNodeCount;
        }

        public List<(string, double)> AnalyzeXml(System.Xml.XmlReader reader, string docName)
        {
            Dictionary<string, int> nodeCount = new Dictionary<string, int>();

            XDocument readerDoc = XDocument.Load(reader);

            foreach (var tagName in readerDoc.Root.DescendantNodes().OfType<XElement>().Select(x => x.Name))
            {
                if (!nodeCount.ContainsKey(tagName.LocalName))
                {
                    nodeCount.Add(tagName.LocalName, 1);
                }
                else
                {
                    nodeCount[tagName.LocalName]++;
                }
            }

            List<(string, double)> listNodeCount = new List<(string, double)>();

            foreach (KeyValuePair<string, int> entry in nodeCount)
            {
                listNodeCount.Add(("Number of " + entry.Key + "s in " + docName, (double)entry.Value));
            }

            return listNodeCount;
        }
    }
}
