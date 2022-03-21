using System;

namespace Quiz2.Models
{
    internal class StatusMessageBoxWindowModel
    {
        public enum ScoreResult
        {
            Perfect,
            Awful,
            Good,
            Bad
        }

        private string _resultTextBlockEarnedScoreText = "Zdobyte punkty: ";

        public string ResultTextBlockEarnedScoreText
        {
            get => _resultTextBlockEarnedScoreText;
            set => _resultTextBlockEarnedScoreText = value;
        }

        private string _resultTextBlockCorrectAnswerText = "Poprawna odpowiedź: ";

        public string ResultTextBlockCorrectAnswerText
        {
            get => _resultTextBlockCorrectAnswerText;
            set => _resultTextBlockCorrectAnswerText = value;
        }

        private string _incorrectStatusMessageTextBlockText = "Skup się.";

        public string IncorrectStatusMessageTextBlockText
        {
            get => _incorrectStatusMessageTextBlockText;
            set => _incorrectStatusMessageTextBlockText = value;
        }

        private string _correctStatusMessageTextBlockText = "Otrzymujesz punkt.";

        public string CorrectStatusMessageTextBlockText
        {
            get => _correctStatusMessageTextBlockText;
            set => _correctStatusMessageTextBlockText = value;
        }

        private string _incorrectStatusTitleTextBlockText = "Źle!";

        public string IncorrectStatusTitleTextBlockText
        {
            get => _incorrectStatusTitleTextBlockText;
            set => _incorrectStatusTitleTextBlockText = value;
        }

        private string _correctStatusTitleTextBlockText = "Dobrze!";

        public string CorrectStatusTitleTextBlockText
        {
            get => _correctStatusTitleTextBlockText;
            set => _correctStatusTitleTextBlockText = value;
        }

        public Uri StatusSvgViewboxSource { get; set; }

        private Uri _correctIconUri = new Uri("/Icons/correct.svg", UriKind.Relative);

        public Uri CorrectIconUri
        {
            get => _correctIconUri;
            set => _correctIconUri = value;
        }

        private Uri _incorrectIconUri = new Uri("/Icons/cancel.svg", UriKind.Relative);

        public Uri IncorrectIconUri
        {
            get => _incorrectIconUri;
            set => _incorrectIconUri = value;
        }

        public string StatusTitleTextBlockText { get; set; }

        public string StatusMessageTextBlockText { get; set; }

        public string ResultTextBlockText { get; set; }

        public System.Windows.Visibility ResultTextBlockTextVisibility { get; set; }
    }
}