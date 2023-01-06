using AWork.Contracts.Dto.Sales.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Sales
{
    public interface ICurrencyService
    {
        Task<IEnumerable<CurrencyDto>> GetAllCurrency(bool trackChanges);
        Task<CurrencyDto> GetCurrencyByCode(string CurrencyCode , bool trackChanges);

        void Insert(CurrencyForCreateDto currencyForCreateDto);

        void Edit(CurrencyDto currencyDto);

        void Remove(CurrencyDto currencyDto);
    }
}
