using System.Windows.Input;

namespace AsmUI
{
    public class WindowCommands
    {
        static WindowCommands()
        {
            Run = new RoutedCommand("Run", typeof(MainWindow));
        }
        public static RoutedCommand Run { get; set; }
    }
}