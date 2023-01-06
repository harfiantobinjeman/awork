using AWork.Contracts.Dto.HumanResources.Employee;
using AWork.Domain.Models;
using System;
using System.Collections.Generic;

namespace AWork.Contracts.Dto.Purchasing
{
    public class PurchaseOrderHeaderForCreateDto
    {
        public int PurchaseOrderId { get; set; }
        public byte RevisionNumber { get; set; }
        public byte Status { get; set; }
        public int EmployeeId { get; set; }
        public int VendorId { get; set; }
        public int ShipMethodId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal Freight { get; set; }
        public decimal TotalDue { get; set; }
        public virtual EmployeeDto Employee { get; set; }
        public virtual ShipMethodDto ShipMethod { get; set; }
        public virtual VendorDto Vendor { get; set; }
        public virtual ICollection<PurchaseOrderDetailsDto> PurchaseOrderDetails { get; set; }
    }
}
