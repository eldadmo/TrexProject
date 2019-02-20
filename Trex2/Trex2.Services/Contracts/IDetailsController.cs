using System.Collections.Generic;
using System.Threading.Tasks;
using Trex2.Common.Models;

namespace Trex2.Services.Contracts
{
    public interface IDetailsController
    {
        Task<List<Person>> Get();
        Task<Person> Get(int PersonId);
        Task<Person> Post(Person Person);
        Task<bool> Put(int PersonId, Person Person);
        Task<bool> Delete(int PersonId);
    }
}