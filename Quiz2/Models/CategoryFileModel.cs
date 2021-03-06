using System.Collections.Generic;

namespace Quiz2.Models
{
    internal class CategoryFileModel
    {
        public Informations Informations { get; set; }
        public List<Packages> Packages { get; set; }
    }

    internal class Informations
    {
        public string CategoryTitle { get; set; }
        public int NumberOfQuestions { get; set; }
    }

    internal class Packages
    {
        public int ID { get; set; }
        public string Question { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }
        public string CorrectAnswer { get; set; }
    }
}