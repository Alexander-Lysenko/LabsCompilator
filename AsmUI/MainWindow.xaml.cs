using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AsmGrammar;
using Microsoft.Win32;
using System.IO;

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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Сохранить файл перед закрытием программы?", "Выход из программы",
                MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {
                if (result == MessageBoxResult.Yes)
                {
                    Save_OnClick(sender as MenuItem, null);
                }
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Start_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Asm asm = new Asm();
                RegistersTb.Text += DateTime.Now.ToLongTimeString() + ": запуск программы\n";
                RegistersTb.Text += asm.Evaluate(new TextRange(CommandsRtb.Document.ContentStart, CommandsRtb.Document.ContentEnd).Text) + "\n";
            }
            catch (Exception ex)
            {
                RegistersTb.Text += ex.Message;
            }    
        }
        private void Test_OnClick(object sender, RoutedEventArgs e)
        {

        }
        private void New_OnClick(object sender, RoutedEventArgs e)
        {
            _pathToFile = "";
            CommandsRtb.Document = new FlowDocument();
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
                    tr.Load(fs, DataFormats.Rtf);
                }
                _pathToFile = ofd.FileName;
            }
            
        }
        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            if (_pathToFile == "")
                SaveAs_OnClick(sender, null);
            else
            {
                // Если такой файл существует, он перезаписывается, 
                using (FileStream fs = File.Create(_pathToFile))
                {
                    new TextRange(CommandsRtb.Document.ContentStart, CommandsRtb.Document.ContentEnd).
                        Save(fs, DataFormats.Text);
                }
            }

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
        }
        private void Help_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Здесь должна быть справка");
        }
    }
}
