using Caliburn.Micro;
using Trex2.Contracts;

namespace Trex2.ViewModels
{
    public class MainViewModel : Conductor<IScreen>.Collection.AllActive, IMainViewModel, IHandle<string>
    {
        private string _notificationArea;

        public MainViewModel(IDetailsViewModel detailsViewModel, ISubscribersViewModel subscribersViewModel, IEventAggregator eventAggregator)
        {
            eventAggregator.Subscribe(this);
            Items.Add(detailsViewModel);
            Items.Add(subscribersViewModel);
        }

        public string NotificationArea
        {
            get => _notificationArea;
            set
            {
                if (value == _notificationArea) return;
                _notificationArea = value;
                NotifyOfPropertyChange();
            }
        }

        public sealed override void ActivateItem(IScreen item)
        {
            base.ActivateItem(item);
        }

        public void Handle(string notificationMessage)
        {
            NotificationArea += notificationMessage;
        }

        
    }
}