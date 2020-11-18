using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzerObfuscator
{
    interface ITagAnalyzer
    {
        public List<(string, string)> AnalyzeXmls(System.Xml.XmlReader docReader, System.Xml.XmlReader obfReader);
        public List<(string, string)> AnalyzeXml(System.Xml.XmlReader reader, string docName);
    }
}
