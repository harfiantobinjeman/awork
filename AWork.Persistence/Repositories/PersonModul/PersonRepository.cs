using AWork.Domain.Dto;
using AWork.Domain.Models;
using AWork.Domain.Repositories.PersonModul;
using AWork.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.PersonModul
{
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(Person person)
        {
            Update(person);
        }

        public async Task<IEnumerable<Person>> GetAllPerson(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.BusinessEntityId)
                .Include(c => c.BusinessEntity)
                .Include(c => c.EmailAddresses)
                .ToListAsync();

        }

        public async Task<IEnumerable<Person>> GetAllPersonPage(int pageIndex, int pageSize, bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.FirstName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Person> GetBusinessEntityByEmail(string email, bool trackChanges)
        {
            return await FindByCondition(c => c.BusinessEntityId.Equals(email), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<Person> GetPerson(string firstName, string lastName, bool trackChanges)
        {
            return await FindByCondition(a => a.FirstName.Equals(firstName)
            && a.LastName.Equals(lastName), trackChanges)
                .Include(c => c.BusinessEntity)
                .Include(c => c.EmailAddresses)
                .SingleOrDefaultAsync();
        }

        public async Task<Person> GetPersonById(int personId, bool trackChanges)
        {
            var person = await FindByCondition(p => p.BusinessEntityId.Equals(personId), trackChanges)
                .Include(b => b.BusinessEntity)
                .Include(bp => bp.BusinessEntityContacts)
                .Include(b => b.BusinessEntity.BusinessEntityAddresses)
                .Include(e => e.EmailAddresses)
                .Include(pp => pp.PersonPhones)
                .SingleOrDefaultAsync();
            return person;
        }
        public void Insert(Person person)
        {
            Create(person);
        }


        public void Remove(Person person)
        {
            Delete(person);
        }

        public async Task<IEnumerable<TotalPersonByPersonType>> GetTotalPersonByPersonType(string personType)
        {
            var rawSQL = await _dbContext.GetTotalPersonByPersonTypeSQL
                .FromSqlRaw("select PersonType,count(PersonType)TotalPerson from Person.Person group by PersonType")
                .Select(x => new TotalPersonByPersonType
                {
                    PersonType = x.PersonType,
                    TotalPerson = x.TotalPerson

                })
                .OrderBy(x => x.PersonType)
                .ToListAsync();

            return rawSQL;
        }
    }
}
