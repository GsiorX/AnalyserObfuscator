using System;
using AnalyzerObfuscator;

namespace DataGenerator
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"./output.xaml", false))
            {
                file.WriteLine(TextGenerator.generateFlowText(100));
            }
        }
    }
}
