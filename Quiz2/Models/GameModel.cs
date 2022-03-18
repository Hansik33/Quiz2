using System.Collections.Generic;
using System.Windows.Media;

namespace Quiz2.Models
{
    internal class GameModel
    {
        public string QuestionsFilePath { get; set; }

        public string FullJSON { get; set; }

        public string CategoryTitle { get; set; }

        public int NumberOfQuestions { get; set; }

        public int ID { get; set; }

        public string Question { get; set; }

        public string AnswerA { get; set; }

        public string AnswerB { get; set; }

        public string AnswerC { get; set; }

        public string AnswerD { get; set; }

        public string CorrectAnswer { get; set; }

        public List<Packages> QuestionsList { get; set; } = new List<Packages>();

        private int _scoreNumber = 0;

        public int ScoreNumber
        {
            get => _scoreNumber;
            set => _scoreNumber = value;
        }

        private int _answeredNumber = 0;

        public int AnsweredNumber
        {
            get => _answeredNumber;
            set => _answeredNumber = value;
        }

        public List<int> UsedIDList { get; set; } = new List<int>();

        private Brush _correctAnswersProgressBarForegroundBrush = Brushes.Black;

        public Brush CorrectAnswersProgressBarForegroundBrush
        {
            get => _correctAnswersProgressBarForegroundBrush;
            set => _correctAnswersProgressBarForegroundBrush = value;
        }

        private Brush _allQuestionsProgressBarForegroundBrush = Brushes.Black;

        public Brush AllQuestionsProgressBarForegroundBrush
        {
            get => _allQuestionsProgressBarForegroundBrush;
            set => _allQuestionsProgressBarForegroundBrush = value;
        }
    }
}