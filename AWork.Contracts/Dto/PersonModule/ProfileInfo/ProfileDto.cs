using AWork.Contracts.Dto;
using AWork.Domain.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.PersonModule.Profile
{
    public class ProfileDto
    { 
        public virtual PersonDto PersonDto { get; set; }

        public virtual List<EmailAddressForCreateDto> EmailAddressForCreateDtos { get; set; } = new List<EmailAddressForCreateDto>();

        public virtual List<PersonPhoneForCreateDto> PersonPhoneForCreateDto { get; set; } = new List<PersonPhoneForCreateDto>();
        public virtual PersonPhoneDto PersonPhoneDto { get; set; }
        public virtual List<PersonPhoneDto> PersonPhoneDtos { get; set; }
        public virtual PhoneNumberTypeDto PhoneNumberTypeDto { get; set; }


        public virtual BusinessEntityAddressDto BusinessEntityAddressDto { get; set; }

        public virtual ICollection<AddressDto> AddressDtos { get; set; }
        public AddressDto AddressDto { get; set; }
        public virtual AddressForCreateDto AddressForCreateDto { get; set; }
        public virtual CountryRegionDto CountryRegionDto { get; set; }



    }
}
