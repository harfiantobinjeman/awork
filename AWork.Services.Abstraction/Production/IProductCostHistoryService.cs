using AWork.Contracts.Dto.Production;
using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Production
{
    public interface IProductCostHistoryService
    {
        //trackChanges => feature untuk mendekteksi perubahan data diobject category
        Task<IEnumerable<ProductCostHistoryDto>> GetAllProductCostHistory(bool trackChanges);

        //craete 1 record with this code
        Task<ProductCostHistoryDto> GetProductCostHistoryById(int ProductId, bool trackChanges);
        void Insert(ProductCostHistoryForCreateDto productCostHistoryForCreateDto);
        void Edit(ProductCostHistoryDto productCostHistoryDto);
        void Remove(ProductCostHistoryDto productCostHistoryDto);
    }
}
