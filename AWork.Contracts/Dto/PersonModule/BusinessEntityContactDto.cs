using AWork.Contracts.Dto.PersonModule;
using AWork.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AWork.Contracts.Dto
{
    public class BusinessEntityContactDto
    {
        [ForeignKey("BusinessEntityDto")]
        public int BusinessEntityId { get; set; }

        [Key]
        public int PersonId { get; set; }

        [ForeignKey("ContactTypeDto")]
        public int ContactTypeId { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual BusinessEntityDto BusinessEntity { get; set; }
        public virtual ContactTypeDto ContactType { get; set; }
        public virtual PersonDto Person { get; set; }

    }
}
