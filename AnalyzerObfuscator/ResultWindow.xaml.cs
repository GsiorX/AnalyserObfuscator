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
            List<(string, double)> res = analyzer.AnalyzeDocuments(_document, _obfDocument);
            int i = 1;
            foreach ((string, double) row in res)
            {
                System.Windows.Controls.TextBox txt = new System.Windows.Controls.TextBox();
                txt.Text = row.Item1;
                Grid.SetColumn(txt, 1);
                Grid.SetRow(txt, i);
                resultsGrid.Children.Add(txt);

                System.Windows.Controls.TextBox txt2 = new System.Windows.Controls.TextBox();
                txt2.Text = row.Item2.ToString();
                Grid.SetColumn(txt2, 2);
                Grid.SetRow(txt2, i);
                resultsGrid.Children.Add(txt2);
                i++;
            }
        }
    }
}
