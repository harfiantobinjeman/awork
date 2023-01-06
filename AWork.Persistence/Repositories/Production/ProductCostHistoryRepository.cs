using AWork.Domain.Models;
using AWork.Domain.Repositories.Production;
using AWork.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.Production
{
    internal class ProductCostHistoryRepository : RepositoryBase<ProductCostHistory>, IProductCostHistoryRepository
    {
        public ProductCostHistoryRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(ProductCostHistory productCostHistory)
        {
            Update(productCostHistory);
        }

        public async Task<IEnumerable<ProductCostHistory>> GetAllProductCostHistory(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.ProductId).ToListAsync();
        }

        public async Task<ProductCostHistory> GetProductCostHistoryById(int ProductId, bool trackChanges)
        {
            return await FindByCondition(c => c.ProductId.Equals(ProductId), trackChanges).SingleOrDefaultAsync();
        }

        public void Insert(ProductCostHistory productCostHistory)
        {
            Create(productCostHistory);
        }

        public void Remove(ProductCostHistory productCostHistory)
        {
            Delete(productCostHistory);
        }
    }
}
