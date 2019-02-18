using Caliburn.Micro;
using Trex2.Contracts;

namespace Trex2.ViewModels
{
    public class ShellWindowViewModel :Conductor<IScreen>.Collection.OneActive, IShellWindowViewModel
    {
        private readonly IMainViewModel _mainViewModel;

        public ShellWindowViewModel(IMainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        protected override void OnActivate()
        {
            ActivateItem(_mainViewModel);
        }

        public sealed override void ActivateItem(IScreen item)
        {
            base.ActivateItem(item);
        }
    }
}