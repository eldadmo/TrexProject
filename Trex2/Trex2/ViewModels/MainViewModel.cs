using Caliburn.Micro;
using Trex2.Contracts;

namespace Trex2.ViewModels
{
    public class MainViewModel : Conductor<IScreen>.Collection.AllActive, IMainViewModel
    {
        public MainViewModel(IDetailsViewModel detailsViewModel)
        {
            ActivateItem(detailsViewModel);
        }

        public sealed override void ActivateItem(IScreen item)
        {
            base.ActivateItem(item);
        }
    }
}