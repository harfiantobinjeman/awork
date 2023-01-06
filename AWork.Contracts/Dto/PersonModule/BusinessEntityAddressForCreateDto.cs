using System;

namespace AWork.Contracts.Dto
{
    public class BusinessEntityAddressForCreateDto
    {
        public int BusinessEntityId { get; set; }
        public int AddressId { get; set; }
        public int AddressTypeId { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
