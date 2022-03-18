using CreateQuestionFile.Models;
using CreateQuestionFile.ViewModelBase;
using Newtonsoft.Json;
using Quiz2.Commands;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;

namespace CreateQuestionFile.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            CreateCategoryFileCommand = new RelayCommand(CreateCategoryFile);
            AddNextQuestionCommand = new RelayCommand(AddNextQuestion);
        }

        private CategoryFileModel categoryFileModel = new CategoryFileModel();

        private TextInfo textInfo = new CultureInfo("pl-PL", false).TextInfo;

        private bool DoesExistQuestionsFolder()
        {
            if (Directory.Exists(QuestionsPath)) return true;
            else return false;
        }

        private bool AreAllFieldsNonEmpty()
        {
            if (String.IsNullOrWhiteSpace(CategoryTitleTextBoxText)) return false;

            if (String.IsNullOrWhiteSpace(QuestionTextBoxText)) return false;

            if (String.IsNullOrWhiteSpace(AnswerATextBoxText)) return false;
            if (String.IsNullOrWhiteSpace(AnswerBTextBoxText)) return false;
            if (String.IsNullOrWhiteSpace(AnswerCTextBoxText)) return false;
            if (String.IsNullOrWhiteSpace(AnswerDTextBoxText)) return false;

            if (!(IsAnswerACorrectCheckBoxIsChecked)
                && !(IsAnswerBCorrectCheckBoxIsChecked)
                && !(IsAnswerCCorrectCheckBoxIsChecked)
                && !(IsAnswerDCorrectCheckBoxIsChecked)) return false;

            return true;
        }

        private string WhichAnswerIsCorrect()
        {
            var correctAnswer = string.Empty;

            if (IsAnswerACorrectCheckBoxIsChecked) correctAnswer = AnswerATextBoxText;
            else if (IsAnswerBCorrectCheckBoxIsChecked) correctAnswer = AnswerBTextBoxText;
            else if (IsAnswerCCorrectCheckBoxIsChecked) correctAnswer = AnswerCTextBoxText;
            else if (IsAnswerDCorrectCheckBoxIsChecked) correctAnswer = AnswerDTextBoxText;

            return correctAnswer;
        }

        private void AssignPackageValuesToVariables()
        {
            NumberQuestionNumber++;
            NumberQuestionText = "Ilość pytań: " + NumberQuestionNumber.ToString();

            Question = QuestionTextBoxText;

            AnswerA = AnswerATextBoxText;
            AnswerB = AnswerBTextBoxText;
            AnswerC = AnswerCTextBoxText;
            AnswerD = AnswerDTextBoxText;

            CorrectAnswer = WhichAnswerIsCorrect();

            Question = Char.ToUpper(Question[0]) + Question.Substring(1);

            AnswerA = Char.ToUpper(AnswerA[0]) + AnswerA.Substring(1);
            AnswerB = Char.ToUpper(AnswerB[0]) + AnswerB.Substring(1);
            AnswerC = Char.ToUpper(AnswerC[0]) + AnswerC.Substring(1);
            AnswerD = Char.ToUpper(AnswerD[0]) + AnswerD.Substring(1);

            CorrectAnswer = Char.ToUpper(CorrectAnswer[0]) + CorrectAnswer.Substring(1);

            if (!(CategoryTitleTextBoxIsReadOnly)) CategoryTitleTextBoxIsReadOnly = true;
        }

        private void ClearFields()
        {
            QuestionTextBoxText = String.Empty;

            AnswerATextBoxText = String.Empty;
            AnswerBTextBoxText = String.Empty;
            AnswerCTextBoxText = String.Empty;
            AnswerDTextBoxText = String.Empty;

            IsAnswerACorrectCheckBoxIsChecked = false;
            IsAnswerBCorrectCheckBoxIsChecked = false;
            IsAnswerCCorrectCheckBoxIsChecked = false;
            IsAnswerDCorrectCheckBoxIsChecked = false;
        }

        private void AddNewPackage()
        {
            PackagesList.Add(new Packages()
            {
                ID = NumberQuestionNumber,
                Question = this.Question,
                AnswerA = this.AnswerA,
                AnswerB = this.AnswerB,
                AnswerC = this.AnswerC,
                AnswerD = this.AnswerD,
                CorrectAnswer = this.CorrectAnswer
            });
        }

        private void AssignCategoryValuesToVariables()
        {
            CategoryTitle = CategoryTitleTextBoxText;

            CategoryTitle = textInfo.ToTitleCase(CategoryTitle);

            categoryFileModel.Informations = new Informations()
            {
                CategoryTitle = CategoryTitle,
                NumberOfQuestions = NumberQuestionNumber,
            };

            categoryFileModel.Packages = PackagesList;
        }

        private void CreateFileName()
        {
            var fileName = CategoryTitle;

            fileName = fileName.ToLower();

            fileName = fileName.Replace(" ", "-");

            this.FileName = fileName;
        }

        private void SerializeAndCreateCategoryFile()
        {
            FullJSON = JsonConvert.SerializeObject(categoryFileModel, Formatting.Indented);

            File.WriteAllText(QuestionsPath
                + "/"
                + FileName
                + ".json", FullJSON);
        }

        private void InformAboutDone()
        {
            MessageBox.Show("Twój plik kategorii został dodany do gry!", "Sukces",
                MessageBoxButton.OK, MessageBoxImage.Information);
            System.Environment.Exit(0);
        }

        private void AddNextQuestion(object obj)
        {
            if (AreAllFieldsNonEmpty())
            {
                AssignPackageValuesToVariables();

                AddNewPackage();

                ClearFields();
            }
            else MessageBox.Show("Wszystkie pola muszą być wypełnione!", "Błąd",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void CreateCategoryFile(object obj)
        {
            if (DoesExistQuestionsFolder())
            {
                if (NumberQuestionNumber > 0)
                {
                    AssignCategoryValuesToVariables();

                    CreateFileName();

                    SerializeAndCreateCategoryFile();

                    InformAboutDone();
                }
                else MessageBox.Show("Liczba pytań musi być większa od zera!", "Błąd",
                        MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Nie znaleziono folderu z pytaniami!" +
                    "\nZalecana jest reinstalacja oprogramowania.",
                    "Błąd krytyczny", MessageBoxButton.OK, MessageBoxImage.Error);
                System.Environment.Exit(0);
            }
        }

        public bool CategoryTitleTextBoxIsReadOnly
        {
            get => model.CategoryTitleTextBoxIsReadOnly;
            set
            {
                model.CategoryTitleTextBoxIsReadOnly = value;
                OnPropertyChanged(nameof(CategoryTitleTextBoxIsReadOnly));
            }
        }

        public string QuestionsPath
        {
            get => model.QuestionsPath;
            set
            {
                model.QuestionsPath = value;
                OnPropertyChanged(nameof(QuestionsPath));
            }
        }

        public bool IsAnswerDCorrectCheckBoxIsChecked
        {
            get => model.IsAnswerDCorrectCheckBoxIsChecked;
            set
            {
                model.IsAnswerDCorrectCheckBoxIsChecked = value;
                OnPropertyChanged(nameof(IsAnswerDCorrectCheckBoxIsChecked));
            }
        }

        public bool IsAnswerCCorrectCheckBoxIsChecked
        {
            get => model.IsAnswerCCorrectCheckBoxIsChecked;
            set
            {
                model.IsAnswerCCorrectCheckBoxIsChecked = value;
                OnPropertyChanged(nameof(IsAnswerCCorrectCheckBoxIsChecked));
            }
        }

        public bool IsAnswerBCorrectCheckBoxIsChecked
        {
            get => model.IsAnswerBCorrectCheckBoxIsChecked;
            set
            {
                model.IsAnswerBCorrectCheckBoxIsChecked = value;
                OnPropertyChanged(nameof(IsAnswerBCorrectCheckBoxIsChecked));
            }
        }

        public bool IsAnswerACorrectCheckBoxIsChecked
        {
            get => model.IsAnswerACorrectCheckBoxIsChecked;
            set
            {
                model.IsAnswerACorrectCheckBoxIsChecked = value;
                OnPropertyChanged(nameof(IsAnswerACorrectCheckBoxIsChecked));
            }
        }

        public string AnswerDTextBoxText
        {
            get => model.AnswerDTextBoxText;
            set
            {
                model.AnswerDTextBoxText = value;
                OnPropertyChanged(nameof(AnswerDTextBoxText));
            }
        }

        public string AnswerCTextBoxText
        {
            get => model.AnswerCTextBoxText;
            set
            {
                model.AnswerCTextBoxText = value;
                OnPropertyChanged(nameof(AnswerCTextBoxText));
            }
        }

        public string AnswerBTextBoxText
        {
            get => model.AnswerBTextBoxText;
            set
            {
                model.AnswerBTextBoxText = value;
                OnPropertyChanged(nameof(AnswerBTextBoxText));
            }
        }

        public string AnswerATextBoxText
        {
            get => model.AnswerATextBoxText;
            set
            {
                model.AnswerATextBoxText = value;
                OnPropertyChanged(nameof(AnswerATextBoxText));
            }
        }

        public string QuestionTextBoxText
        {
            get => model.QuestionTextBoxText;
            set
            {
                model.QuestionTextBoxText = value;
                OnPropertyChanged(nameof(QuestionTextBoxText));
            }
        }

        public string CategoryTitleTextBoxText
        {
            get => model.CategoryTitleTextBoxText;
            set
            {
                model.CategoryTitleTextBoxText = value;
                OnPropertyChanged(nameof(CategoryTitleTextBoxText));
            }
        }

        public List<Packages> PackagesList
        {
            get => model.PackagesList;
            set
            {
                model.PackagesList = value;
                OnPropertyChanged(nameof(PackagesList));
            }
        }

        public string FileName
        {
            get => model.FileName;
            set
            {
                model.FileName = value;
                OnPropertyChanged(nameof(FileName));
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

        public string CategoryTitle
        {
            get => model.CategoryTitle;
            set
            {
                model.CategoryTitle = value;
                OnPropertyChanged(nameof(CategoryTitle));
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

        public int NumberQuestionNumber
        {
            get => model.NumberQuestionNumber;
            set
            {
                model.NumberQuestionNumber = value;
                OnPropertyChanged(nameof(NumberQuestionNumber));
            }
        }

        public string NumberQuestionText
        {
            get => model.NumberQuestionText;
            set
            {
                model.NumberQuestionText = value;
                OnPropertyChanged(nameof(NumberQuestionText));
            }
        }

        public RelayCommand AddNextQuestionCommand { get; set; }

        public RelayCommand CreateCategoryFileCommand { get; set; }

        private MainWindowModel model = new MainWindowModel();
    }
}