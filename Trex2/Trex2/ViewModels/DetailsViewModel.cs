using System.Threading.Tasks;
using Caliburn.Micro;
using Trex2.Common.Events;
using Trex2.Common.Models;
using Trex2.Contracts;

namespace Trex2.ViewModels
{
    public class DetailsViewModel : Screen, IDetailsViewModel
    {
        private readonly IEventAggregator m_eventAggregator;
        private string m_firstName;
        private string m_lastName;
        private string m_email;
        private bool m_canSubmit;
        private bool m_isBusy;

        public DetailsViewModel(IEventAggregator eventAggregator)
        {
            m_eventAggregator = eventAggregator;
            CheckFields();
        }

        public bool IsBusy
        {
            get => m_isBusy;
            set
            {
                if (value == m_isBusy) return;
                m_isBusy = value;
                NotifyOfPropertyChange();
            }
        }

        private void CheckFields()
        {
            CanSubmit = !(string.IsNullOrEmpty(m_firstName) || string.IsNullOrEmpty(m_lastName));
        }

        public string FirstName
        {
            get => m_firstName;
            set
            {
                if (value == m_firstName) return;
                m_firstName = value;
                NotifyOfPropertyChange();
                CheckFields();
            }
        }

        public string LastName
        {
            get => m_lastName;
            set
            {
                if (value == m_lastName) return;
                m_lastName = value;
                NotifyOfPropertyChange();
                CheckFields();
            }
        }

        public bool CanSubmit
        {
            get => m_canSubmit;
            set
            {
                if (value == m_canSubmit) return;
                m_canSubmit = value;
                NotifyOfPropertyChange();
            }
        }

        public string Email
        {
            get => m_email;
            set
            {
                if (value == m_email) return;
                m_email = value;
                NotifyOfPropertyChange();
                CheckFields();
            }
        }

        public async void Submit()
        {
            CanSubmit = false;
            IsBusy = true;
            await Task.Run(() =>
            {
                Task.Delay(350).Wait();
                var person = new Person { FirstName = FirstName, LastName = LastName, Email = Email };

                m_eventAggregator.PublishOnUIThread(new PersonAddEvent(person));
            });
            CanSubmit = true;
            IsBusy = false;
        }
    }
}