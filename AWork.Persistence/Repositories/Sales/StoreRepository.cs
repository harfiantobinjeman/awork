using AWork.Domain.Models;
using AWork.Domain.Repositories.Sales;
using AWork.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.Sales
{
    public class StoreRepository : RepositoryBase<Store>, IStoreRepository
    {
        public StoreRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Store>> GetAllAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.BusinessEntityId)
                .Include(e=>e.BusinessEntity)
                .ToListAsync();
        }

        public async Task<Store> GetCurrencyByCode(int storeId, bool trackChanges)
        {
            return await FindByCondition(c => c.BusinessEntityId.Equals(storeId), trackChanges)
                .Include(e=>e.BusinessEntity)
                .SingleOrDefaultAsync();
        }

        public void Insert(Store store)
        {
            Create(store);
        }

        public void Remove(Store store)
        {
            Delete(store);
        }
        public void Change(Store store)
        {
            Update(store);
        }
    }
}
