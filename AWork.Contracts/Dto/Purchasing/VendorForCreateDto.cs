using AWork.Contracts.Dto.PersonModule;
using AWork.Domain.Models;
using System;
using System.Collections.Generic;

namespace AWork.Contracts.Dto.Purchasing
{
    public class VendorForCreateDto
    {
        public int BusinessEntityId { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public byte CreditRating { get; set; }
        public bool? PreferredVendorStatus { get; set; }
        public bool? ActiveFlag { get; set; }
        public string PurchasingWebServiceUrl { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual BusinessEntityDto BusinessEntity { get; set; }
        public virtual ICollection<ProductVendor> ProductVendors { get; set; }
        public virtual ICollection<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }


        public virtual List<ProductVendorForCreateDto> ProductVendorForCreateDtos { get; set; } = new List<ProductVendorForCreateDto>();
    }
}
