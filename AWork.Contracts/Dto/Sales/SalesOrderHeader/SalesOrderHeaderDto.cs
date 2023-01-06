using AWork.Contracts.Dto.Sales.CreditCard;
using AWork.Contracts.Dto.Sales.SalesOrderDetail;
using AWork.Contracts.Dto.Sales.SalesPerson;
using AWork.Contracts.Dto.Sales.SalesTerritory;
using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Sales.SalesOrderHeader
{
    public class SalesOrderHeaderDto
    {
        public int SalesOrderId { get; set; }
        public byte RevisionNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public byte Status { get; set; }
        public bool? OnlineOrderFlag { get; set; }
        public string SalesOrderNumber { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string AccountNumber { get; set; }
        public int CustomerId { get; set; }
        public int? SalesPersonId { get; set; }
        public int? TerritoryId { get; set; }
        public int BillToAddressId { get; set; }
        public int ShipToAddressId { get; set; }
        public int ShipMethodId { get; set; }
        public int? CreditCardId { get; set; }
        public string CreditCardApprovalCode { get; set; }
        public int? CurrencyRateId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal Freight { get; set; }
        public decimal TotalDue { get; set; }
        public string Comment { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Address BillToAddress { get; set; }
        public virtual CreditCardDto CreditCard { get; set; }
        public virtual CurrencyRate CurrencyRate { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual SalesPersonDto SalesPerson { get; set; }
        public virtual ShipMethod ShipMethod { get; set; }
        public virtual Address ShipToAddress { get; set; }
        public virtual SalesTerritoryDto Territory { get; set; }
        public virtual ICollection<SalesOrderDetailDto> SalesOrderDetails { get; set; }
        public virtual ICollection<SalesOrderHeaderSalesReason> SalesOrderHeaderSalesReasons { get; set; }
    }
}
