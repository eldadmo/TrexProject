using System.Linq;
using Caliburn.Micro;
using Trex2.Contracts;
using Trex2.Models;

namespace Trex2.ViewModels
{
    public class SubscribersViewModel : Screen, ISubscribersViewModel, IHandle<Person>
    {
        private readonly IEventAggregator _eventAggregator;
        private Person _selectedItem;
        private bool _canUnSubscribe;
        public IObservableCollection<Person> Subscribers { get; set; }

        public Person SelectedItem
        {
            get => _selectedItem;
            set
            {
                CanUnSubscribe = value != null;
                if (Equals(value, _selectedItem)) return;
                _selectedItem = value;
                NotifyOfPropertyChange();
            }
        }

        public bool CanUnSubscribe
        {
            get => _canUnSubscribe;
            set
            {
                if (value == _canUnSubscribe) return;
                _canUnSubscribe = value;
                NotifyOfPropertyChange();
            }
        }

        public SubscribersViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            eventAggregator.Subscribe(this);
            Subscribers = new BindableCollection<Person>();
        }

        public void Handle(Person message)
        {
            if (message == null) return;
            var person = Subscribers.FirstOrDefault(x =>
                x.LastName.Equals(message.LastName) && x.FirstName.Equals(message.FirstName));
            if (person != null)
            {
                //TODO : Create Notice
            }
            else
            {
                Subscribers.Add(message);
            }
                
        }
        
        public void UnSubscribe()
        {
            if(SelectedItem==null) return;
            Subscribers.Remove(SelectedItem);
            SelectedItem = null;
        }
    }
}