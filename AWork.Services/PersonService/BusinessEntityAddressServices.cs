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
    public class BusinessEntityAddressServices : IBusinessEntityAddressServices
    {
        private IPersonRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public BusinessEntityAddressServices(IPersonRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<BusinessEntityAddressDto>> GetAllBusinessAddressContact(bool trackChanges)
        {
            var businessEntityAddressMdl = await _repositoryManager.BusinessEntityAddressRepository.GetAllBusinessEntityAddress(trackChanges);
            var businessEntityAddressDto = _mapper.Map<IEnumerable<BusinessEntityAddressDto>>(businessEntityAddressMdl);
            return businessEntityAddressDto;
        }

        public async Task<BusinessEntityAddressDto> GetBusinessEntityAddressById(int businessEntityAddressId,int addressID, bool trackChanges)
        {
            var businessEntityAddressMdl = await _repositoryManager.BusinessEntityAddressRepository.GetBusinessEntityAddressById(businessEntityAddressId, addressID, trackChanges);
            var businessEntityAddressDto = _mapper.Map<BusinessEntityAddressDto>(businessEntityAddressMdl);
            return businessEntityAddressDto;
        }

        public void insert(BusinessEntityAddressForCreateDto businessEntityAddressForCreateDto)
        {
            var businessEntityAddressMdl = _mapper.Map<BusinessEntityAddress>(businessEntityAddressForCreateDto);
            _repositoryManager.BusinessEntityAddressRepository.Insert(businessEntityAddressMdl);
            _repositoryManager.Save();
        }

        public void edit(BusinessEntityAddressDto businessEntityAddressDto)
        {
            
        }

        public void delete(BusinessEntityAddressDto businessEntityAddressDto)
        {
            var businessEntityAddressMdl = _mapper.Map<BusinessEntityAddress>(businessEntityAddressDto);
            _repositoryManager.BusinessEntityAddressRepository.Remove(businessEntityAddressMdl);
            _repositoryManager.SaveAsync();
        }

        public async Task<BusinessEntityAddressDto> GetBusinessEntityAddressByBusinessEntityId(int businessEntityId, bool trackChanges)
        {
            var businessEntityAddressMdl = await _repositoryManager.BusinessEntityAddressRepository.GetBusinessEntityAddressByBusinessEntityId(businessEntityId, trackChanges);
            var businessEntityAddressDto = _mapper.Map<BusinessEntityAddressDto>(businessEntityAddressMdl);
            return businessEntityAddressDto;
        }
    }
}
