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
    public class EmailAddressRepository : RepositoryBase<EmailAddress>, IEmailAddressRepository
    {
        public EmailAddressRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }


        public void Edit(EmailAddress emailAddress)
        {
            Update(emailAddress);

        }

        public async Task<IEnumerable<EmailAddress>> GetAllEmailAddress(bool trackChanges)
        {
            return await FindAll(trackChanges).Include(p => p.BusinessEntity)
                .OrderBy(e => e.EmailAddress1)
                .ToListAsync();
        }

        public async Task<EmailAddress> GetEmailAddress(string emailAddress, bool trackChanges)
        {
            return await FindByCondition(c => c.EmailAddress1.Equals(emailAddress), trackChanges)
                .Include(p => p.BusinessEntity)
                .SingleOrDefaultAsync();
        }

        public async Task<EmailAddress> GetEmailAddressId(string emailAddress, int businessEntity, bool trackChanges)
        {
            return await FindByCondition(e => e.EmailAddress1.Equals(emailAddress) && e.BusinessEntityId.Equals(businessEntity), trackChanges)
                .Include(p => p.BusinessEntity)
                .SingleOrDefaultAsync(p => p.BusinessEntityId == businessEntity);
        }

        public async Task<EmailAddress> GetEmailId(int businessEntity, bool trackChanges)
        {
            return await FindByCondition(c => c.BusinessEntityId.Equals(businessEntity), trackChanges)
                .Include(b => b.BusinessEntity)
                .SingleOrDefaultAsync();
        }

        public void Insert(EmailAddress emailAddress)
        {
            Create(emailAddress);
        }

        public void Remove(EmailAddress emailAddress)
        {
            Delete(emailAddress);
        }
    }
}
