using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Logika interakcji dla klasy DeobfuscationResultWindow.xaml
    /// </summary>
    public partial class DeobfuscationResultWindow : Window
    {
        private string deobfDoc = "";

        public DeobfuscationResultWindow(FlowDocument document)
        {
            InitializeComponent();
            Analyze(document);
        }

        void Analyze(FlowDocument document)
        {
            before.Document = document;

            string text = new TextRange(document.ContentStart, document.ContentEnd).Text;
            //Tutaj jakaś deobfuskacja
            //zmienna deobfDoc sluzy pozniej do zapisu

            //reader.Document = deobfDocument;
        }
        private void save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.ValidateNames = false;
            saveFileDialog.CheckFileExists = false;
            saveFileDialog.FileName = "deobfuscated file.xaml";
            saveFileDialog.Filter = "Xaml file (*.xaml)|*.xaml";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, deobfDoc);

                    MessageBox.Show("File saved", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Failed to save a file", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
