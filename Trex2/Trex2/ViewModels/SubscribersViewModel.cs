using System.Linq;
using System.Windows.Input;
using Caliburn.Micro;
using Trex2.Common.Events;
using Trex2.Common.Models;
using Trex2.Contracts;
using Trex2.Services.Contracts;

namespace Trex2.ViewModels
{
    public class SubscribersViewModel : Screen, ISubscribersViewModel, IHandle<PersonAddEvent> ,IHandle<PersonDetailsUpdate>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDetailsController _detailsController;
        private Person _selectedItem;
        private bool _canUnSubscribe;
        private IObservableCollection<Person> _subscribers;



        public SubscribersViewModel(IEventAggregator eventAggregator, IDetailsController detailsController)
        {
            _eventAggregator = eventAggregator;
            _detailsController = detailsController;
            UpdateSubscribers();
            eventAggregator.Subscribe(this);
            
        }

        private async void UpdateSubscribers()
        {
            var items = await _detailsController.Get();
            Subscribers = new BindableCollection<Person>();
            Subscribers.AddRange(items);                    
        }
        public IObservableCollection<Person> Subscribers
        {
            get => _subscribers;
            set
            {
                if (Equals(value, _subscribers)) return;
                _subscribers = value;
                NotifyOfPropertyChange();
            }
        }
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

        public async void Handle(PersonAddEvent itemAddedEvent)
        {
            if (itemAddedEvent?.Person == null) return;
            if (Subscribers.FirstOrDefault(x => x.Id == itemAddedEvent.Person.Id) != null)
            {
                _eventAggregator.PublishOnUIThread("Person is Already Exist");
            }
            else
            {
                var details = await _detailsController.Post(itemAddedEvent.Person);
                Subscribers.Add(details);
            }
                
        }

        public void OnSelectedItem(object sender, MouseButtonEventArgs e)
        {
            if (SelectedItem !=null)
            _eventAggregator.PublishOnUIThread(new SelectedItemChangedEvent(SelectedItem));
        }
        public async void UnSubscribe()
        {
            if(SelectedItem==null) return;
            if (await _detailsController.Delete(SelectedItem.Id))
            {
                Subscribers.Remove(SelectedItem);
                _eventAggregator.PublishOnUIThread(new SelectedItemChangedEvent(SelectedItem));
            }
            else
            {
                _eventAggregator.PublishOnUIThread($"Can't remove the following Person {SelectedItem.Id}");
            }
            SelectedItem = null;
        }

        public void Handle(PersonDetailsUpdate message)
        {
            if(message?.Person == null) return;

            Person subscriber = Subscribers.FirstOrDefault(x=>x.Id == message.Person.Id);

            if (subscriber == null) return;

            subscriber.FirstName = message.Person.FirstName;
            subscriber.LastName = message.Person.LastName;
            subscriber.Email = message.Person.Email;
            subscriber.Comment = message.Person.Comment;
            Subscribers.Refresh();
            //Subscribers.Remove(subscriber);
            //Subscribers.Add(message.Person);
        }
    }
}