using System;
using AWork.Domain.Models;

namespace AWork.Contracts.Dto
{
    public class EmailAddressForCreateDto
    {

        public int BusinessEntityId { get; set; }
        public int EmailAddressId { get; set; }
        public string EmailAddress1 { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
