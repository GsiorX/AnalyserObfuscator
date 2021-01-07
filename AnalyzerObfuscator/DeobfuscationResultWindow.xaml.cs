using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Markup;

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
            MostCommonWords mostCommonWords = new MostCommonWords();
            var res = mostCommonWords.Find(text, 3).GetValueOrDefault("and", 0);
            // Jeśli and jest pośród 3 najczęstrzych wyrazów to zamień , and na kropki
            if (res > 0)
            {
                text = string.Join(". ", text.Split(", and ").Select(words => Vocabulary.capitalize(words)));
            }

            // Zapisz deobfuskowany tekst
            deobfDoc = @"<FlowDocument xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
xmlns:local=""clr-namespace:AnalyzerObfuscator.test_documents""
ColumnWidth=""400"" FontSize=""14"" FontFamily=""Georgia"" ColumnGap=""20"" PagePadding=""20"">

<Paragraph>
" + text +
@"
</Paragraph>
</FlowDocument>";
            FlowDocument content = XamlReader.Parse(deobfDoc) as FlowDocument;
            reader.Document = content;
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
