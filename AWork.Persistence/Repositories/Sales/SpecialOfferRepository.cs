using AWork.Domain.Dto.Sales;
using AWork.Domain.Models;
using AWork.Domain.Repositories.Sales;
using AWork.Persistence.Base;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.Sales
{
    public class SpecialOfferRepository : RepositoryBase<SpecialOffer>, ISpecialOfferRepository
    {
        public SpecialOfferRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(SpecialOffer specialOfferProduct)
        {
            Update(specialOfferProduct);
        }

        public async Task<IEnumerable<SpecialOffer>> GetAllSpecialOffer(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(c => c.SpecialOfferId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string productName, bool trackChanges)
        {
            // cara sql : select * from Production.Product where Name like 'Cha%'
            var products = await _dbContext.Products.Where(p => p.Name.Contains(productName)).ToListAsync();
            return products;
        }

        public async Task<SpecialOffer> GetSpecialOfferById(int specialOfferId, bool trackChanges)
        {
            return await FindByCondition(c => c.SpecialOfferId.Equals(specialOfferId), trackChanges)
                .Include(p=>p.SpecialOfferProducts)
                .SingleOrDefaultAsync();
        }

        /*public async Task<IEnumerable<SpecialOffer>> GetSpecialOfferPaged(int pageIndex, int pageSize, bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(c => c.SpecialOfferId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }*/

        public void Insert(SpecialOffer specialOffer)
        {
            Create(specialOffer);
        }

        public void Remove(SpecialOffer specialOffer)
        {
            Delete(specialOffer);
        }
    }
}
