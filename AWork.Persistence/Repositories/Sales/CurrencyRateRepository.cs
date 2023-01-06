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
    public class CurrencyRateRepository : RepositoryBase<CurrencyRate>, ICurrencyRateRepository
    {
        public CurrencyRateRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<CurrencyRate>> GetAllAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.CurrencyRateId).ToListAsync();
        }

        public async Task<CurrencyRate> GetByIdAsync(int currencyRateId, bool trackChanges)
        {
            return await FindByCondition(c => c.CurrencyRateId.Equals(currencyRateId), trackChanges).SingleOrDefaultAsync();
        }

        public void Insert(CurrencyRate currencyRate)
        {
            Create(currencyRate);
        }

        public void Remove(CurrencyRate currencyRate)
        {
            Delete(currencyRate);
        }
        public void Change(CurrencyRate currencyRate)
        {
            Update(currencyRate);
        }
    }
}
