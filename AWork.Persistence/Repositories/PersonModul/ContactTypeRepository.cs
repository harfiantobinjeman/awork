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
    public class ContactTypeRepository : RepositoryBase<ContactType>, IContactTypeRepository
    {
        public ContactTypeRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(ContactType contactType)
        {
            Update(contactType);
        }

        public async Task<IEnumerable<ContactType>> GetAllContactType(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.ContactTypeId).ToListAsync();
        }

        public async Task<ContactType> GetAllContactTypeById(int contactId, bool trackChanges)
        {
            return await FindByCondition(c => c.ContactTypeId.Equals(contactId), trackChanges).SingleOrDefaultAsync();
        }

        public void Insert(ContactType contactType)
        {
            Create(contactType);
        }

        public void Remove(ContactType contactType)
        {
            Delete(contactType);
        }
    }
}
