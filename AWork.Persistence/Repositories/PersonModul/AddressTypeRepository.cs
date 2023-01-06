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
    public class AddressTypeRepository : RepositoryBase<AddressType>, IAddressTypeRepository
    {
        public AddressTypeRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(AddressType addressType)
        {
            Update(addressType);
        }

        public async Task<AddressType> GetAddressTypeId(int addressTypeId, bool trackChanges)
        {
            return await FindByCondition(c => c.AddressTypeId.Equals(addressTypeId), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<AddressType>> GetAllAddressType(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(a => a.AddressTypeId).ToListAsync();
        }

        public void Insert(AddressType addressType)
        {
            Create(addressType);
        }

        public void Remove(AddressType addressType)
        {
            Delete(addressType);
        }
    }
}
