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
        string _documentName;
        string _obfDocumentName;
        Dictionary<string, int> _parameters;
        public ResultWindow(string documentName, string obfDocumentName, Dictionary<string, int> parameters)
        {
            InitializeComponent();

            _documentName = documentName;
            _obfDocumentName = obfDocumentName;
            _parameters = parameters;

            Analyse();
        }

        protected void Analyse()
        {
            Analyzer analyzer = new Analyzer(_documentName, _obfDocumentName, _parameters);
            // Analyse documents
            List<Object> diff = new List<object>();

            diff.AddRange(analyzer.GetDifferences());
            diff.AddRange(analyzer.GetMostCommonWords());

            differences.ItemsSource = diff;

            algorithms.ItemsSource = analyzer.AnalyzeDocuments();
        }
    }
}
