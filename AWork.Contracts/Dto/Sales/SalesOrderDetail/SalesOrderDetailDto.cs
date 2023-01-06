using AWork.Contracts.Dto.Sales.SpecialOfferProduct;
using AWork.Contracts.Dto.Sales.SalesOrderHeader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Sales.SalesOrderDetail
{
    public class SalesOrderDetailDto
    {
        public int SalesOrderId { get; set; }
        public int SalesOrderDetailId { get; set; }
        public string CarrierTrackingNumber { get; set; }
        public short OrderQty { get; set; }
        public int ProductId { get; set; }
        public int SpecialOfferId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitPriceDiscount { get; set; }
        public decimal LineTotal { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual SalesOrderHeaderDto SalesOrder { get; set; }
        public virtual SpecialOfferProductDto SpecialOfferProduct { get; set; }
    }
}
