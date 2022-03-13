using Newtonsoft.Json;
using Quiz2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace CreateQuestionFile
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private static int numberQuestionNumber = 0;
        private string _NumberQuestionText = "Ilość pytań: " + numberQuestionNumber.ToString();

        private string question;

        private string answerA;
        private string answerB;
        private string answerC;
        private string answerD;

        private string correctAnswer;

        private string categoryTitle;

        private string fullJSON;

        private string fileName;

        private List<Packages> packagesList = new List<Packages>();

        private CategoryFileModel categoryFileModel = new CategoryFileModel();

        private TextInfo textInfo = new CultureInfo("pl-PL", false).TextInfo;

        public string NumberQuestionText
        {
            get => _NumberQuestionText;
            set
            {
                _NumberQuestionText = value;
                OnPropertyChanged("NumberQuestionText");
            }
        }

        private void ChooseOnlyOneCheckbox(object sender, RoutedEventArgs e)
        {
            var givenObject = sender as CheckBox;

            var objectName = givenObject.Name;

            if (objectName != IsAnswerACorrectCheckBox.Name) IsAnswerACorrectCheckBox.IsChecked = false;
            if (objectName != IsAnswerBCorrectCheckBox.Name) IsAnswerBCorrectCheckBox.IsChecked = false;
            if (objectName != IsAnswerCCorrectCheckBox.Name) IsAnswerCCorrectCheckBox.IsChecked = false;
            if (objectName != IsAnswerDCorrectCheckBox.Name) IsAnswerDCorrectCheckBox.IsChecked = false;
        }

        private bool AreAllFieldsNonEmpty()
        {
            if (String.IsNullOrWhiteSpace(CategoryTitleTextBox.Text.ToString())) return false;

            if (String.IsNullOrWhiteSpace(QuestionTextBox.Text.ToString())) return false;

            if (String.IsNullOrWhiteSpace(AnswerATextBox.Text.ToString())) return false;
            if (String.IsNullOrWhiteSpace(AnswerBTextBox.Text.ToString())) return false;
            if (String.IsNullOrWhiteSpace(AnswerCTextBox.Text.ToString())) return false;
            if (String.IsNullOrWhiteSpace(AnswerDTextBox.Text.ToString())) return false;

            if (IsAnswerACorrectCheckBox.IsChecked == false
                && IsAnswerBCorrectCheckBox.IsChecked == false
                && IsAnswerCCorrectCheckBox.IsChecked == false
                && IsAnswerDCorrectCheckBox.IsChecked == false) return false;

            return true;
        }

        private string WhichAnswerIsCorrect()
        {
            var correctAnswer = string.Empty;

            if (IsAnswerACorrectCheckBox.IsChecked == true) correctAnswer = AnswerATextBox.Text.ToString();
            else if (IsAnswerBCorrectCheckBox.IsChecked == true) correctAnswer = AnswerBTextBox.Text.ToString();
            else if (IsAnswerCCorrectCheckBox.IsChecked == true) correctAnswer = AnswerCTextBox.Text.ToString();
            else if (IsAnswerDCorrectCheckBox.IsChecked == true) correctAnswer = AnswerDTextBox.Text.ToString();

            return correctAnswer;
        }

        private void AssignPackageValuesToVariables()
        {
            numberQuestionNumber++;
            NumberQuestionText = "Ilość pytań: " + numberQuestionNumber.ToString();

            question = QuestionTextBox.Text.ToString();

            answerA = AnswerATextBox.Text.ToString();
            answerB = AnswerBTextBox.Text.ToString();
            answerC = AnswerCTextBox.Text.ToString();
            answerD = AnswerDTextBox.Text.ToString();

            correctAnswer = WhichAnswerIsCorrect();

            question = Char.ToUpper(question[0]) + question.Substring(1);

            answerA = Char.ToUpper(answerA[0]) + answerA.Substring(1);
            answerB = Char.ToUpper(answerB[0]) + answerB.Substring(1);
            answerC = Char.ToUpper(answerC[0]) + answerC.Substring(1);
            answerD = Char.ToUpper(answerD[0]) + answerD.Substring(1);

            correctAnswer = Char.ToUpper(correctAnswer[0]) + correctAnswer.Substring(1);
        }

        private void AddNewPackage()
        {
            packagesList.Add(new Packages()
            {
                ID = numberQuestionNumber,
                Question = question,
                AnswerA = answerA,
                AnswerB = answerB,
                AnswerC = answerC,
                AnswerD = answerD,
                CorrectAnswer = correctAnswer
            });
        }

        private void AssignCategoryValuesToVariables()
        {
            categoryTitle = CategoryTitleTextBox.Text.ToString();

            categoryTitle = textInfo.ToTitleCase(categoryTitle);

            categoryFileModel.Informations = new Informations()
            {
                CategoryTitle = categoryTitle,
                NumberOfQuestions = numberQuestionNumber,
            };

            categoryFileModel.Packages = packagesList;
        }

        private void CreateFileName()
        {
            var fileName = categoryTitle;

            fileName = fileName.ToLower();

            fileName = fileName.Replace(" ", "-");

            this.fileName = fileName;
        }

        private void SerializeAndCreateCategoryFile()
        {
            fullJSON = JsonConvert.SerializeObject(categoryFileModel, Formatting.Indented);

            File.WriteAllText("questions/"
                + fileName
                + ".json", fullJSON);
        }

        private void InformAboutDone()
        {
            MessageBox.Show("Twój plik kategorii został dodany do gry!", "Sukces",
                MessageBoxButton.OK, MessageBoxImage.Information);

            Application.Current.Shutdown();
        }

        private void AddNextQuestion(object sender, RoutedEventArgs e)
        {
            if (AreAllFieldsNonEmpty())
            {
                AssignPackageValuesToVariables();

                AddNewPackage();
            }
            else MessageBox.Show("Wszystkie pola muszą być wypełnione!", "Błąd",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void CreateCategoryFile(object sender, RoutedEventArgs e)
        {
            if (numberQuestionNumber > 0)
            {
                if (AreAllFieldsNonEmpty())
                {
                    AssignCategoryValuesToVariables();

                    CreateFileName();

                    SerializeAndCreateCategoryFile();

                    InformAboutDone();
                }
                else MessageBox.Show("Wszystkie pola muszą być wypełnione!", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Liczba pytań musi być większa od zera!", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [Conditional("DEBUG")]
        private void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
                throw new ArgumentNullException(GetType().Name + " does not contain property: " + propertyName);
        }
    }
}