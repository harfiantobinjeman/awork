using AWork.Contracts.Dto.Sales.PersonCreditCard;
using AWork.Contracts.Dto.Sales.SalesOrderHeader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Sales
{
    public interface IPersonCreditCardService
    {
        Task<IEnumerable<PersonCreditCardDto>> GetAllPersonCreditCards(bool trackChanges);
        Task<PersonCreditCardDto> GetPersonCreditCardById(int bussinessEntityId, bool trackChanges);

        void Insert(PersonCreditCardForCreateDto personCreditCardForCreateDto);

        void Edit(PersonCreditCardDto personCreditCardDto);

        void Remove(PersonCreditCardDto personCreditCardDto);
    }
}
