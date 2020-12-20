using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AnalyzerObfuscator
{
    class TagAnalyzer : ITagAnalyzer
    {
        public List<Difference> AnalyzeXmls(System.Xml.XmlReader docReader, System.Xml.XmlReader obfReader)
        {
            Dictionary<string, int> documentNodeCount = AnalyzeXml(docReader);
            Dictionary<string, int> obfNodeCount = AnalyzeXml(obfReader);

            List<Difference> differences = new List<Difference>();

            foreach (KeyValuePair<string, int> entry in documentNodeCount)
            {
                if (!differences.Any(d => d.Name == entry.Key))
                {
                    differences.Add(new Difference { Name = entry.Key, DocumentValue = entry.Value, ObfuscatedDocumentValue = 0 });
                }
                else
                {
                    differences[differences.FindIndex(d => d.Name == entry.Key)].DocumentValue = entry.Value;
                }
            }

            foreach (KeyValuePair<string, int> entry in obfNodeCount)
            {
                if (!differences.Any(d => d.Name == entry.Key))
                {
                    differences.Add(new Difference { Name = entry.Key, DocumentValue = 0, ObfuscatedDocumentValue = entry.Value });
                }
                else
                {
                    differences[differences.FindIndex(d => d.Name == entry.Key)].ObfuscatedDocumentValue = entry.Value;
                }
            }

            return differences;
        }

        public Dictionary<string, int> AnalyzeXml(System.Xml.XmlReader reader)
        {
            Dictionary<string, int> nodeCount = new Dictionary<string, int>();

            XDocument readerDoc = XDocument.Load(reader);

            foreach (var tagName in readerDoc.Root.DescendantNodes().OfType<XElement>().Select(x => x.Name))
            {
                string tag = tagName.LocalName + "s";

                if (!nodeCount.ContainsKey(tag))
                {
                    nodeCount.Add(tag, 1);
                }
                else
                {
                    nodeCount[tag]++;
                }
            }

            return nodeCount;
        }

    }
}
