using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Production
{
    public interface IProductCostHistoryRepository
    {
        //trackChanges => feature untuk mendekteksi perubahan data diobject category
        Task<IEnumerable<ProductCostHistory>> GetAllProductCostHistory(bool trackChanges);

        //craete 1 record with this code
        Task<ProductCostHistory> GetProductCostHistoryById(int ProductId, bool trackChanges);
        void Insert(ProductCostHistory productCostHistory);
        void Edit(ProductCostHistory productCostHistory);
        void Remove(ProductCostHistory productCostHistory);
    }
}
