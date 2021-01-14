using System;
using System.Collections.Generic;
using System.Globalization;
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

        public static bool CheckDouble(string text, out double value)
        {
            text = text.Replace(',', '.');
            bool res = double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out value);
            return res;
        }

        public static double ParseDouble(string text)
        {
            text = text.Replace(',', '.');
            double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out double value);
            return value;
        }
    }
}
