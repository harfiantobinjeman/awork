using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AWork.Contracts.Dto.PersonModule
{
    public class ContactTypeDto
    {
        [Key]
        public int ContactTypeId { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<BusinessEntityContactDto> BusinessEntityContacts { get; set; }

    }
}
