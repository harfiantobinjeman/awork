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
    public class BusinessEntityRepository : RepositoryBase<BusinessEntity>, IBusinessEntityRepository
    {
        public BusinessEntityRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public Task<BusinessEntity> CreateBusinessEntity()
        {
            throw new NotImplementedException();
        }
        public void Edit(BusinessEntity businessEntity)
        {
            Update(businessEntity);
        }
        public async Task<IEnumerable<BusinessEntity>> GetAllBusinessEntity(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.BusinessEntityId)
                .Include(p => p.Person)
                .Include(pba => pba.BusinessEntityAddresses)
                .Include(pc => pc.BusinessEntityContacts)
                .ToListAsync();
        }


        public async Task<BusinessEntity> GetBusinessEntityById(int businessEntityId, bool trackChanges)
        {
            return await FindByCondition(c => c.BusinessEntityId.Equals(businessEntityId), trackChanges)
                .Include(p => p.Person)
                .Include(s => s.Store)
                .Include(v => v.Vendor)
                .SingleOrDefaultAsync();
        }

        public void Insert(BusinessEntity businessEntity)
        {
            Create(businessEntity);
        }

        public void Remove(BusinessEntity businessEntity)
        {
            Delete(businessEntity);
        }
    }
}
