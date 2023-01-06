using AWork.Contracts.Dto.PersonModule;
using AWork.Contracts.Dto.Sales.CreditCard;
using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Sales.PersonCreditCard
{
    public class PersonCreditCardDto
    {
        public int BusinessEntityId { get; set; }
        public int CreditCardId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CardNumber { get; set; }

        public byte ExpMonth { get; set; }
        public short ExpYear { get; set; }

        public virtual PersonDto BusinessEntity { get; set; }
        public virtual CreditCardDto CreditCard { get; set; }
    }
}
