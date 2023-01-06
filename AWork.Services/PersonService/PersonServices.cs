using AutoMapper;
using AWork.Contracts.Dto;
using AWork.Contracts.Dto.Authentication;
using AWork.Contracts.Dto.PersonModule;
using AWork.Contracts.Dto.PersonModule.Profile;
using AWork.Contracts.Dto.Production;
using AWork.Contracts.Dto.Sales.SalesPerson;
using AWork.Contracts.Dto.Sales.Store;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction.PersonModul;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.PersonService
{
    public class PersonServices : IPersonServices
    {
        private IPersonRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public PersonServices(IPersonRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void Delete(PersonDto personDto)
        {
            var personMdl = _mapper.Map<Person>(personDto);
            _repositoryManager.PersonRepository.Remove(personMdl);
            _repositoryManager.SaveAsync();
        }

        public void Edit(PersonDto personDto)
        {
            var personMdl = _mapper.Map<Person>(personDto);
            _repositoryManager.PersonRepository.Edit(personMdl);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<PersonDto>> GetAllPerson(bool trackChanges)
        {
            var personMdl = await _repositoryManager.PersonRepository.GetAllPerson(trackChanges);
            var personDto = _mapper.Map<IEnumerable<PersonDto>>(personMdl);
            return personDto;
        }

        public async Task<PersonDto> GetAllPersonById(int personId, bool trackChanges)
        {
            var personMdl = await _repositoryManager.PersonRepository.GetPersonById(personId, trackChanges);
            var personDto = _mapper.Map<PersonDto>(personMdl);
            return personDto;
        }
        public void Insert(PersonForCreateDto personForCreateDto)
        {
            //reverse from dto to mdl                                   
            var personMdl = _mapper.Map<Person>(personForCreateDto);
            _repositoryManager.PersonRepository.Insert(personMdl);
            _repositoryManager.Save();

        }

        public async Task<IEnumerable<PersonDto>> GetPersonPage(int pageIndex, int pageSize, bool trackChanges)
        {
            var personMdl = await _repositoryManager.PersonRepository.GetAllPersonPage(pageIndex, pageSize, trackChanges);
            var personDto = _mapper.Map<IEnumerable<PersonDto>>(personMdl);
            return personDto;
        }


        public async Task<PersonDto> GetPerson(string firstName, string LastName, bool trackChanges)
        {
            var personMdl = await _repositoryManager.PersonRepository.GetPerson(firstName, LastName, trackChanges);
            var personDto = _mapper.Map<PersonDto>(personMdl);
            return personDto;
        }

        public async Task<PersonDto> CreatePerson(PersonForCreateDto personForCreateDto)
        {

            var personMdl =  _mapper.Map<Person>(personForCreateDto);
            _repositoryManager.PersonRepository.Insert(personMdl);
            _repositoryManager.Save();

            //reverse from entity to dto
            var personDto = _mapper.Map<PersonDto>(personMdl);
            return personDto;
        }

        public async Task<ProfileDto> GetProfileById(int businessEntityId, bool trackChanges)
        {
            var personMdl = await _repositoryManager.PersonRepository.GetPersonById(businessEntityId, trackChanges);
            var personDto = _mapper.Map<ProfileDto>(personMdl);
            return personDto;
        }

        public void personProfileEdit(PersonDto personDto, List<EmailAddressForCreateDto> emailAddressForCreateDtos,
            List<PersonPhoneForCreateDto> personPhoneForCreateDtos, AddressForCreateDto addressForCreateDto)
        {
            var profileMdl = _mapper.Map<Person>(personDto);
            _repositoryManager.PersonRepository.Edit(profileMdl);
            _repositoryManager.Save();

            foreach (var item in emailAddressForCreateDtos)
            {
                var email = _mapper.Map<EmailAddress>(item);
                _repositoryManager.EmailAddressRepository.Edit(email);
            }
            _repositoryManager.Save();

            foreach (var item in personPhoneForCreateDtos)
            {
                var phoneNumber = _mapper.Map<PersonPhone>(item);
                _repositoryManager.PersonPhoneRepository.Edit(phoneNumber);
            }
            _repositoryManager.Save();

            var addressMdl = _mapper.Map<Address>(addressForCreateDto);
            _repositoryManager.AddressRepository.Insert(addressMdl);
            _repositoryManager.Save();

        }
    }
}