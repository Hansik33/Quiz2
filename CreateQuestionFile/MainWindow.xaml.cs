using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;

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

        private List<JObject> packagesList = new List<JObject>();

        public string NumberQuestionText
        {
            get => _NumberQuestionText;
            set
            {
                _NumberQuestionText = value;
                OnPropertyChanged("NumberQuestionText");
            }
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
        }

        private void AddNewPackage()
        {
            packagesList.Add(new JObject(
                new JProperty("ID", numberQuestionNumber),
                new JProperty("Question", question),
                new JProperty("AnswerA", answerA),
                new JProperty("AnswerB", answerB),
                new JProperty("AnswerC", answerC),
                new JProperty("AnswerD", answerD),
                new JProperty("CorrectAnswer", correctAnswer))
                );
        }

        private void AddNextQuestion(object sender, RoutedEventArgs e)
        {
            AssignPackageValuesToVariables();
            AddNewPackage();
        }

        private void CreateFileCategory(object sender, RoutedEventArgs e)
        {
            categoryTitle = CategoryTitleTextBox.Text.ToString();

            JObject main = new JObject();
            JObject informations = new JObject(
                new JProperty("Informations:",
                new JObject(
                    new JProperty("CategoryTitle", categoryTitle),
                    new JProperty("NumberOfQuesions", numberQuestionNumber)
                    ))
                );
            JObject packages = new JObject(
                new JProperty("Packages",
                    new JArray(packagesList))
                );
            main.Merge(informations);
            main.Merge(packages);
            fullJSON = JsonConvert.SerializeObject(main, Formatting.Indented);

            File.WriteAllText("questions/"
                + categoryTitle.ToLower()
                + ".json", fullJSON);
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