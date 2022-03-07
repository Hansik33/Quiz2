using Quiz2.Commands;
using Quiz2.Interfaces;
using Quiz2.Patterns;
using Quiz2.ViewModels.ViewModelBase;
using System.Windows.Input;

namespace Quiz2.ViewModels
{
    public class UserControl1ViewModel : BaseViewModel, IPageViewModel
    {
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
    }
}