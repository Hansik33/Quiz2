using Newtonsoft.Json;
using Quiz2.Commands;
using Quiz2.Interfaces;
using Quiz2.Models;
using Quiz2.Patterns;
using Quiz2.ViewModels.ViewModelBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;

namespace Quiz2.ViewModels
{
    internal class GameViewModel : BaseViewModel, IPageViewModel
    {
        public GameViewModel()
        {
            AnswerQuestionCommand = new RelayCommand(AnswerQuestion);

            GetChosenCategoryFilePath();
            GetValuesFromJSON();
            DrawAndAssignValues();
        }

        private void AnswerQuestion(object obj)
        {
            CheckAnswerCorrectness();

            ChangeProgressBarTextBlockForegroundColor();

            TryFinish();

            DrawAndAssignValues();
        }

        private bool ShouldFinishQuiz()
        {
            if (QuestionsList.Count == 0) return true;
            else return false;
        }

        private void TryFinish()
        {
            if (ShouldFinishQuiz())
            {
                File.WriteAllText("EARNEDSCORE.temp", ScoreNumber.ToString());

                GoToChangeCategoryView.Execute(null);
            }
        }

        private void ChangeProgressBarTextBlockForegroundColor()
        {
            var resultScore = ((double)ScoreNumber / (double)NumberOfQuestions);
            var resultAnswered = ((double)AnsweredNumber / (double)NumberOfQuestions);

            if (resultScore == 0.5) CorrectAnswersProgressBarForegroundBrush = Brushes.White;
            if (resultAnswered == 0.5) AllQuestionsProgressBarForegroundBrush = Brushes.White;
        }

        private void CheckAnswerCorrectness()
        {
            var chosenAnswer = File.ReadAllText("CHOSENANSWER.temp");

            File.Delete("CHOSENANSWER.temp");

            if (chosenAnswer == CorrectAnswer) ScoreNumber++;

            AnsweredNumber++;
        }

        private bool DidThisQuestionUse(int id)
        {
            if (UsedIDList.Contains(id)) return true;
            else return false;
        }

        private void DrawAndAssignValues()
        {
            if (QuestionsList.Count > 0)
            {
                var randomlySelectedQuestion = random.Next(QuestionsList.Count);

                ID = QuestionsList[randomlySelectedQuestion].ID;

                if (!(DidThisQuestionUse(ID)))
                {
                    Question = QuestionsList[randomlySelectedQuestion].Question;

                    CorrectAnswer = QuestionsList[randomlySelectedQuestion].CorrectAnswer;

                    var answersList = new List<string>();

                    answersList.Add(QuestionsList[randomlySelectedQuestion].AnswerA);
                    answersList.Add(QuestionsList[randomlySelectedQuestion].AnswerB);
                    answersList.Add(QuestionsList[randomlySelectedQuestion].AnswerC);
                    answersList.Add(QuestionsList[randomlySelectedQuestion].AnswerD);

                    answersList.Shuffle();

                    AnswerA = answersList[0];
                    AnswerB = answersList[1];
                    AnswerC = answersList[2];
                    AnswerD = answersList[3];

                    QuestionsList.Remove(QuestionsList[randomlySelectedQuestion]);

                    answersList.Clear();
                }
                else DrawAndAssignValues();
            }
        }

        private void GetChosenCategoryFilePath()
        {
            if (File.Exists(@"CHOSENCATEGORY.temp"))
            {
                var categoryTitle = File.ReadAllText(@"CHOSENCATEGORY.temp");

                CategoryTitle = categoryTitle.ToString();
            }

            var namesFilesList = new List<string>(changeCategoryViewModel.NamesFiles);

            for (int i = 0; i < namesFilesList.Count; i++)
            {
                var allFileText = File.ReadAllText(namesFilesList[i]);
                CategoryFileModel categoryModel = JsonConvert.DeserializeObject<CategoryFileModel>(allFileText);
                var givenCategoryName = categoryModel.Informations.CategoryTitle;
                if (givenCategoryName == CategoryTitle)
                {
                    QuestionsFilePath = namesFilesList[i];

                    File.Delete(@"CHOSENCATEGORY.temp");

                    break;
                }
                else continue;
            }
        }

        private void GetValuesFromJSON()
        {
            if (QuestionsFilePath != null)
            {
                FullJSON = File.ReadAllText(QuestionsFilePath);

                var categoryFileModel = JsonConvert.DeserializeObject<CategoryFileModel>(FullJSON);

                QuestionsList = categoryFileModel.Packages;

                NumberOfQuestions = categoryFileModel.Informations.NumberOfQuestions;
            }
        }

        private Random random = new Random();

        public Brush AllQuestionsProgressBarForegroundBrush
        {
            get => model.AllQuestionsProgressBarForegroundBrush;
            set
            {
                model.AllQuestionsProgressBarForegroundBrush = value;
                OnPropertyChanged(nameof(AllQuestionsProgressBarForegroundBrush));
            }
        }

        public Brush CorrectAnswersProgressBarForegroundBrush
        {
            get => model.CorrectAnswersProgressBarForegroundBrush;
            set
            {
                model.CorrectAnswersProgressBarForegroundBrush = value;
                OnPropertyChanged(nameof(CorrectAnswersProgressBarForegroundBrush));
            }
        }

        public List<int> UsedIDList
        {
            get => model.UsedIDList;
            set
            {
                model.UsedIDList = value;
                OnPropertyChanged(nameof(UsedIDList));
            }
        }

        public int AnsweredNumber
        {
            get => model.AnsweredNumber;
            set
            {
                model.AnsweredNumber = value;
                OnPropertyChanged(nameof(AnsweredNumber));
            }
        }

        public int ScoreNumber
        {
            get => model.ScoreNumber;
            set
            {
                model.ScoreNumber = value;
                OnPropertyChanged(nameof(ScoreNumber));
            }
        }

        public string CorrectAnswer
        {
            get => model.CorrectAnswer;
            set
            {
                model.CorrectAnswer = value;
                OnPropertyChanged(nameof(CorrectAnswer));
            }
        }

        public string AnswerD
        {
            get => model.AnswerD;
            set
            {
                model.AnswerD = value;
                OnPropertyChanged(nameof(AnswerD));
            }
        }

        public string AnswerC
        {
            get => model.AnswerC;
            set
            {
                model.AnswerC = value;
                OnPropertyChanged(nameof(AnswerC));
            }
        }

        public string AnswerB
        {
            get => model.AnswerB;
            set
            {
                model.AnswerB = value;
                OnPropertyChanged(nameof(AnswerB));
            }
        }

        public string AnswerA
        {
            get => model.AnswerA;
            set
            {
                model.AnswerA = value;
                OnPropertyChanged(nameof(AnswerA));
            }
        }

        public string Question
        {
            get => model.Question;
            set
            {
                model.Question = value;
                OnPropertyChanged(nameof(Question));
            }
        }

        public int ID
        {
            get => model.ID;
            set
            {
                model.ID = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        public List<Packages> QuestionsList
        {
            get => model.QuestionsList;
            set
            {
                model.QuestionsList = value;
                OnPropertyChanged(nameof(QuestionsList));
            }
        }

        public int NumberOfQuestions
        {
            get => model.NumberOfQuestions;
            set
            {
                model.NumberOfQuestions = value;
                OnPropertyChanged(nameof(NumberOfQuestions));
            }
        }

        public string FullJSON
        {
            get => model.FullJSON;
            set
            {
                model.FullJSON = value;
                OnPropertyChanged(nameof(FullJSON));
            }
        }

        public string QuestionsFilePath
        {
            get => model.QuestionsFilePath;
            set
            {
                model.QuestionsFilePath = value;
                OnPropertyChanged(nameof(QuestionsFilePath));
            }
        }

        public string CategoryTitle
        {
            get => model.CategoryTitle;
            set
            {
                model.CategoryTitle = value;
                OnPropertyChanged(nameof(CategoryTitle));
            }
        }

        private ChangeCategoryViewModel changeCategoryViewModel = new ChangeCategoryViewModel();
        private GameModel model = new GameModel();

        public ICommand AnswerQuestionCommand { get; set; }

        private ICommand _goToChangeCategoryView;

        public ICommand GoToChangeCategoryView
        {
            get
            {
                return _goToChangeCategoryView ?? (_goToChangeCategoryView = new RelayCommand(x =>
                {
                    Mediator.Notify("GoToChangeCategoryView", "");
                }));
            }
        }
    }

    public static class Shuffling
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}