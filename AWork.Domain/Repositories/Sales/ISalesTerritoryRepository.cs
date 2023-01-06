using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Sales
{
    public interface ISalesTerritoryRepository
    {
        Task<IEnumerable<SalesTerritory>> GetAllSalesTerritory(bool trackChanges);

        Task<SalesTerritory> GetSalesTerritoryById(int territoryId, bool trackChanges);

        void Insert(SalesTerritory salesTeritory);

        void Edit(SalesTerritory salesTeritory);

        void Remove(SalesTerritory salesTeritory);
    }
}
