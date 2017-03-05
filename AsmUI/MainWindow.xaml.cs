using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.Win32;
using AsmGrammar;

namespace AsmUI
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
        private string _pathToFile = "";
        private bool _edited;
        private void CommandsRtb_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Focus();
            CommandsRtb.Focus();
            _edited = true;
            Run.IsEnabled = new TextRange(CommandsRtb.Document.ContentStart, CommandsRtb.Document.ContentEnd).Text.Length > 5;
        }
        private void New_OnClick(object sender, RoutedEventArgs e)
        {
            _pathToFile = "";
            CommandsRtb.Document = new FlowDocument();
            _edited = false;
        }
        private void Open_OnClick(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "Assembler files (*.asm)|*.asm",
                InitialDirectory = Environment.SpecialFolder.Desktop.ToString(),
            };
            if (ofd.ShowDialog() == true)
            {
                TextRange tr = new TextRange(CommandsRtb.Document.ContentStart, CommandsRtb.Document.ContentEnd);
                using (FileStream fs = File.Open(ofd.FileName, FileMode.Open))
                {
                    tr.Load(fs, DataFormats.Text);
                }
                _pathToFile = ofd.FileName;
            }
            
        }
        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            if (_pathToFile == "")
            {
                SaveAs_OnClick(sender, null);
            }
            else
            {
                // Если такой файл существует, он перезаписывается, 
                using (FileStream fs = File.Create(_pathToFile))
                {
                    new TextRange(CommandsRtb.Document.ContentStart, CommandsRtb.Document.ContentEnd).
                        Save(fs, DataFormats.Text);
                }
            }
                _edited = false;
        }
        private void SaveAs_OnClick(object sender, RoutedEventArgs e)
        {
            var sfd = new SaveFileDialog
            {
                Filter = "Assembler files (*.asm)|*.asm",
                AddExtension = true,
                InitialDirectory = Environment.SpecialFolder.Desktop.ToString()
            };
            if (sfd.ShowDialog() == true)
            {
                using (FileStream fs = File.Create(sfd.FileName))
                {
                    new TextRange(CommandsRtb.Document.ContentStart, CommandsRtb.Document.ContentEnd).
                        Save(fs, DataFormats.Text);
                }   
            }
            _pathToFile = sfd.FileName;
            _edited = false;
        }
        private void Start_OnClick(object sender, RoutedEventArgs e)
        {
            string answer;
            try
            {
                Asm asm = new Asm();
                RegistersTb.Text += DateTime.Now.ToLongTimeString() + ": ";
                answer = asm.Evaluate(new TextRange(CommandsRtb.Document.ContentStart, CommandsRtb.Document.ContentEnd).Text);
                RegistersTb.Text += "успешный запуск программы\r\n" + answer;
                
            }
            catch (Exception ex)
            {
                RegistersTb.Text += ex.Message;
            }
            RegistersTb.Text += Environment.NewLine + "------------------------" + Environment.NewLine;
        }
        private void Help_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Здесь должна быть справка");
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (_edited)
            {
                var result = MessageBox.Show("Сохранить файл перед закрытием программы?", "Выход из программы",
                    MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                switch (result)
                {
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                    case MessageBoxResult.Yes:
                        Save_OnClick(sender as MenuItem, null);
                        break;
                }
            }
        }

    }
}
