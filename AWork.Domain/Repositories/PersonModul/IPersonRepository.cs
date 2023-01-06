using AWork.Domain.Dto;
using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.PersonModul
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAllPerson(bool trackChanges);
        Task<Person> GetPersonById(int personId, bool trackChanges);
        Task <IEnumerable<Person>> GetAllPersonPage (int pageIndex, int pageSize, bool trackChanges);
        Task<Person> GetPerson(string firstName, string lastName, bool trackChanges);
        
        Task<Person> GetBusinessEntityByEmail(string email, bool trackChanges);
        Task<IEnumerable<TotalPersonByPersonType>> GetTotalPersonByPersonType(string personType);
        void Insert(Person person);
        void Edit(Person person);
        void Remove(Person person);
    }
}
