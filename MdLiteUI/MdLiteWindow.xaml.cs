﻿using System.Windows;
using System.Windows.Documents;
using MdLiteGrammar;

namespace MdLiteUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ParseBtn_OnClick(object sender, RoutedEventArgs e)
        {
            MdLite mdLite = new MdLite();
            //ResultsWb.NavigateToString(mdLite.Parse(new TextRange(MarkdownRtb.Document.ContentStart, 
            //    MarkdownRtb.Document.ContentEnd).Text));
            ResultsWb.Text = mdLite.Parse(new TextRange(MarkdownRtb.Document.ContentStart,
                MarkdownRtb.Document.ContentEnd).Text);
        }
    }
}
