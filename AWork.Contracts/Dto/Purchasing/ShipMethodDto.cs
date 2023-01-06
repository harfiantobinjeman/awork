using AWork.Contracts.Dto.Sales.SalesOrderHeader;
using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AWork.Contracts.Dto.Purchasing
{
    public class ShipMethodDto
    {
        public ShipMethodDto()
        {
            PurchaseOrderHeaders = new HashSet<PurchaseOrderHeaderDto>();
            SalesOrderHeaders = new HashSet<SalesOrderHeaderDto>();
        }
        public int ShipMethodId { get; set; }
        [Display(Name = "ShipMethod Name")]
        public string Name { get; set; }
        public decimal ShipBase { get; set; }
        public decimal ShipRate { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
        
        public virtual ICollection<PurchaseOrderHeaderDto> PurchaseOrderHeaders { get; set; }
        public virtual List<PurchaseOrderHeaderDto> GetPurchaseOrderHeaders { get; set; }
        public virtual ICollection<SalesOrderHeaderDto> SalesOrderHeaders { get; set; }
    }
}
