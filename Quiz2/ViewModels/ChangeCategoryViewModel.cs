using Quiz2.Commands;
using Quiz2.Interfaces;
using Quiz2.Models;
using Quiz2.Patterns;
using Quiz2.ViewModels.ViewModelBase;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Quiz2.ViewModels
{
    public class ChangeCategoryViewModel : BaseViewModel, IPageViewModel
    {
        public ChangeCategoryViewModel()
        {
            CreateButtons();
        }

        private bool DoesExistNonEmptyQuestionsFolder()
        {
            if (Directory.Exists(pathQuestions)
                && Directory.EnumerateFileSystemEntries(pathQuestions).Any()) return true;
            else return false;
        }

        private void CreateButtons()
        {
            if (DoesExistNonEmptyQuestionsFolder())
            {
                NumberFiles = CheckNumberFilesInFolder();
                for (int i = 0; i < NumberFiles; i++)
                {
                    NamesFiles.Add(Directory.GetFiles(pathQuestions)[i]);
                    var allFileText = File.ReadAllText(NamesFiles[i]);
                    //
                    Buttons.Add(new ButtonsProperties()
                    {
                        Content = CategoryName
                    });
                }
            }
            else
            {
                MessageBox.Show("Nie znaleziono folderu z pytaniami lub jest on pusty." +
                    "\nZalecana jest reinstalacja oprogramowania.",
                    "Błąd krytyczny", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private int CheckNumberFilesInFolder()
        {
            var NumberFiles = Directory.GetFiles(pathQuestions, "*", SearchOption.TopDirectoryOnly).Length;

            return NumberFiles;
        }

        public ObservableCollection<ButtonsProperties> Buttons
        {
            get => model.Buttons;

            set
            {
                model.Buttons = value;
                OnPropertyChanged("Buttons");
            }
        }

        public ObservableCollection<string> NamesFiles
        {
            get => model.NamesFiles;

            set
            {
                model.NamesFiles = value;
                OnPropertyChanged("NamesFiles");
            }
        }

        public string CategoryName
        {
            get => model.CategoryName;

            set
            {
                model.CategoryName = value;
                OnPropertyChanged("CategoryName");
            }
        }

        public int NumberFiles
        {
            get => model.NumberFiles;

            set
            {
                model.NumberFiles = value;
                OnPropertyChanged("NumberFiles");
            }
        }

        public string pathQuestions
        {
            get => model.pathQuestions;

            set
            {
                model.pathQuestions = value;
                OnPropertyChanged("pathQuestions");
            }
        }

        public class ButtonsProperties
        {
            public string? Content { get; set; }
        }

        private ICommand _goTo2;

        public ICommand GoTo2
        {
            get
            {
                return _goTo2 ?? (_goTo2 = new RelayCommand(x =>
                {
                    Mediator.Notify("GoTo2Screen", "");
                }));
            }
        }

        private ChangeCategoryModel model = new ChangeCategoryModel();
    }
}