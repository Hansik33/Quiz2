using Quiz2.Commands;
using Quiz2.Interfaces;
using Quiz2.Models;
using Quiz2.Patterns;
using Quiz2.ViewModels.ViewModelBase;
using Quiz2.Views;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Quiz2.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            ShowApplicationInfoCommand = new RelayCommand(ShowApplicationInfo);
            TurnOffApplicationCommand = new RelayCommand(TurnOffApplication);

            PageViewModels.Add(new UserControl1ViewModel());
            PageViewModels.Add(new UserControl2ViewModel());

            CurrentPageViewModel = PageViewModels[0];

            Mediator.Subscribe("GoTo1Screen", OnGo1Screen);
            Mediator.Subscribe("GoTo2Screen", OnGo2Screen);
        }

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                _currentPageViewModel = value;
                OnPropertyChanged("CurrentPageViewModel");
            }
        }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        private void OnGo1Screen(object obj)
        {
            ChangeViewModel(PageViewModels[0]);
        }

        private void OnGo2Screen(object obj)
        {
            ChangeViewModel(PageViewModels[1]);
        }

        private void TurnOffApplication(object obj)
        {
            Application.Current.Shutdown();
        }

        private void ShowApplicationInfo(object obj)
        {
            infoWindow.Show();
            if (Application.Current.MainWindow != null) Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private InfoWindow infoWindow = new InfoWindow();

        public ICommand TurnOffApplicationCommand { get; set; }
        public ICommand ShowApplicationInfoCommand { get; set; }

        private MainWindowModel model = new MainWindowModel();
    }
}