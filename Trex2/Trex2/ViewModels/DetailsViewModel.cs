using System.Threading.Tasks;
using Caliburn.Micro;
using Trex2.Contracts;
using Trex2.Models;

namespace Trex2.ViewModels
{
    public class DetailsViewModel : Screen , IDetailsViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private string _firstName;
        private string _lastName;
        private bool _canSubmit;
        private bool _isBusy;

        public DetailsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            CheckFields();
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (value == _isBusy) return;
                _isBusy = value;
                NotifyOfPropertyChange();
            }
        }

        private void CheckFields()
        {
            CanSubmit = !(string.IsNullOrEmpty(_firstName) || string.IsNullOrEmpty(_lastName));
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value == _firstName) return;
                _firstName = value;              
                NotifyOfPropertyChange();
                CheckFields();
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
                CheckFields();
            }
        }

        public bool CanSubmit
        {
            get => _canSubmit;
            set
            {
                if (value == _canSubmit) return;
                _canSubmit = value;
                NotifyOfPropertyChange();
            }
        }

        public async void Submit()
        {
            CanSubmit = false;
            IsBusy = true;
            await Task.Run(() =>
            {
                Task.Delay(350).Wait();
                var message = new Person {FirstName = FirstName, LastName = LastName};
                _eventAggregator.PublishOnUIThread(message);
            });         
            CanSubmit = true;
            IsBusy = false;
        }


    }
}