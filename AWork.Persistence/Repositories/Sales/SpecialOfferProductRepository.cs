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
    public class SpecialOfferProductRepository : RepositoryBase<SpecialOfferProduct>, ISpecialOfferProductRepository
    {
        public SpecialOfferProductRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(SpecialOfferProduct product)
        {
            Update(product);
        }

        public async Task<IEnumerable<SpecialOfferProduct>> GetAllSpecialOfferProduct(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.SpecialOfferId)
                /*.Include(e => e.SpecialOfferId)
                .Include(s => s.SpecialOffer)*/
                .Include(p => p.Product)
                .ToListAsync();
        }

        public async Task<SpecialOfferProduct> GetSpecialOfferProductById(int specialOfferId, bool trackChanges)
        {
            return await FindByCondition(c => c.SpecialOfferId.Equals(specialOfferId), trackChanges)
                //.Include(e => e.SpecialOfferId)
                //.Include(s => s.SpecialOffer)
                .Include(p => p.Product)
                .SingleOrDefaultAsync();
        }

        public void Insert(SpecialOfferProduct specialOfferProduct)
        {
            Create(specialOfferProduct);
        }

        public void Remove(SpecialOfferProduct specialOfferProduct)
        {
            Delete(specialOfferProduct);
        }
    }
}
