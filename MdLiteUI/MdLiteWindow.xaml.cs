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
