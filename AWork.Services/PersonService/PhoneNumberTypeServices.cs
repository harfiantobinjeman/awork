using AutoMapper;
using AWork.Contracts.Dto;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction.PersonModul;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.PersonService
{
    public class PhoneNumberTypeServices : IPhoneNumberTypeServices
    {
        private IPersonRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public Task<PhoneNumberTypeDto> GetPhoneNumberTypeById(int phoneNumberTypeId, bool trackChanges)
        {
            throw new NotImplementedException();
        }
        public PhoneNumberTypeServices(IPersonRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PhoneNumberTypeDto>> GetAllPhoneNumberType(bool trackChanges)
        {
            var phoneNumberTypeMdl = await _repositoryManager.PhoneNumberTypeRepository.GetAllPhoneNumberType(trackChanges);
            var phoneNumberTypeDto = _mapper.Map<IEnumerable<PhoneNumberTypeDto>>(phoneNumberTypeMdl);
            return phoneNumberTypeDto;
        }

        public async Task<PhoneNumberTypeDto> GetAllPhoneNumberTypeById(int phoneNumberTypeId, bool trackChanges)
        {
            var phoneNumberTypeMdl = await _repositoryManager.PhoneNumberTypeRepository.GetPhoneNumberTypeById(phoneNumberTypeId, trackChanges);
            var phoneNumberTypeDto = _mapper.Map<PhoneNumberTypeDto>(phoneNumberTypeMdl);
            return phoneNumberTypeDto;
        }

        public void Insert(PhoneNumberTypeForCreateDto phoneNumberTypeForCreate)
        {
            var phoneNumberTypeMdl = _mapper.Map<PhoneNumberType>(phoneNumberTypeForCreate);
            _repositoryManager.PhoneNumberTypeRepository.Insert(phoneNumberTypeMdl);
            _repositoryManager.Save();
        }

        public void Edit(PhoneNumberTypeDto phoneNumberTypeDto)
        {
            throw new NotImplementedException();
        }

        public void Remove(PhoneNumberTypeDto phoneNumberTypeDto)
        {
            var phoneNumberTypeMdl = _mapper.Map<PhoneNumberType>(phoneNumberTypeDto);
            _repositoryManager.PhoneNumberTypeRepository.Remove(phoneNumberTypeMdl);
            _repositoryManager.SaveAsync();
        }

        
    }
}
