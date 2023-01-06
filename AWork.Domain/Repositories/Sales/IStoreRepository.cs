using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Sales
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>> GetAllAsync(bool trackChanges);

        Task<Store> GetCurrencyByCode(int storeId, bool trackChanges);

        void Insert(Store store);
        void Remove(Store store);
        void Change(Store store);
    }
}
