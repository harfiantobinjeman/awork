using AWork.Contracts.Dto.Production;
using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Purchasing
{
    public class PurchaseOrderDetailsForCreateDto
    {
        public int PurchaseOrderId { get; set; }
        public int PurchaseOrderDetailId { get; set; }
        public DateTime DueDate { get; set; }
        public short OrderQty { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
        public decimal ReceivedQty { get; set; }
        public decimal RejectedQty { get; set; }
        public decimal StockedQty { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ProductDto Product { get; set; }
        public virtual PurchaseOrderHeaderForCreateDto PurchaseOrder { get; set; }
    }
}
