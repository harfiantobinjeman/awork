using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Sales
{
    public interface IPersonCreditCardRepository
    {
        Task<IEnumerable<PersonCreditCard>> GetAllPersonCreditCard(bool trackChanges);

        Task<PersonCreditCard> GetByIdPersonCreditCard(int personCreditCardId, bool trackChanges);

        void Insert(PersonCreditCard personCreditCard);
        void Remove(PersonCreditCard personCreditCard);
        void Edit(PersonCreditCard personCreditCard);

    }
}
