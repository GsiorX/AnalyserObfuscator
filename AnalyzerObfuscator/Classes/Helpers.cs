using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Documents;
using System.Windows.Markup;

namespace AnalyzerObfuscator
{
    public static class Helpers
    {
        public static System.Xml.XmlReader GetReader(string filePath)
        {
            FileStream xamlFile = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return System.Xml.XmlReader.Create(xamlFile);
        }

        public static FlowDocument SetRTF(string filePath)
        {
            return XamlReader.Load(GetReader(filePath)) as FlowDocument;
        }
    }
}
