using Caliburn.Micro;
using Trex2.Common.Events;
using Trex2.Common.Models;
using Trex2.Contracts;
using Trex2.Services.Contracts;

namespace Trex2.ViewModels
{
    public class DetailsFormViewModel : Screen, IDetailsFormViewModel, IHandle<SelectedItemChangedEvent>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDetailsController _detailsController;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _comment;
        private int _id;
        private bool _canUpdate;

        public DetailsFormViewModel(IEventAggregator eventAggregator, IDetailsController detailsController)
        {
            _eventAggregator = eventAggregator;
            _detailsController = detailsController;
            eventAggregator.Subscribe(this);
        }

        public bool CanUpdate
        {
            get => _canUpdate;
            set
            {
                if (value == _canUpdate) return;
                _canUpdate = value;
                NotifyOfPropertyChange();
            }
        }

        public int Id
        {
            get => _id;
            set
            {
                if (value == _id) return;
                CanUpdate = value > 0;
                _id = value;
                NotifyOfPropertyChange();
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value == _firstName) return;
                _firstName = value;
                NotifyOfPropertyChange();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (value == _lastName) return;
                _lastName = value;
                NotifyOfPropertyChange();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (value == _email) return;
                _email = value;
                NotifyOfPropertyChange();
            }
        }

        public string Comment
        {
            get => _comment;
            set
            {
                if (value == _comment) return;
                _comment = value;
                NotifyOfPropertyChange();
            }
        }
        public async void Update()
        {
            var updatedDetails = new Person()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Comment = Comment
            };
            if (await _detailsController.Put(Id, updatedDetails))
            {
                _eventAggregator.PublishOnUIThread(new PersonDetailsUpdate(updatedDetails));
            }
            else
            {
                _eventAggregator.PublishOnUIThread("Person Details did not update.");
            }
           
        }

        public void Handle(SelectedItemChangedEvent message)
        {
            if (message == null) return;
            FillFields(message.Person);
        }

        private void FillFields(Person messagePerson)
        {
            if (messagePerson == null)
            {
                Id = 0;
                FirstName = LastName = Comment = Email = null;
            }
            else
            {
                Id = messagePerson.Id;
                FirstName = messagePerson.FirstName;
                LastName = messagePerson.LastName;
                Email = messagePerson.Email;
                Comment = messagePerson.Comment;
            }

        }

    }
}