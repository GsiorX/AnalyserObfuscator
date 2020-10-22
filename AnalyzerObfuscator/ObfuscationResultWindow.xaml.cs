using System;
using System.Collections.Generic;
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
    /// Interaction logic for ObfuscationResultWindow.xaml
    /// </summary>
    public partial class ObfuscationResultWindow : Window
    {
        public ObfuscationResultWindow(FlowDocument document)
        {
            InitializeComponent();
            Analyze(document);
        }

        void Analyze(FlowDocument document)
        {
            string text = new TextRange(document.ContentStart, document.ContentEnd).Text;
            GeneralizationObfuscator genObfs = new GeneralizationObfuscator();
            string genObfsRes = genObfs.ObfuscateText(text);
            textBlock.Text = genObfsRes;
        }
    }
}
