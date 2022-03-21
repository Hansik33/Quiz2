using Quiz2.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace Quiz2.Views
{
    public partial class InfoWindow : Window
    {
        public InfoWindow()
        {
            InitializeComponent();
            DataContext = new InfoWindowViewModel();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.DragMove();
        }

        private void ClosingWindow(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            if (Application.Current.MainWindow != null) Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void TurnOffApplication(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}