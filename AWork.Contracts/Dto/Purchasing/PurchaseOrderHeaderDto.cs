using AWork.Contracts.Dto.HumanResources.Employee;
using AWork.Contracts.Dto.PersonModule;
using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace AWork.Contracts.Dto.Purchasing
{
    public class PurchaseOrderHeaderDto
    {
        [DisplayName("Purchase Order ID")]
        public int PurchaseOrderId { get; set; }
        public byte RevisionNumber { get; set; }
        public byte Status { get; set; }
        public int EmployeeId { get; set; }
        public int VendorId { get; set; }
        public int ShipMethodId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal Freight { get; set; }
        public decimal TotalDue { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual EmployeeDto Employee { get; set; }
        public virtual ShipMethodDto ShipMethod { get; set; }
        public virtual VendorDto Vendor { get; set; }
        public virtual ICollection<PurchaseOrderDetailsDto> PurchaseOrderDetails { get; set; }
        public virtual List<PurchaseOrderDetailsDto> GetPurchaseOD { get; set; }
    }
}
