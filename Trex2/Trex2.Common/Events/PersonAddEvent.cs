using Trex2.Common.Models;

namespace Trex2.Common.Events
{
    public class PersonAddEvent
    {
        public Person Person { get; }

        public PersonAddEvent(Person person)
        {
            Person = person;
        }
    }
}