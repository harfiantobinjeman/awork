using AWork.Contracts.Dto.Purchasing;
using AWork.Contracts.Dto.Sales.Store;
using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AWork.Contracts.Dto.PersonModule
{
    public class BusinessEntityDto
    {
        [Key]
        public int BusinessEntityId { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual PersonDto Person { get; set; }
        public virtual StoreDto Store { get; set; }
        public virtual VendorDto Vendor { get; set; }
        public virtual ICollection<BusinessEntityAddressDto> BusinessEntityAddresses { get; set; }
        public virtual ICollection<BusinessEntityContactDto> BusinessEntityContacts { get; set; }
    }
}
