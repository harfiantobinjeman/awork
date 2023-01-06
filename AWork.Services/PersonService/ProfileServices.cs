using AutoMapper;
using AWork.Contracts.Dto;
using AWork.Contracts.Dto.PersonModule;
using AWork.Contracts.Dto.PersonModule.Profile;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction.PersonModul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.PersonService
{
    internal class ProfileServices : IProfileServices
    {
        private IPersonRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ProfileServices(IPersonRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<ProfileDto> GetProfileById(int bussinessEntity, bool trackChanges)
        {
            var businessEntityMdl = await _repositoryManager.BusinessEntityRepository.GetBusinessEntityById(bussinessEntity, trackChanges);
            var businessEntityDto = _mapper.Map<ProfileDto>(businessEntityMdl);
            return businessEntityDto;
        }

        public void ProfileEdit(PersonDto personDto, List<EmailAddressDto> emailAddressDto, List<PersonPhoneDto> personPhoneDto, AddressDto addressDto)
        {
            var profileMdl = _mapper.Map<Person>(personDto);
            _repositoryManager.PersonRepository.Edit(profileMdl);
            _repositoryManager.Save();

            foreach (var item in emailAddressDto)
            {
                var email = _mapper.Map<EmailAddress>(item);
                _repositoryManager.EmailAddressRepository.Edit(email);
            }
            _repositoryManager.Save();

            foreach (var item in personPhoneDto)
            {
                var personPhone = _mapper.Map<PersonPhone>(item);
                _repositoryManager.PersonPhoneRepository.Edit(personPhone);

            }
            _repositoryManager.Save();

            var addressMdl = _mapper.Map<Address>(addressDto);
            _repositoryManager.AddressRepository.Edit(addressMdl);
            _repositoryManager.Save();
        }
    }
}
