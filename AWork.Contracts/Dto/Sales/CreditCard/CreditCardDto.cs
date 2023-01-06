using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Sales.CreditCard
{
    public class CreditCardDto
    {
        
        public int CreditCardId { get; set; }

        [Required(ErrorMessage = "CreditType Is Required")]
        public string CardType { get; set; }

        [Required(ErrorMessage = "CardNumber Is Required")]
        [StringLength(50,ErrorMessage="CardNumber Cannot be longer than 50")]
        public string CardNumber { get; set; }
        
        [Required(ErrorMessage = "ExpMounth Is Required")]
        public byte ExpMonth { get; set; }
        
        [Required(ErrorMessage = "ExpYear Is Required")]
        public short ExpYear { get; set; }
        public DateTime ModifiedDate { get; set; }

        /*public virtual ICollection<PersonCreditCard> PersonCreditCards { get; set; }
        public virtual ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; }*/
    }
}
