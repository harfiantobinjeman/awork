using AWork.Contracts.Dto.Sales.SpecialOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Sales.SpecialOfferProduct
{
    public class SpecialOfferProductForCreateDto
    {
        public int ProductId { get; set; }

        public virtual SpecialOfferDto SpecialOfferDto { get; set; }
    }
}
