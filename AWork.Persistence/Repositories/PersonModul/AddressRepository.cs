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
    public class AddressRepository : RepositoryBase<Address>, IAddressRepository
    {
        public AddressRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(Address address)
        {
            Update(address);
        }

        public async Task<Address> GetAllAddressById(int addressId, bool trackChanges)
        {
            return await FindByCondition(c => c.AddressId.Equals(addressId), trackChanges)
                .Include(b => b.BusinessEntityAddresses)
                .Include(s => s.StateProvince)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Address>> GetAllAdress(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.AddressId).ToListAsync();
        }

        public void Insert(Address address)
        {
            Create(address);
        }

        public void Remove(Address address)
        {
            Delete(address); ;
        }
    }
}
