using CreateQuestionFile.Models;
using CreateQuestionFile.ViewModels;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
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
    }
}