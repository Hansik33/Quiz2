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
    }
}