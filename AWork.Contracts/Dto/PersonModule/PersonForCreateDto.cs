using AWork.Contracts.Dto.HumanResources.Employee;
using AWork.Contracts.Dto.Sales.PersonCreditCard;
using AWork.Contracts.Dto.Sales.SalesCustomer;
using AWork.Domain.Models;
using System;
using System.Collections.Generic;

namespace AWork.Contracts.Dto.PersonModule
{
    public class PersonForCreateDto
    {
        public int BusinessEntityId { get; set; }
        public string PersonType { get; set; }
        public bool NameStyle { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public int EmailPromotion { get; set; }
        public string AdditionalContactInfo { get; set; }
        public string Demographics { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
/*
        public virtual EmailAddressDto EmailAddress { get; set; }

        public virtual PasswordDto Password { get; set; }
        public virtual ICollection<BusinessEntityContactDto> BusinessEntityContacts { get; set; }
        public virtual List<EmailAddressForCreateDto> EmailAddressForCreateDtos { get; set; } = new List<EmailAddressForCreateDto>();
        public virtual ICollection<CustomerDto> Customers { get; set; }
        public virtual ICollection<EmailAddressDto> EmailAddresses { get; set; }
        public virtual ICollection<PersonCreditCardDto> PersonCreditCards { get; set; }
        public virtual ICollection<PersonPhoneDto> PersonPhones { get; set; }*/
    }
}
