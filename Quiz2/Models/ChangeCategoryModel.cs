using System.Collections.Generic;
using static Quiz2.ViewModels.ChangeCategoryViewModel;

namespace Quiz2.Models
{
    internal class ChangeCategoryModel
    {
        private string _questionsPath = @"questions";

        public string QuestionsPath
        {
            get => _questionsPath;
            set => _questionsPath = value;
        }

        public int NumberFiles { get; set; }

        public string CategoryName { get; set; }

        public List<ButtonsProperties> Buttons { get; set; } = new List<ButtonsProperties>();

        public List<string> NamesFiles { get; set; } = new List<string>();
    }
}