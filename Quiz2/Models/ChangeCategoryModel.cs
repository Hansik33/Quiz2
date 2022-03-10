using System.Collections.ObjectModel;
using static Quiz2.ViewModels.ChangeCategoryViewModel;

namespace Quiz2.Models
{
    public class ChangeCategoryModel
    {
        private string _pathQuestions = @"questions";

        public string pathQuestions
        {
            get { return _pathQuestions; }
            set { _pathQuestions = value; }
        }

        public int NumberFiles { get; set; }

        public string? CategoryName { get; set; }

        public ObservableCollection<ButtonsProperties> Buttons { get; set; } = new ObservableCollection<ButtonsProperties>();

        public ObservableCollection<string> NamesFiles { get; set; } = new ObservableCollection<string>();
    }
}