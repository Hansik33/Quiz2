using Quiz2.Commands;
using Quiz2.Interfaces;
using Quiz2.Models;
using Quiz2.Patterns;
using Quiz2.ViewModels.ViewModelBase;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Quiz2.ViewModels
{
    internal class GameWindowViewModel : BaseViewModel
    {
        public GameWindowViewModel()
        {
            PageViewModels.Add(new ChangeCategoryViewModel());
            PageViewModels.Add(new GameViewModel());

            CurrentPageViewModel = PageViewModels[0];

            GoToChangeCategoryViewCommand = new RelayCommand(OnGoChangeCategoryViewModel);

            Mediator.Subscribe("GoToChangeCategoryView", OnGoChangeCategoryViewModel);
            Mediator.Subscribe("GoToGameView", OnGoGameView);
        }

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
                OnPropertyChanged(nameof(CurrentPageViewModel));
            }
        }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        private void OnGoChangeCategoryViewModel(object obj)
        {
            ChangeViewModel(PageViewModels[0]);
        }

        private void OnGoGameView(object obj)
        {
            ChangeViewModel(PageViewModels[1]);
        }

        public ICommand GoToChangeCategoryViewCommand { get; set; }

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        private GameWindowModel model = new GameWindowModel();
    }
}