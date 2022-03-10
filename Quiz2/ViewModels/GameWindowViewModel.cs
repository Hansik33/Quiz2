using Quiz2.Interfaces;
using Quiz2.Models;
using Quiz2.Patterns;
using Quiz2.ViewModels.ViewModelBase;
using System.Collections.Generic;
using System.Linq;

namespace Quiz2.ViewModels
{
    public class GameWindowViewModel : BaseViewModel
    {
        public GameWindowViewModel()
        {
            PageViewModels.Add(new ChangeCategoryViewModel());
            PageViewModels.Add(new UserControl2ViewModel());

            CurrentPageViewModel = PageViewModels[0];

            Mediator.Subscribe("GoTo1Screen", OnGo1Screen);
            Mediator.Subscribe("GoTo2Screen", OnGo2Screen);
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

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        private GameWindowModel model = new GameWindowModel();
    }
}