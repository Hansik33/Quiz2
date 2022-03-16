using Newtonsoft.Json;
using Quiz2.Commands;
using Quiz2.Interfaces;
using Quiz2.Models;
using Quiz2.Patterns;
using Quiz2.ViewModels.ViewModelBase;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Quiz2.ViewModels
{
    internal class GameViewModel : BaseViewModel, IPageViewModel
    {
        public GameViewModel()
        {
            GetChosenCategoryFilePath();
        }

        private void GetChosenCategoryFilePath()
        {
            if (File.Exists(@"CHOSENCATEGORY.temp"))
            {
                var categoryTitle = File.ReadAllText(@"CHOSENCATEGORY.temp");

                CategoryTitle = categoryTitle.ToString();
            }

            var namesFilesList  = new List<string>(changeCategoryViewModel.NamesFiles);

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

        ChangeCategoryViewModel changeCategoryViewModel = new ChangeCategoryViewModel();

        private ICommand _GoToChangeCategoryView;

        public ICommand GoToChangeCategoryView
        {
            get
            {
                return _GoToChangeCategoryView ?? (_GoToChangeCategoryView = new RelayCommand(x =>
                {
                    Mediator.Notify("GoToChangeCategoryView", "");
                }));
            }
        }

        GameModel model = new GameModel();
    }
}