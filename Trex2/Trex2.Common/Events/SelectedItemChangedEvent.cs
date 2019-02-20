using Trex2.Common.Models;

namespace Trex2.Common.Events
{
    /// <summary>
    /// Set the same Person instance!
    /// </summary>
    public class SelectedItemChangedEvent 
    {
        public Person Person { get; }

        public SelectedItemChangedEvent(Person person)
        {
            Person = person;
        }
    }
}