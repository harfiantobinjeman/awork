using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Sales.PersonCreditCard
{
    public class PersonCreditCardForCreateDto
    {
        public int BusinessEntityId { get; set; }
        public int CreditCardId { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
