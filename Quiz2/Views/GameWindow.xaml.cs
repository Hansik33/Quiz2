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

        private void ClosingWindow(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            if (Application.Current.MainWindow != null) Application.Current.MainWindow.WindowState = WindowState.Normal;
        }
    }
}