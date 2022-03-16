using Quiz2.Commands;
using Quiz2.Models;
using Quiz2.Views;
using System.Windows;
using System.Windows.Input;

namespace Quiz2.ViewModels
{
    internal class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            TurnOffApplicationCommand = new RelayCommand(TurnOffApplication);
            ShowApplicationInfoCommand = new RelayCommand(ShowApplicationInfo);
            ShowGameWindowCommand = new RelayCommand(ShowGameWindow);
        }

        private void TurnOffApplication(object obj)
        {
            Application.Current.Shutdown();
        }

        private void ShowApplicationInfo(object obj)
        {
            infoWindow.Show();
            if (Application.Current.MainWindow != null) Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void ShowGameWindow(object obj)
        {
            gameWindow.Show();
            if (Application.Current.MainWindow != null) Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private InfoWindow infoWindow = new InfoWindow();
        private GameWindow gameWindow = new GameWindow();

        public ICommand TurnOffApplicationCommand { get; set; }
        public ICommand ShowApplicationInfoCommand { get; set; }
        public ICommand ShowGameWindowCommand { get; set; }

        private MainWindowModel model = new MainWindowModel();
    }
}