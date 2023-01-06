using AWork.Contracts.Dto.Production;
using AWork.Contracts.Dto.Sales.SpecialOffer;
using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Sales.SpecialOfferProduct
{
    public class SpecialOfferProductDto
    {
        public int SpecialOfferId { get; set; }
        //public int SpecialOfferProductId { get; set; }
        public int ProductId { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Product Product { get; set; }
        public virtual SpecialOfferDto SpecialOffer { get; set; }
        //public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }

        public virtual ICollection<ProductDto> Products { get; set; }

    }
}
