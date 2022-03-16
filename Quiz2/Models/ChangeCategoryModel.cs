using System.Collections.ObjectModel;
using static Quiz2.ViewModels.ChangeCategoryViewModel;

namespace Quiz2.Models
{
    internal class ChangeCategoryModel
    {
        private string _PathQuestions = @"questions";

        public string PathQuestions
        {
            get => _PathQuestions; 
            set => _PathQuestions = value; 
        }

        public int NumberFiles { get; set; }

        public string CategoryName { get; set; }

        public ObservableCollection<ButtonsProperties> Buttons { get; set; } = new ObservableCollection<ButtonsProperties>();

        public ObservableCollection<string> NamesFiles { get; set; } = new ObservableCollection<string>();
    }
}