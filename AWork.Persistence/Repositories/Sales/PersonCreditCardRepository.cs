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
    public class PersonCreditCardRepository : RepositoryBase<PersonCreditCard>, IPersonCreditCardRepository
    {
        public PersonCreditCardRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(PersonCreditCard personCreditCard)
        {
            Update(personCreditCard);
        }

        public async Task<IEnumerable<PersonCreditCard>> GetAllPersonCreditCard(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(C => C.BusinessEntityId)
                .Include(b => b.CreditCard)
                .Include(a => a.BusinessEntity)
                .ToListAsync();
        }

        public async Task<PersonCreditCard> GetByIdPersonCreditCard(int personCreditCardId, bool trackChanges)
        {
            return await FindByCondition(c => c.BusinessEntityId.Equals(personCreditCardId), trackChanges)
                .Include(b => b.CreditCardId)
                .Include(a => a.BusinessEntityId)
                .SingleOrDefaultAsync();
        }

        public void Insert(PersonCreditCard personCreditCard)
        {
            Create(personCreditCard);
        }

        public void Remove(PersonCreditCard personCreditCard)
        {
            Delete(personCreditCard);
        }
    }
}
