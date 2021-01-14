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

            BasicAnalizer basicAnalizer = new BasicAnalizer();
            var baRes = basicAnalizer.AnalyzeSingleText(text);
            var sentenceCount = baRes.GetValueOrDefault("sentenceCount", 0);
            var generalNounCount = baRes.GetValueOrDefault("generalNounCount", 0);
            // Jeśli generalizacji jest co najwyżej 10 razy mniej niż zdań to spróbuj je usunąć.
            if (generalNounCount / sentenceCount > 0.1)
            {
                List<string> sentences = new List<string>(text.Split("."));
                List<string> outputSentences = new List<string>();
                for (var i = 0; i < sentences.Count - 1; i++)
                {
                    var sentence = sentences[i].Trim();
                    List<string> splitSentence = new List<string>(sentence.Split(" "));
                    // Jeśli zdanie posiada generalizacje
                    if (splitSentence.Where(e => Vocabulary.generalNouns.Contains(e)).Count() > 0)
                    {
                        var nextSentence = sentences[i + 1].Trim();
                        List<string> splitNextSentence = new List<string>(nextSentence.Split(" "));
                        if (splitNextSentence.Count < 5)
                        {
                            outputSentences.Add(sentence);
                            continue;
                        }

                        bool hasIs = splitNextSentence[2] == "was" || splitNextSentence[2] == "is";
                        bool hasA = splitNextSentence[3] == "a" || splitNextSentence[3] == "an";
                        var unGeneralized = splitSentence.Where(e => Vocabulary.generalNouns.Contains(e)).Where(e => {
                            if (Vocabulary.generalizations.TryGetValue(splitNextSentence[4], out (string, string) value))
                            {
                                return value.Item2 == e;
                            }

                            return false;
                        });

                        if (splitNextSentence.Count == 5 && hasIs && hasA && unGeneralized.Count() > 0)
                        {
                            var newSentence = sentence;
                            foreach (var a in unGeneralized)
                            {
                                newSentence = newSentence.Replace(a, splitNextSentence[4]);
                            }
                            outputSentences.Add(newSentence);
                            i++;
                        }
                        else
                        {
                            outputSentences.Add(sentence);
                        }
                    }
                    else
                    {
                        outputSentences.Add(sentence);
                    }

                }
                var texts = outputSentences.Select(s => s.Trim()).Where(s => s != "").Select(s => Vocabulary.capitalize(s)).ToArray();
                text = string.Join(". ", texts) + ".";
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
