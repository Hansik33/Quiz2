using Quiz2.ViewModels;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace Quiz2.Views
{
    public partial class StatusMessageBoxWindow : Window
    {
        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        public StatusMessageBoxWindow()
        {
            InitializeComponent();
            DataContext = new StatusMessageBoxWindowViewModel();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.DragMove();
        }

        private void HideWindow(object sender, RoutedEventArgs e)
        {
            SetCursorPos(1024, 768);
            this.Close();
        }
    }
}