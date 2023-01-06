using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AWork.Contracts.Dto
{
    public class PersonPhoneDto
    {
        
        public int BusinessEntityId { get; set; }
        public string PhoneNumber { get; set; }
        public int PhoneNumberTypeId { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Person BusinessEntity { get; set; }
        public virtual PhoneNumberType PhoneNumberType { get; set; }
    }
}
