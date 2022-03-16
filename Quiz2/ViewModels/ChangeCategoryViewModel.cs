using Newtonsoft.Json;
using Quiz2.Commands;
using Quiz2.Interfaces;
using Quiz2.Models;
using Quiz2.Patterns;
using Quiz2.ViewModels.ViewModelBase;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Quiz2.ViewModels
{
    internal class ChangeCategoryViewModel : BaseViewModel, IPageViewModel
    {
        public ChangeCategoryViewModel()
        {
            CreateButtons();
        }

        private bool DoesExistNonEmptyQuestionsFolder()
        {
            if (Directory.Exists(QuestionsPath)
                && Directory.EnumerateFileSystemEntries(QuestionsPath).Any()) return true;
            else return false;
        }

        private void CreateButtons()
        {
            if (DoesExistNonEmptyQuestionsFolder())
            {
                NumberFiles = CheckNumberFilesInFolder();
                for (int i = 0; i < NumberFiles; i++)
                {
                    NamesFiles.Add(Directory.GetFiles(QuestionsPath)[i]);
                    var allFileText = File.ReadAllText(NamesFiles[i]);
                    CategoryFileModel categoryModel = JsonConvert.DeserializeObject<CategoryFileModel>(allFileText);
                    CategoryName = categoryModel.Informations.CategoryTitle;
                    Buttons.Add(new ButtonsProperties()
                    {
                        Content = CategoryName
                    });
                }
            }
            else
            {
                MessageBox.Show("Nie znaleziono folderu z pytaniami lub jest on pusty!" +
                    "\nZalecana jest reinstalacja oprogramowania.",
                    "Błąd krytyczny", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private int CheckNumberFilesInFolder()
        {
            var NumberFiles = Directory.GetFiles(QuestionsPath, "*", SearchOption.TopDirectoryOnly).Length;

            return NumberFiles;
        }

        public List<ButtonsProperties> Buttons
        {
            get => model.Buttons;

            set
            {
                model.Buttons = value;
                OnPropertyChanged(nameof(Buttons));
            }
        }

        public List<string> NamesFiles
        {
            get => model.NamesFiles;

            set
            {
                model.NamesFiles = value;
                OnPropertyChanged(nameof(NamesFiles));
            }
        }

        public string CategoryName
        {
            get => model.CategoryName;

            set
            {
                model.CategoryName = value;
                OnPropertyChanged(nameof(CategoryName));
            }
        }

        public int NumberFiles
        {
            get => model.NumberFiles;

            set
            {
                model.NumberFiles = value;
                OnPropertyChanged(nameof(NumberFiles));
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

        public class ButtonsProperties
        {
            public string Content { get; set; }
        }

        private ICommand _GoToGameView;

        public ICommand GoToGameView
        {
            get
            {
                return _GoToGameView ?? (_GoToGameView = new RelayCommand(x =>
                {
                    Mediator.Notify("GoToGameView", "");
                }));
            }
        }

        private ChangeCategoryModel model = new ChangeCategoryModel();
    }
}