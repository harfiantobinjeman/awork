using AWork.Contracts.Dto.PersonModule;
using AWork.Domain.Models;
using System;

namespace AWork.Contracts.Dto
{
    public class BusinessEntityAddressDto
    {
        public int BusinessEntityId { get; set; }
        public int AddressId { get; set; }
        public int AddressTypeId { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual AddressDto Address { get; set; }
        public virtual AddressTypeDto AddressType { get; set; }
        public virtual BusinessEntityDto BusinessEntity { get; set; }


    }
}
