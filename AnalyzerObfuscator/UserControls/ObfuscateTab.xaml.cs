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

            documentName = System.IO.Path.Combine(Environment.CurrentDirectory, @"..\\..\\..\\test documents\testObf.xaml");
            file1.Text = "testObf.xaml";
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

            var options = new System.Collections.Generic.List<string>();
            if (addObf.IsChecked == true) options.Add("and");
            if (passiveObf.IsChecked == true) options.Add("pas");
            if (synonymObf.IsChecked == true) options.Add("syn");
            if (generalizationObf.IsChecked == true) options.Add("gen");

            ObfuscationResultWindow resultWindow = new ObfuscationResultWindow(document, options);
            resultWindow.Show();
        }
    }
}
