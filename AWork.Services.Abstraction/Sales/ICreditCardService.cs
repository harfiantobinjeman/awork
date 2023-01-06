using AWork.Contracts.Dto.Sales.CreditCard;
using AWork.Contracts.Dto.Sales.SalesOrderHeader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Sales
{
    public interface ICreditCardService
    {
        Task<IEnumerable<CreditCardDto>> GetAllCreditCard(bool trackChanges);
        Task<CreditCardDto> GetAllCreditCardById(int CreditCardId, bool trackChanges);
        Task<IEnumerable<CreditCardDto>> GetCreditCardPage(int pageIndex, int pageSize, bool trackChanges);


        void Insert(CreditCardForCreateDto creditCardForCreateDto);

        void Edit(CreditCardDto creditCardDto);

        void Remove(CreditCardDto creditCardDto);
    }
}
