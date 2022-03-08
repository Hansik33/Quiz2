using System.ComponentModel;
using System.Windows;

namespace Quiz2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TurnOffApplicationClosingWindow(object obj, CancelEventArgs e)
        {
            e.Cancel = true;
            Application.Current.Shutdown();
        }
    }
}