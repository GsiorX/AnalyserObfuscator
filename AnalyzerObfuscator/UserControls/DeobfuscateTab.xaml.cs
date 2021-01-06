using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnalyzerObfuscator.UserControls
{
    /// <summary>
    /// Logika interakcji dla klasy DeobfuscateTab.xaml
    /// </summary>
    public partial class DeobfuscateTab : UserControl
    {
        string documentName;

        public DeobfuscateTab()
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

            DeobfuscationResultWindow resultWindow = new DeobfuscationResultWindow(document);
            resultWindow.Show();
        }
    }
}
