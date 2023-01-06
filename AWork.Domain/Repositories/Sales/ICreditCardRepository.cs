using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Sales
{
    public interface ICreditCardRepository
    {
        Task<IEnumerable<CreditCard>> GetAllCreditCard(bool trackChanges);

        Task<CreditCard> GetCreditCardById(int CreditCardId, bool trackChanges);
        Task<IEnumerable<CreditCard>> GetCreditCardPage(int pageIndex, int pageSize, bool trackChanges);


        void Insert(CreditCard creditCard);
        void Remove(CreditCard creditCard);
        void Change(CreditCard creditCard);


    }
}
