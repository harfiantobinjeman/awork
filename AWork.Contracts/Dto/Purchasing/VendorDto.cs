using AWork.Contracts.Dto.PersonModule;
using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AWork.Contracts.Dto.Purchasing
{
    public class VendorDto
    {
       /* public VendorDto()
        {
            ProductVendors = new HashSet<ProductVendor>();
            PurchaseOrderHeaders = new HashSet<PurchaseOrderHeader>();
        }*/

        public int BusinessEntityId { get; set; }
        public string AccountNumber { get; set; }
        [Display(Name = "Vendor Name")]
        public string Name { get; set; }
        public byte CreditRating { get; set; }
        public bool? PreferredVendorStatus { get; set; }
        public bool? ActiveFlag { get; set; }
        public string PurchasingWebServiceUrl { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual BusinessEntityDto BusinessEntity { get; set; }
        public virtual ICollection<ProductVendorDto> ProductVendors { get; set; }
        public virtual ICollection<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }


        public virtual List<ProductVendorDto> ProductVendorDto { get; set; }
        public virtual List<ProductVendorForCreateDto> ProductVendorForCreateDtos { get; set; } = new List<ProductVendorForCreateDto>();
    }
}
