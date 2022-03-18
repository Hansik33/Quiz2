using Quiz2.ViewModels;
using System.IO;
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

        private void AnswerQuestion(object sender, System.Windows.RoutedEventArgs e)
        {
            var givenObject = sender as Button;

            var chosenAnswer = givenObject.Content as string;

            File.WriteAllText("CHOSENANSWER.temp", chosenAnswer);
        }
    }
}