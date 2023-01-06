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
    public class PhoneNumberTypeRepository : RepositoryBase<PhoneNumberType>, IPhoneNumberTypeRepository
    {
        public PhoneNumberTypeRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(PhoneNumberType phoneNumberType)
        {
            Update(phoneNumberType);
        }

        public async Task<IEnumerable<PhoneNumberType>> GetAllPhoneNumberType(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(a => a.PhoneNumberTypeId).ToListAsync();
        }

        public async Task<PhoneNumberType> GetPhoneNumberTypeById(int phoneNumberTypeId, bool trackChanges)
        {
            return await FindByCondition(c => c.PhoneNumberTypeId.Equals(phoneNumberTypeId), trackChanges)
                .Include(p => p.PersonPhones)
                .SingleOrDefaultAsync();
        }

        public void Insert(PhoneNumberType phoneNumberType)
        {
            Create(phoneNumberType);
        }

        public void Remove(PhoneNumberType phoneNumberType)
        {
            Delete(phoneNumberType);
        }
    }
}
