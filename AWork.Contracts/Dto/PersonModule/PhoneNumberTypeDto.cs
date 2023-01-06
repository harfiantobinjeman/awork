using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AWork.Contracts.Dto
{
    public class PhoneNumberTypeDto
    {
        [Key]
        public int PhoneNumberTypeId { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<PersonPhoneDto> PersonPhones { get; set; }
    }
}
