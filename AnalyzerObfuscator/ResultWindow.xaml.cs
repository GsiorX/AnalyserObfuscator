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
        public ResultWindow(string documentName, string obfDocumentName)
        {
            InitializeComponent();

            _documentName = documentName;
            _obfDocumentName = obfDocumentName;

            Analyse();
        }

        protected void Analyse()
        {
            Analyzer analyzer = new Analyzer(_documentName, _obfDocumentName);
            // Analyse documents
            differences.ItemsSource = analyzer.GetDifferences();

            algorithms.ItemsSource = analyzer.AnalyzeDocuments();
        }
    }
}
