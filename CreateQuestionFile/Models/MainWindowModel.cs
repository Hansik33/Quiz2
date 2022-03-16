﻿using System.Collections.Generic;

namespace CreateQuestionFile.Models
{
    internal class MainWindowModel
    {
        private string _QuestionsPath = @"questions";

        public string QuestionsPath
        {
            get => _QuestionsPath;
            set => _QuestionsPath = value;
        }

        public bool IsAnswerDCorrectCheckBoxIsChecked { get; set; }

        public bool IsAnswerCCorrectCheckBoxIsChecked { get; set; }

        public bool IsAnswerBCorrectCheckBoxIsChecked { get; set; }

        public bool IsAnswerACorrectCheckBoxIsChecked { get; set; }

        public string AnswerDTextBoxText { get; set; }

        public string AnswerCTextBoxText { get; set; }

        public string AnswerBTextBoxText { get; set; }

        public string AnswerATextBoxText { get; set; }

        public string QuestionTextBoxText { get; set; }

        private bool _CategoryTitleTextBoxIsReadOnly = false;

        public bool CategoryTitleTextBoxIsReadOnly
        {
            get => _CategoryTitleTextBoxIsReadOnly;
            set => _CategoryTitleTextBoxIsReadOnly = value;
        }

        public string CategoryTitleTextBoxText { get; set; }

        public static int _NumberQuestionNumber = 0;

        public int NumberQuestionNumber
        {
            get => _NumberQuestionNumber;
            set => _NumberQuestionNumber = value;
        }

        public string _NumberQuestionText = "Ilość pytań: " + _NumberQuestionNumber.ToString();

        public string NumberQuestionText
        {
            get => _NumberQuestionText;
            set => _NumberQuestionText = value;
        }

        public string Question { get; set; }

        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }

        public string CorrectAnswer { get; set; }

        public string CategoryTitle { get; set; }

        public string FullJSON { get; set; }

        public string FileName { get; set; }

        public List<Packages> PackagesList { get; set; } = new List<Packages>();
    }
}