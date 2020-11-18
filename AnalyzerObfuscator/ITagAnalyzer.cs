using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzerObfuscator
{
    interface ITagAnalyzer
    {
        public List<(string, double)> AnalyzeXmls(System.Xml.XmlReader docReader, System.Xml.XmlReader obfReader);
        public List<(string, double)> AnalyzeXml(System.Xml.XmlReader reader, string docName);
    }
}
