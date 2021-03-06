using Quiz2.Commands;
using Quiz2.Models;
using Quiz2.ViewModels.ViewModelBase;
using System.Diagnostics;
using System.Windows.Input;

namespace Quiz2.ViewModels
{
    internal class InfoWindowViewModel : BaseViewModel
    {
        public InfoWindowViewModel()
        {
            GoToWebsiteCommand = new RelayCommand(GoToWebsite);
        }

        private void GoToWebsite(object obj)
        {
            Process.Start(new ProcessStartInfo(Webiste) { UseShellExecute = true });
        }

        public string Informations
        {
            get => model.Informations;

            set
            {
                model.Informations = value;
                OnPropertyChanged(nameof(Informations));
            }
        }

        public string Webiste
        {
            get => model.Website;

            set
            {
                model.Website = value;
                OnPropertyChanged(nameof(Webiste));
            }
        }

        public ICommand GoToWebsiteCommand { get; set; }

        private InfoWindowModel model = new InfoWindowModel();
    }
}