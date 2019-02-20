using Trex2.Common.Models;

namespace Trex2.Common.Events
{
    public class PersonDetailsUpdate
    {
        public Person Person { get; }

        public PersonDetailsUpdate(Person person)
        {
            Person = person;
        }
    }
}