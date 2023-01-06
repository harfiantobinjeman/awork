using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Production
{
    public interface ITransactionHistoryRepository
    {
        Task<IEnumerable<TransactionHistory>> GetAllTransaction(bool trackChange);
        Task<TransactionHistory>GetTransactionHistoryById(int transaktionId, bool trackChange);

        void Insert (TransactionHistory transactionHistory);
        void Edit (TransactionHistory transactionHistory);
        void Remove (TransactionHistory transactionHistory);
    }
}
