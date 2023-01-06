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
    public class BusinessEntityContactRepository : RepositoryBase<BusinessEntityContact>, IBusinessEntityContactRepository
    {
        public BusinessEntityContactRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(BusinessEntityContact businessEntityContact)
        {
            Update(businessEntityContact);
        }

        public async Task<IEnumerable<BusinessEntityContact>> GetAllBusinessEntityContact(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(a => a.ContactTypeId).ToListAsync();
        }

        public async Task<BusinessEntityContact> GetBusinessEntityContactById(int businessEntityContactId, bool trackChanges)
        {
            return await FindByCondition(c => c.ContactTypeId.Equals(businessEntityContactId), trackChanges).SingleOrDefaultAsync();
        }

        public void Insert(BusinessEntityContact businessEntityContact)
        {
            Create(businessEntityContact);
        }

        public void Remove(BusinessEntityContact businessEntityContact)
        {
            Delete(businessEntityContact);
        }
    }
}
