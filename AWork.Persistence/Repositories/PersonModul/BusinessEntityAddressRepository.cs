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
    public class BusinessEntityAddressRepository : RepositoryBase<BusinessEntityAddress>, IBusinessEntityAddressRepository
    {
        public BusinessEntityAddressRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(BusinessEntityAddress businessEntityAddress)
        {
            Update(businessEntityAddress);
        }
        public async Task<BusinessEntityAddress> GetBusinessEntityAddressById(int BusinessEntity, int addressID, bool trackChanges)
        {
            return await FindByCondition(c => c.BusinessEntityId.Equals(BusinessEntity) && c.AddressId.Equals(addressID), trackChanges)
                .Include(b => b.BusinessEntity)
                .Include(at => at.AddressType)
                .Include(a => a.Address)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<BusinessEntityAddress>> GetAllBusinessEntityAddress(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(a => a.AddressTypeId).ToListAsync();
        }

        public void Insert(BusinessEntityAddress businessEntityAddress)
        {
            Create(businessEntityAddress);

        }

        public void Remove(BusinessEntityAddress businessEntityAddress)
        {
            Delete(businessEntityAddress);
        }

        public async Task<BusinessEntityAddress> GetBusinessEntityAddressByBusinessEntityId(int businessEntityAddressId, bool trackChanges)
        {
            return await FindByCondition(c => c.BusinessEntityId.Equals(businessEntityAddressId), trackChanges)
                .Include(b => b.BusinessEntity)
                .Include(a => a.Address)
                .Include(at => at.AddressType)
                .SingleOrDefaultAsync();
        }
    }
}
