using AWork.Contracts.Dto.Sales.SalesTerritory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Sales
{
    public interface ISalesTerritoryService
    {
        Task<IEnumerable<SalesTerritoryDto>> GetAllSalesTerritory(bool trackChanges);
        Task<SalesTerritoryDto> GetSalesTerritoryById(int territoryId, bool trackChanges);

        void Insert(SalesTerritoryForCreateDto salesTerritoryForCreateDto);

        void Edit(SalesTerritoryDto salesTerritoryDto);

        void Remove(SalesTerritoryDto salesTerritoryDto);
    }
}
