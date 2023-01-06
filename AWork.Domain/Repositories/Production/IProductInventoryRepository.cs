using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Production
{
    public interface IProductInventoryRepository
    {
        Task<IEnumerable<ProductInventory>> GetAllInventory(bool trackChange);
        Task<ProductInventory>GetInventoryById(int inventoryId, int locationId, bool trackChange);

        void Insert(ProductInventory productInventory);
        void Edit (ProductInventory productInventory);
        void Remove (ProductInventory productInventory);
    }
}
