using Caliburn.Micro;
using Trex2.Contracts;

namespace Trex2.ViewModels
{
    public class ShellWindowViewModel :Conductor<IScreen>.Collection.OneActive, IShellWindowViewModel
    {
        public ShellWindowViewModel(IMainViewModel mainViewModel)
        {
            ActivateItem(mainViewModel);
        }
        public sealed override void ActivateItem(IScreen item)
        {
            base.ActivateItem(item);
        }
    }
}