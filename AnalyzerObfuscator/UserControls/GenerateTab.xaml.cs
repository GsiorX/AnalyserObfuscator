using System.Collections.Generic;
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
            if (Validate())
            {
                Dictionary<string, double> options = new Dictionary<string, double>();
                options.Add("adjProb", double.Parse(adjProb.Text));
                options.Add("secAdjProb", double.Parse(secAdjProb.Text));
                options.Add("continuousProb", double.Parse(continuousProb.Text));
                options.Add("theProb", double.Parse(theProb.Text));
                options.Add("someProb", double.Parse(someProb.Text));
                options.Add("nextSentenceProb", double.Parse(nextSentenceProb.Text));

                TextGenerator textGenerator = new TextGenerator(options);

                string generatedDocument = textGenerator.generateFlowText(int.Parse(length.Text));
                GenerationResultWindow resultWindow = new GenerationResultWindow(generatedDocument);
                resultWindow.Show();
            }
        }

        private bool Validate()
        {
            int Length;
            double AdjProbability, secAdjProbability, continuousProbability, theProbability, someProbability, nextSentenceProbability;

            if (!int.TryParse(length.Text, out Length))
            {
                MessageBox.Show("Length must be a number");
                return false;
            }

            if (Length <= 0)
            {
                MessageBox.Show("Length must be greater than 0");
                return false;
            }

            if (!double.TryParse(adjProb.Text, out AdjProbability))
            {
                MessageBox.Show("Adjective probability must be a number");
                return false;
            }

            if (AdjProbability <= 0 || AdjProbability > 1)
            {
                MessageBox.Show("Adjective probability must be greater than 0 and less or equal 1");
                return false;
            }

            if (!double.TryParse(secAdjProb.Text, out secAdjProbability))
            {
                MessageBox.Show("Second adjective probability must be a number");
                return false;
            }

            if (secAdjProbability <= 0 || secAdjProbability > 1)
            {
                MessageBox.Show("Second adjective probability must be greater than 0 and less or equal 1");
                return false;
            }

            if (!double.TryParse(continuousProb.Text, out continuousProbability))
            {
                MessageBox.Show("Continuous probability must be a number");
                return false;
            }

            if (continuousProbability <= 0 || continuousProbability > 1)
            {
                MessageBox.Show("Continuous probability must be greater than 0 and less or equal 1");
                return false;
            }

            if (!double.TryParse(theProb.Text, out theProbability))
            {
                MessageBox.Show("\"The\" word probability must be a number");
                return false;
            }

            if (theProbability <= 0 || theProbability > 1)
            {
                MessageBox.Show("\"The\" word probability must be greater than 0 and less or equal 1");
                return false;
            }

            if (!double.TryParse(someProb.Text, out someProbability))
            {
                MessageBox.Show("\"Some\" word probability must be a number");
                return false;
            }

            if (someProbability <= 0 || someProbability > 1)
            {
                MessageBox.Show("\"Some\" word probability must be greater than 0 and less or equal 1");
                return false;
            }

            if (!double.TryParse(nextSentenceProb.Text, out nextSentenceProbability))
            {
                MessageBox.Show("Next sentence probability must be a number");
                return false;
            }

            if (nextSentenceProbability <= 0 || nextSentenceProbability > 1)
            {
                MessageBox.Show("Next sentence probability must be greater than 0 and less or equal 1");
                return false;
            }

            return true;
        }
    }
}
