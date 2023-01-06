using AWork.Contracts.Dto.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Production
{
    public interface IProductInventoryService
    {
        Task<IEnumerable<ProductInventoryDto>> GetAllInventory(bool trackChange);
        Task<ProductInventoryDto> GetInventoryById(int id, int locationId, bool trackChange);

        void Insert(ProductInventoryDtoForCreate inventoryDtoForCreate);
        void Edit(ProductInventoryDto productInventoryDto);
        void Remove(ProductInventoryDto productInventoryDto);
    }
}
