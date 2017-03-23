using System;
using System.Windows;
using GrammarOfArithmetic;

namespace GrammarOfArithmeticUI
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

        private void SolveButton_OnClick(object sender, RoutedEventArgs e)
        {
            ExpressionModule expr = new ExpressionModule(InputTextBox.Text);
            try
            {
                AnswerText.Text = "Ответ: " + expr.Parse();
            }
            catch (Exception exception)
            {
                AnswerText.Text = String.Format("Ошибка {0}:\n{1}", exception.GetType(), exception.Message);
            }
        }
    }
}
