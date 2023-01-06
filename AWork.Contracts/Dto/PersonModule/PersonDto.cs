using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AWork.Contracts.Dto.PersonModule
{
    public class PersonDto
    {
        [ForeignKey("BusinessEntityDto")]
        public int BusinessEntityId { get; set; }
        public string PersonType { get; set; }
        public bool NameStyle { get; set; }
        public string Title { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public int EmailPromotion { get; set; }
        public string AdditionalContactInfo { get; set; }
        public string Demographics { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }


        public virtual BusinessEntityDto BusinessEntity { get; set; }

        public virtual ICollection<EmailAddressDto> EmailAddresses { get; set; }
        public virtual List<EmailAddressDto> EmailAddressesDto { get; set; } = new List<EmailAddressDto>();
      


        public virtual ICollection<PersonPhoneDto> PersonPhones { get; set; }
        public virtual List<PersonPhoneDto> PersonPhonesDto { get; set; } = new List<PersonPhoneDto>();
       

        


    }
}
