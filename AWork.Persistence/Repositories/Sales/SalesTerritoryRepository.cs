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
    internal class SalesTerritoryRepository : RepositoryBase<SalesTerritory>, ISalesTerritoryRepository
    {
        public SalesTerritoryRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(SalesTerritory salesTeritory)
        {
            Update(salesTeritory);
        }

        public async Task<IEnumerable<SalesTerritory>> GetAllSalesTerritory(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(t => t.TerritoryId)
                .Include(c => c.CountryRegionCodeNavigation)
                .ToListAsync();
        }

        public async Task<SalesTerritory> GetSalesTerritoryById(int territoryId, bool trackChanges)
        {
            return await FindByCondition(t => t.TerritoryId.Equals(territoryId), trackChanges)
                .Include(c => c.CountryRegionCodeNavigation)
                .SingleOrDefaultAsync();
        }

        public void Insert(SalesTerritory salesTeritory)
        {
            Create(salesTeritory);
        }

        public void Remove(SalesTerritory salesTeritory)
        {
            Delete(salesTeritory);
        }
    }
}
