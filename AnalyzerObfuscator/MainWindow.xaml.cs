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
using System.Xml.Linq;
using System.Xml.XPath;

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

            documentName = System.IO.Path.Combine(Environment.CurrentDirectory, @"..\\..\\..\\test documents\test1a.xaml");
            file1.Text = "test1a.xaml";
            obfDocumentName = System.IO.Path.Combine(Environment.CurrentDirectory, @"..\\..\\..\\test documents\test1b.xaml");
            file2.Text = "test1b.xaml";
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

        private void next_Click(object sender, RoutedEventArgs e)
        {
            if (analyseBtn.IsChecked == true)
            { 
                ResultWindow resultWindow = new ResultWindow(documentName, obfDocumentName);
                resultWindow.Show();
            }

            if (obfuscateBtn.IsChecked == true)
            {
                FlowDocument document = Helpers.SetRTF(documentName);

                ObfuscationResultWindow resultWindow = new ObfuscationResultWindow(document);
                resultWindow.Show();
            }

        }

        private void obfuscateBtn_Checked(object sender, RoutedEventArgs e)
        {
            btnOpenFile2.IsEnabled = false;
        }

        private void analyseBtn_Checked(object sender, RoutedEventArgs e)
        {
            btnOpenFile2.IsEnabled = true;
        }
    }
}
