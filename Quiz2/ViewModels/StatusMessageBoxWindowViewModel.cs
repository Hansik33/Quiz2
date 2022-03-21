using Quiz2.Models;
using Quiz2.ViewModels.ViewModelBase;
using System;
using System.IO;
using static Quiz2.Models.StatusMessageBoxWindowModel;

namespace Quiz2.ViewModels
{
    internal class StatusMessageBoxWindowViewModel : BaseViewModel
    {
        public StatusMessageBoxWindowViewModel()
        {
            TrySummarize();
            SetValues();
        }

        private void TrySummarize()
        {
            if (IsFinish())
            {
                var _earnedScore = File.ReadAllText("EARNEDSCORE.temp");
                File.Delete("EARNEDSCORE.temp");

                var _numberQuestions = File.ReadAllText("NUMBEROFQUESTIONS.temp");
                File.Delete("NUMBEROFQUESTIONS.temp");

                int earnedScore = Int32.Parse(_earnedScore);
                int numberQuestions = Int32.Parse(_numberQuestions);

                var resultScore = ((double)earnedScore / (double)numberQuestions);

                if (resultScore >= 0.5) InformAboutScore(ScoreResult.Good, earnedScore, numberQuestions);
                if (resultScore < 0.5) InformAboutScore(ScoreResult.Bad, earnedScore, numberQuestions);
                if (resultScore == 0) InformAboutScore(ScoreResult.Awful, earnedScore, numberQuestions);
                if (resultScore == 1) InformAboutScore(ScoreResult.Perfect, earnedScore, numberQuestions);
            }
        }

        private void InformAboutScore(ScoreResult scoreResult, int score, int numberQuestions)
        {
            if (scoreResult == ScoreResult.Perfect)
            {
                StatusSvgViewboxSource = CorrectIconUri;
                StatusTitleTextBlockText = CorrectStatusTitleTextBlockText;
                StatusMessageTextBlockText = "Znasz to na pamięć :)";
                ResultTextBlockText = ResultTextBlockEarnedScoreText + score + "/" + numberQuestions;
                ResultTextBlockTextVisibility = System.Windows.Visibility.Visible;
            }
            else if (scoreResult == ScoreResult.Awful)
            {
                StatusSvgViewboxSource = IncorrectIconUri;
                StatusTitleTextBlockText = IncorrectStatusTitleTextBlockText;
                StatusMessageTextBlockText = "Wstyd...";
                ResultTextBlockText = ResultTextBlockEarnedScoreText + score + "/" + numberQuestions;
                ResultTextBlockTextVisibility = System.Windows.Visibility.Visible;
            }
            else if (scoreResult == ScoreResult.Good)
            {
                StatusSvgViewboxSource = CorrectIconUri;
                StatusTitleTextBlockText = CorrectStatusTitleTextBlockText;
                StatusMessageTextBlockText = "Robisz postępy.";
                ResultTextBlockText = ResultTextBlockEarnedScoreText + score + "/" + numberQuestions;
                ResultTextBlockTextVisibility = System.Windows.Visibility.Visible;
            }
            else if (scoreResult == ScoreResult.Bad)
            {
                StatusSvgViewboxSource = IncorrectIconUri;
                StatusTitleTextBlockText = IncorrectStatusTitleTextBlockText;
                StatusMessageTextBlockText = "Lepiej poćwicz.";
                ResultTextBlockText = ResultTextBlockEarnedScoreText + score + "/" + numberQuestions;
                ResultTextBlockTextVisibility = System.Windows.Visibility.Visible;
            }
        }

        private void SetValues()
        {
            if (DidAnswerCorrect())
            {
                StatusSvgViewboxSource = CorrectIconUri;

                StatusTitleTextBlockText = CorrectStatusTitleTextBlockText;

                StatusMessageTextBlockText = CorrectStatusMessageTextBlockText;

                ResultTextBlockTextVisibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                if (File.Exists("CORRECTANSWER.temp"))
                {
                    var correctAnswer = File.ReadAllText("CORRECTANSWER.temp");

                    File.Delete("CORRECTANSWER.temp");

                    StatusSvgViewboxSource = IncorrectIconUri;

                    StatusTitleTextBlockText = IncorrectStatusTitleTextBlockText;

                    StatusMessageTextBlockText = IncorrectStatusMessageTextBlockText;

                    ResultTextBlockCorrectAnswerText = ResultTextBlockCorrectAnswerText + correctAnswer.ToString();

                    ResultTextBlockText = ResultTextBlockCorrectAnswerText;

                    ResultTextBlockTextVisibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private bool IsFinish()
        {
            if (File.Exists("EARNEDSCORE.temp")) return true;
            else return false;
        }

        private bool DidAnswerCorrect()
        {
            if (File.Exists("ISANSWERCORRECT.temp"))
            {
                var fileContent = File.ReadAllText("ISANSWERCORRECT.temp");

                if (fileContent == "True")
                {
                    File.Delete("ISANSWERCORRECT.temp");
                    return true;
                }
                else
                {
                    File.Delete("ISANSWERCORRECT.temp");
                    return false;
                }
            }
            else return false;
        }

        public string ResultTextBlockEarnedScoreText
        {
            get => model.ResultTextBlockEarnedScoreText;
            set
            {
                model.ResultTextBlockEarnedScoreText = value;
                OnPropertyChanged(nameof(ResultTextBlockEarnedScoreText));
            }
        }

        public string ResultTextBlockCorrectAnswerText
        {
            get => model.ResultTextBlockCorrectAnswerText;
            set
            {
                model.ResultTextBlockCorrectAnswerText = value;
                OnPropertyChanged(nameof(ResultTextBlockCorrectAnswerText));
            }
        }

        public string IncorrectStatusMessageTextBlockText
        {
            get => model.IncorrectStatusMessageTextBlockText;
            set
            {
                model.IncorrectStatusMessageTextBlockText = value;
                OnPropertyChanged(nameof(IncorrectStatusMessageTextBlockText));
            }
        }

        public string CorrectStatusMessageTextBlockText
        {
            get => model.CorrectStatusMessageTextBlockText;
            set
            {
                model.CorrectStatusMessageTextBlockText = value;
                OnPropertyChanged(nameof(CorrectStatusMessageTextBlockText));
            }
        }

        public string IncorrectStatusTitleTextBlockText
        {
            get => model.IncorrectStatusTitleTextBlockText;
            set
            {
                model.IncorrectStatusTitleTextBlockText = value;
                OnPropertyChanged(nameof(IncorrectStatusTitleTextBlockText));
            }
        }

        public string CorrectStatusTitleTextBlockText
        {
            get => model.CorrectStatusTitleTextBlockText;
            set
            {
                model.CorrectStatusTitleTextBlockText = value;
                OnPropertyChanged(nameof(CorrectStatusTitleTextBlockText));
            }
        }

        public System.Windows.Visibility ResultTextBlockTextVisibility
        {
            get => model.ResultTextBlockTextVisibility;
            set
            {
                model.ResultTextBlockTextVisibility = value;
                OnPropertyChanged(nameof(ResultTextBlockTextVisibility));
            }
        }

        public string ResultTextBlockText
        {
            get => model.ResultTextBlockText;
            set
            {
                model.ResultTextBlockText = value;
                OnPropertyChanged(nameof(ResultTextBlockText));
            }
        }

        public string StatusMessageTextBlockText
        {
            get => model.StatusMessageTextBlockText;
            set
            {
                model.StatusMessageTextBlockText = value;
                OnPropertyChanged(nameof(StatusMessageTextBlockText));
            }
        }

        public string StatusTitleTextBlockText
        {
            get => model.StatusTitleTextBlockText;
            set
            {
                model.StatusTitleTextBlockText = value;
                OnPropertyChanged(nameof(StatusTitleTextBlockText));
            }
        }

        public Uri IncorrectIconUri
        {
            get => model.IncorrectIconUri;
            set
            {
                model.IncorrectIconUri = value;
                OnPropertyChanged(nameof(IncorrectIconUri));
            }
        }

        public Uri CorrectIconUri
        {
            get => model.CorrectIconUri;
            set
            {
                model.CorrectIconUri = value;
                OnPropertyChanged(nameof(CorrectIconUri));
            }
        }

        public Uri StatusSvgViewboxSource
        {
            get => model.StatusSvgViewboxSource;
            set
            {
                model.StatusSvgViewboxSource = value;
                OnPropertyChanged(nameof(StatusSvgViewboxSource));
            }
        }

        private StatusMessageBoxWindowModel model = new StatusMessageBoxWindowModel();
    }
}