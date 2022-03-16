using Quiz2.Commands;
using Quiz2.Interfaces;
using Quiz2.Patterns;
using Quiz2.ViewModels.ViewModelBase;
using System.Windows.Input;

namespace Quiz2.ViewModels
{
    internal class UserControl2ViewModel : BaseViewModel, IPageViewModel
    {
        private ICommand _goTo1;

        public ICommand GoTo1
        {
            get
            {
                return _goTo1 ?? (_goTo1 = new RelayCommand(x =>
                {
                    Mediator.Notify("GoTo1Screen", "");
                }));
            }
        }
    }
}