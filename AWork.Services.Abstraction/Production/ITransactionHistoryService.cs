using AWork.Contracts.Dto.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Production
{
    public interface ITransactionHistoryService
    {
        Task<IEnumerable<TransactionHistoryDto>> GetAllTransaction(bool trackChange);
        Task<TransactionHistoryDto> GetTransactionHistoryById(int transactioId, bool trackChange);
        TransactionHistoryDto EditTransaction();
        void Insert (TransactionHistoryForCreateDto transactionHistoryForCreateDto);
        void Edit (TransactionHistoryDto transactionHistoryDto);
        void Remove (TransactionHistoryDto transactionHistoryDto);
    }
}
