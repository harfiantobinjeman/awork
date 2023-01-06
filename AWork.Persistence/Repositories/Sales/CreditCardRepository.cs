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
    public class CreditCardRepository : RepositoryBase<CreditCard>, ICreditCardRepository
    {
        public CreditCardRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Change(CreditCard creditCard)
        {
            Update(creditCard);
        }


        public async Task<IEnumerable<CreditCard>> GetAllCreditCard(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.CreditCardId).ToListAsync();
        }



        public async Task<CreditCard> GetCreditCardById(int CreditCardId, bool trackChanges)
        {
            return await FindByCondition(c => c.CreditCardId.Equals(CreditCardId), trackChanges)
               
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CreditCard>> GetCreditCardPage(int pageIndex, int pageSize, bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(p => p.CreditCardId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public void Insert(CreditCard creditCard)
        {
            Create(creditCard);
        }

        public void Remove(CreditCard creditCard)
        {
            Delete(creditCard);
        }
    }
}
