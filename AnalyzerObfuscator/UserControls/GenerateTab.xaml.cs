using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace AnalyzerObfuscator.UserControls
{
    /// <summary>
    /// Logika interakcji dla klasy GenerateTab.xaml
    /// </summary>
    public partial class GenerateTab : UserControl
    {
        public GenerateTab()
        {
            InitializeComponent();
        }

        private void generate_Click(object sender, RoutedEventArgs e)
        {
            int textLength = Convert.ToInt32(length.Text);
            
            //TODO: add more generation options (number of paragraphs, different xml tags etc.)

            if (textLength > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.ValidateNames = false;
                saveFileDialog.CheckFileExists = false;
                saveFileDialog.FileName = "generated file.xaml";
                saveFileDialog.Filter = "Xaml file (*.xaml)|*.xaml";

                if (saveFileDialog.ShowDialog() == true)
                {
                    File.WriteAllText(saveFileDialog.FileName, TextGenerator.generateFlowText(textLength));

                    MessageBox.Show("File saved", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
