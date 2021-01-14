using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
            if (Validate())
            {
                FlowDocument document = Helpers.SetRTF(documentName);

                Dictionary<string, double> options = new Dictionary<string, double>();

                if (addObf.IsChecked == true) options.Add("and", Helpers.ParseDouble(andProb.Text));
                if (passiveObf.IsChecked == true) options.Add("pas", 0);
                if (synonymObf.IsChecked == true) options.Add("syn", Helpers.ParseDouble(synProb.Text));
                if (generalizationObf.IsChecked == true) options.Add("gen", Helpers.ParseDouble(genProb.Text));

                ObfuscationResultWindow resultWindow = new ObfuscationResultWindow(document, options);
                resultWindow.Show();
            }
        }

        private void addObf_Checked(object sender, RoutedEventArgs e)
        {
            if (addObf.IsChecked == true && andProb != null)
            {
                andProb.IsEnabled = true;
            }
            else if (addObf.IsChecked == false && andProb.IsInitialized == true)
            {
                andProb.IsEnabled = false;
            }
        }

        private void generalizationObf_Checked(object sender, RoutedEventArgs e)
        {
            if (generalizationObf.IsChecked == true && genProb != null)
            {
                genProb.IsEnabled = true;
            }
            else if (generalizationObf.IsChecked == false && genProb.IsInitialized == true)
            {
                genProb.IsEnabled = false;
            }
        }

        private void synonymObf_Checked(object sender, RoutedEventArgs e)
        {
            if (synonymObf.IsChecked == true && synProb != null)
            {
                synProb.IsEnabled = true;
            }
            else if (synonymObf.IsChecked == false)
            {
                synProb.IsEnabled = false;
            }
        }
        private bool Validate()
        {
            double andProbability, synProbability, genProbability;
            if (addObf.IsChecked == true)
            {
                if (!Helpers.CheckDouble(andProb.Text, out andProbability))
                {
                    MessageBox.Show("And probability must be a number");
                    return false;
                }

                if (andProbability <= 0 || andProbability > 1)
                {
                    MessageBox.Show("And probability probability must be a number between 0 and 1");
                    return false;
                }
            }

            if (generalizationObf.IsChecked == true)
            {
                if (!Helpers.CheckDouble(genProb.Text, out genProbability))
                {
                    MessageBox.Show("Generalization probability must be a number");
                    return false;
                }

                if (genProbability <= 0 || genProbability > 1)
                {
                    MessageBox.Show("Generalization probability probability must be a number between 0 and 1");
                    return false;
                }
            }

            if (synonymObf.IsChecked == true)
            {
                if (!Helpers.CheckDouble(synProb.Text, out synProbability))
                {
                    MessageBox.Show("Synonym probability must be a number");
                    return false;
                }

                if (synProbability <= 0 || synProbability > 1)
                {
                    MessageBox.Show("Synonym probability probability must be a number between 0 and 1");
                    return false;
                }
            }

            return true;
        }
    }
}
