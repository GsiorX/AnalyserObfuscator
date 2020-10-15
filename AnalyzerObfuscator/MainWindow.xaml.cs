using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnalyzerObfuscator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string documentName;
        string obfDocumentName;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            Nullable<bool> result = openFileDialog.ShowDialog();

            if(result == true)
            {
                documentName = openFileDialog.FileName;

                //Info dla użytkownika
                file1.Text = documentName.Split("\\").Last();
            }
        }

        private void btnOpenFile2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            Nullable<bool> result = openFileDialog.ShowDialog();

            if (result == true)
            {
                obfDocumentName = openFileDialog.FileName;

                //Info dla użytkownika
                file2.Text = obfDocumentName.Split("\\").Last();
            }
        }

        private static FlowDocument SetRTF(string filePath)
        {
            FileStream xamlFile = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            // StringReader stringReader = new StringReader(xamlString);
            System.Xml.XmlReader xmlReader = System.Xml.XmlReader.Create(xamlFile);
            return XamlReader.Load(xmlReader) as FlowDocument;
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            Analyzer analyzer = new Analyzer();
            FlowDocument document = SetRTF(documentName);
            FlowDocument obfDocument = SetRTF(obfDocumentName);

            (double, double, double) res = analyzer.AnalyzeDocuments(document, obfDocument);
            result.Text = res.Item1.ToString();
        }
    }
}
