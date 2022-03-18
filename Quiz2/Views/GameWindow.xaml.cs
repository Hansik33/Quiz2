using Quiz2.ViewModels;
using System.Windows;

namespace Quiz2.Views
{
    public partial class GameWindow : Window
    {
        public GameWindow()
        {
            InitializeComponent();
            DataContext = new GameWindowViewModel();
        }

        private void TurnOffApplicationClosingWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            System.Environment.Exit(0);
        }
    }
}