using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnalyzerObfuscator
{
    /// <summary>
    /// Logika interakcji dla klasy AnalysisTab.xaml
    /// </summary>
    public partial class AnalysisTab : UserControl
    {
        string documentName;
        string obfDocumentName;

        public AnalysisTab()
        {
            InitializeComponent();

            documentName = System.IO.Path.Combine(Environment.CurrentDirectory, @"..\\..\\..\\test documents\test1a.xaml");
            file1.Text = "test1a.xaml";
            obfDocumentName = System.IO.Path.Combine(Environment.CurrentDirectory, @"..\\..\\..\\test documents\test1b.xaml");
            file2.Text = "test1b.xaml";
        }
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            Nullable<bool> result = openFileDialog.ShowDialog();

            if (result == true)
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

        private void next_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, int> parameters = new Dictionary<string, int>();
            int numOfWordsAsInt;
            try
            {
                numOfWordsAsInt = Convert.ToInt32(numOfWords.Text);
            }
            catch (Exception)
            {
                numOfWordsAsInt = 5;
            }
            parameters.Add("numOfWords", numOfWordsAsInt);

            ResultWindow resultWindow = new ResultWindow(documentName, obfDocumentName, parameters);
            resultWindow.Show();
        }
    }
}
