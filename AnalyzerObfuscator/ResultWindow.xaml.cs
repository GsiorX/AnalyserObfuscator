using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AnalyzerObfuscator
{
    /// <summary>
    /// Logika interakcji dla klasy ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        FlowDocument _document;
        FlowDocument _obfDocument;
        public ResultWindow(FlowDocument document, FlowDocument obfDocument)
        {
            InitializeComponent();

            _document = document;
            _obfDocument = obfDocument;

            Analyse();
        }

        protected void Analyse()
        {
            Analyzer analyzer = new Analyzer();
            // Analyse documents
            (double, double, double) res = analyzer.AnalyzeDocuments(_document, _obfDocument);

            jaccart.Text = res.Item1.ToString();
        }
    }
}
