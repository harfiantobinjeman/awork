using AWork.Domain.Models;
using AWork.Domain.Repositories.Purchasing;
using AWork.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.Purchasing
{
    public class VendorRepository : RepositoryBase<Vendor>, IVendorRepository
    {
        public VendorRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(Vendor vendor)
        {
            Update(vendor);
        }

        public async Task<IEnumerable<Vendor>> GetAllVendor(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(v => v.BusinessEntityId).Include(poh=>poh.BusinessEntity).ToListAsync();
        }

        public async Task<Vendor> GetVendorById(int id, bool trackChanges)
        {
            return await FindByCondition(v => v.BusinessEntityId.Equals(id), trackChanges)
                .Include(pv => pv.ProductVendors)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Vendor>> GetVendorPaged(int pageIndex, int pageSize, bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(b => b.BusinessEntityId)
                .Include(be => be.BusinessEntity)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public void Insert(Vendor vendor)
        {
            Create(vendor);
        }

        public void Remove(Vendor vendor)
        {
            Delete(vendor);
        }
        public async Task<IEnumerable<Vendor>> FindVendorByName(string vendorName, bool trackChanges)
        {
            var vendor = await _dbContext.Vendors.Where(p => p.Name.Contains(vendorName)).ToListAsync();
            return vendor;
        }
    }
}
