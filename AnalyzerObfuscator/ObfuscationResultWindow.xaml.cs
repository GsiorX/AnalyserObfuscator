﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Windows.Markup;
using System.IO;
using System.Xml;

namespace AnalyzerObfuscator
{
    /// <summary>
    /// Interaction logic for ObfuscationResultWindow.xaml
    /// </summary>
    public partial class ObfuscationResultWindow : Window
    {
        private string obfDoc = "";
        public ObfuscationResultWindow(FlowDocument document, List<string> obfs)
        {
            InitializeComponent();
            Analyze(document, obfs);
        }

        void Analyze(FlowDocument document, List<string> obfsNames)
        {

            string text = new TextRange(document.ContentStart, document.ContentEnd).Text;
            text = text.Trim().Substring(0, text.Trim().Length - 1);
            List<IObfuscator> obfs = new List<IObfuscator>();
            if (obfsNames.Contains("gen")) obfs.Add(new GeneralizationObfuscator());
            if (obfsNames.Contains("syn")) obfs.Add(new GeneralizationObfuscator());
            if (obfsNames.Contains("pas")) obfs.Add(new GeneralizationObfuscator());
            if (obfsNames.Contains("and")) obfs.Add(new GeneralizationObfuscator());

            //GeneralizationObfuscator genObfs = new GeneralizationObfuscator();
            //SynonymObfuscator synObfs = new SynonymObfuscator();
            //PassiveObfuscator pasObfs = new PassiveObfuscator();
            //string genObfsRes = genObfs.ObfuscateText(text);
            //string pasObfsRes = pasObfs.ObfuscateText(genObfsRes);
            //string synObfsRes = synObfs.ObfuscateText(pasObfsRes);
            //List<IObfuscator> obfs = new List<IObfuscator>()
            //{
            //    //new GeneralizationObfuscator(),
            //    //new SynonymObfuscator(),
            //    //new PassiveObfuscator(),
            //    //new AddObfuscator(),
            //};

            string result = IObfuscator.JoinObf(obfs, text).Replace(".", ". ");
            result = result + ".";
            obfDoc = @"<FlowDocument xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
xmlns:local=""clr-namespace:AnalyzerObfuscator.test_documents""
ColumnWidth=""400"" FontSize=""14"" FontFamily=""Georgia"" ColumnGap=""20"" PagePadding=""20"">

<Paragraph>
" + result +
@"
</Paragraph>
</FlowDocument>";
            FlowDocument content = XamlReader.Parse(obfDoc) as FlowDocument;
            reader.Document = content;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ValidateNames = false;
            openFileDialog.CheckFileExists = false;
            openFileDialog.Filter = "Xaml file (*.xaml)|*.xaml";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    using StreamWriter file = new StreamWriter(openFileDialog.FileName, true);
                    file.WriteLine(obfDoc);
                    MessageBox.Show("File saved", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception _)
                {
                    MessageBox.Show("Failed to save a file", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}