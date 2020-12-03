using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace AnalyzerObfuscator
{
    /// <summary>
    /// Logika interakcji dla klasy ObfuscateTab.xaml
    /// </summary>
    public partial class ObfuscateTab : UserControl
    {
        string documentName;

        public ObfuscateTab()
        {
            InitializeComponent();

            documentName = System.IO.Path.Combine(Environment.CurrentDirectory, @"..\\..\\..\\test documents\test1a.xaml");
            file1.Text = "test1a.xaml";
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

        private void next_Click(object sender, RoutedEventArgs e)
        {
            FlowDocument document = Helpers.SetRTF(documentName);

            ObfuscationResultWindow resultWindow = new ObfuscationResultWindow(document);
            resultWindow.Show();
        }
    }
}
