using System.Configuration;
using Caliburn.Micro;
using Trex2.Contracts;

namespace Trex2.ViewModels
{
    public class ShellWindowViewModel :Conductor<IScreen>.Collection.OneActive, IShellWindowViewModel
    {
        private readonly IMainViewModel _mainViewModel;
        private string _title;

        public ShellWindowViewModel(IMainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public string Title
        {
            get => _title;
            set
            {
                if (value == _title) return;
                _title = value;
                NotifyOfPropertyChange();
            }
        }

        protected override void OnActivate()
        {
            Title = $"{ConfigurationManager.AppSettings["AppName"]} {ConfigurationManager.AppSettings["Version"]}"; 
            ActivateItem(_mainViewModel);
        }

        public sealed override void ActivateItem(IScreen item)
        {
            base.ActivateItem(item);
        }
    }
}