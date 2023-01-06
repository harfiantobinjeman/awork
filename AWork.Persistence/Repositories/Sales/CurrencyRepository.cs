using AWork.Domain.Models;
using AWork.Domain.Repositories.Sales;
using AWork.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.Sales
{
    public class CurrencyRepository : RepositoryBase<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Currency>> GetAllAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.CurrencyCode).ToListAsync();
        }

        public async Task<Currency> GetCurrencyByCode(string currencyCode, bool trackChanges)
        {
            return await FindByCondition(c => c.CurrencyCode.Equals(currencyCode), trackChanges).SingleOrDefaultAsync();
        }

        public void Insert(Currency currency)
        {
            Create(currency);
        }

        public void Remove(Currency currency)
        {
            Delete(currency);
        }
        public void Change(Currency currency)
        {
            Update(currency);
        }
    }
}
