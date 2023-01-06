using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Domain.Dto.Purchasing
{
	public class CartPurchaseItem
	{
        public int PurchaseOrderId { get; set; }
        public int PurchaseOrderDetailId { get; set; }
        public byte Status { get; set; }
        public int EmployeeId { get; set; }
        public int VendorId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
		public short OrderQty { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal LineTotal { get; set; }
		public decimal SubTotal { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal Freight { get; set; }
        public decimal TotalDue { get; set; }
        public decimal ShipBase { get; set; }
        public decimal ShipRate { get; set; }
        public int ShipMethodId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DueDate { get; set; }
    }
    public class ProjectList
    {
        public List<CartPurchaseItem> cartPurchases { get; set; }
    }
}
