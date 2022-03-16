using Quiz2.ViewModels;
using System.Windows.Controls;

namespace Quiz2.Views
{
    public partial class GameView : UserControl
    {
        public GameView()
        {
            InitializeComponent();
            DataContext = new GameViewModel();
        }
    }
}