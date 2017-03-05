using System.Windows.Input;

namespace AsmUI
{
    public class WindowCommands
    {
        static WindowCommands()
        {
            Run = new RoutedCommand("Run", typeof(MainWindow));
            ClearConsole = new RoutedCommand("ClearConsole", typeof(MainWindow));
        }
        public static RoutedCommand Run { get; set; }
        public static RoutedCommand ClearConsole { get; set; }
    }
}