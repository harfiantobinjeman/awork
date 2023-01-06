using AWork.Contracts.Dto.Sales.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Sales
{
    public interface IStoreService
    {
        Task<IEnumerable<StoreDto>> GetAllAsync(bool trackChanges);

        Task<StoreDto> GetCurrencyByCode(int storeId, bool trackChanges);

        //for create hanya untuk buat store baru
        void Insert(StoreForCreateDto storeForCreateDto);
        void Remove(StoreDto storeDto);
        void Change(StoreDto storeDto);
    }
}
