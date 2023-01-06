using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Sales
{
    public interface ICurrencyRepository
    {
        Task<IEnumerable<Currency>> GetAllAsync(bool trackChanges);

        Task<Currency> GetCurrencyByCode(string currencyCode, bool trackChanges);

        void Insert(Currency currency);
        void Remove(Currency currency);
        void Change(Currency currency);
    }
}
