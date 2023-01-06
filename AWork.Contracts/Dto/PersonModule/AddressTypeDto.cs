using AWork.Domain.Models;
using System;
using System.Collections.Generic;

namespace AWork.Contracts.Dto
{
    public class AddressTypeDto
    {
        public int AddressTypeId { get; set; }
        public string Name { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual ICollection<BusinessEntityAddressDto> BusinessEntityAddresses { get; set; }
    }
}
