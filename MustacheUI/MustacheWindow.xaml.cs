﻿using System;
using System.Windows;
using System.Windows.Documents;
using MustacheGrammar;

namespace MustacheUI
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
            Mustache mustache = new Mustache();
            try
            {
                ResultsTb.Text = mustache.Parse(
                new TextRange(TemplateRtb.Document.ContentStart,
                    TemplateRtb.Document.ContentEnd).Text,
                new TextRange(KeyValueRtb.Document.ContentStart,
                    KeyValueRtb.Document.ContentEnd).Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка распознавания", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
