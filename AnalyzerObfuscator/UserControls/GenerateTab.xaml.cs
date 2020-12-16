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
                string generatedDocument = TextGenerator.generateFlowText(textLength);
                GenerationResultWindow resultWindow = new GenerationResultWindow(generatedDocument);
                resultWindow.Show();
            }
        }
    }
}
