using AWork.Contracts.Dto.PersonModule;
using AWork.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AWork.Contracts.Dto
{
    public class EmailAddressDto
    {
        [ForeignKey("PersonDto")]
        public int BusinessEntityId { get; set; }

        [Key]
        public int EmailAddressId { get; set; }
        public string EmailAddress1 { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual PersonDto BusinessEntity { get; set; }
    }
}
