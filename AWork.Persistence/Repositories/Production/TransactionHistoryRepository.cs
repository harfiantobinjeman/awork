using AWork.Domain.Models;
using AWork.Domain.Repositories.Production;
using AWork.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.Production
{
    internal class TransactionHistoryRepository : RepositoryBase<TransactionHistory>, ITransactionHistoryRepository
    {
        public TransactionHistoryRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(TransactionHistory transactionHistory)
        {
            //throw new NotImplementedException();
            Update(transactionHistory);
        }

        public async Task<IEnumerable<TransactionHistory>> GetAllTransaction(bool trackChange)
        {
            //throw new NotImplementedException();
            return await FindAll(trackChange).OrderBy(c => c.TransactionId)
                .Include(d => d.Product)
                .ToListAsync();

        }

        public async Task<TransactionHistory> GetTransactionHistoryById(int transaktionId, bool trackChange)
        {
            //throw new NotImplementedException();
            //return await FindByCondition(c => c.LocationId.Equals(locationId), trackChange).SingleOrDefaultAsync();
            return await FindByCondition(c => c.TransactionId.Equals(transaktionId), trackChange)
                .Include(d => d.Product)
                .SingleOrDefaultAsync();
        }

        public void Insert(TransactionHistory transactionHistory)
        {
            //throw new NotImplementedException();
            Create(transactionHistory);
        }

        public void Remove(TransactionHistory transactionHistory)
        {
            //throw new NotImplementedException();
            Delete(transactionHistory);
        }
    }
}
