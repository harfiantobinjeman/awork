using AutoMapper;
using AWork.Contracts.Dto;
using AWork.Contracts.Dto.PersonModule;
using AWork.Contracts.Dto.PersonModule.Profile;
using AWork.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.PersonModul
{
    public interface IProfileServices
    {

        public Task<ProfileDto> GetProfileById(int bussinessEntity, bool trackChanges);

        public void ProfileEdit(PersonDto personDto, List<EmailAddressDto> emailAddressDto, List<PersonPhoneDto> personPhoneDto, AddressDto addressDto);

    }
}
