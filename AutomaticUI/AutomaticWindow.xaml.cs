using System;
using System.Windows;
using AutomaticGrammar;

namespace AutomaticUI
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

        private void VerifyButton_OnClick(object sender, RoutedEventArgs e)
        {
            var Real = new RealNumbers();
            try
            {
                AnswerText.Text = Real.Vetify(InputTextBox.Text) ? "Введенный текст является действительным числом" : 
                    "Введенный текст не является действительным числом или не распознан";
            }
            catch (Exception exception)
            {
                AnswerText.Text = String.Format("Ошибка {0}:\n{1}", exception.GetType(), exception.Message);
            }
        }
    }
}
