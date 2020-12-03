using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzerObfuscator
{
    interface ITagAnalyzer
    {
        public List<Difference> AnalyzeXmls(System.Xml.XmlReader docReader, System.Xml.XmlReader obfReader);
        public Dictionary<string, int> AnalyzeXml(System.Xml.XmlReader reader);
    }
}
