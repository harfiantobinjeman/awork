using AWork.Domain.Models;
using AWork.Domain.Repositories.PersonModul;
using AWork.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.PersonModul
{
    public class PersonPhoneRepository : RepositoryBase<PersonPhone>, IPersonPhoneRepository
    {
        public PersonPhoneRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(PersonPhone personPhone)
        {
            Update(personPhone);
        }

        public async Task<IEnumerable<PersonPhone>> GetAllPersonPhone(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.PhoneNumberTypeId)
                .Include(pp => pp.PhoneNumberType)
                .ToListAsync();
        }

        public async Task<IEnumerable<PersonPhone>> GetPersonPhoneById(int busineessEntity, bool trackChanges)
        {
            var personPhone = await FindAll(trackChanges)
                .Include(p => p.BusinessEntity)
                .Include(c => c.PhoneNumberType)
                .Where(pp => pp.BusinessEntityId.Equals(busineessEntity))
                .ToListAsync();
            return personPhone;
        } 

        public void Insert(PersonPhone personPhone)
        {
            Create(personPhone);
        }

        public void Remove(PersonPhone personPhone)
        {
            Delete(personPhone);
        }
    }
}
