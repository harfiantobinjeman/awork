using AutoMapper;
using AWork.Contracts.Dto;
using AWork.Contracts.Dto.PersonModule;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction.PersonModul;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.PersonService
{
    public class AddressServices : IAddressServices
    {
        private IPersonRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AddressServices(IPersonRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void Edit(AddressDto addressDto)
        {
            var addressMdl = _mapper.Map<Address>(addressDto);
            _repositoryManager.AddressRepository.Edit(addressMdl);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<AddressDto>> GetAllAddress(bool trackChanges)
        {
            var addressMdl = await _repositoryManager.AddressRepository.GetAllAdress(trackChanges);
            var addressDto = _mapper.Map<IEnumerable<AddressDto>>(addressMdl);
            return addressDto;
        }

        public async Task<AddressDto> GetAllAddressById(int addressId, bool trackChanges)
        {
            var addressMdl = await _repositoryManager.AddressRepository.GetAllAddressById(addressId, trackChanges);
            var addressDto = _mapper.Map<AddressDto>(addressMdl);
            return addressDto;
        }

        public void Insert(AddressForCreateDto addressForCreateDto, BusinessEntityAddressForCreateDto businessEntityAddressForCreateDto)
        {
            var addressMdl = _mapper.Map<Address>(addressForCreateDto);
            _repositoryManager.AddressRepository.Insert(addressMdl);
            _repositoryManager.Save();

            var businessEntityAddressMdl = _mapper.Map<BusinessEntityAddress>(businessEntityAddressForCreateDto);
            businessEntityAddressMdl.AddressId = addressMdl.AddressId;
            _repositoryManager.BusinessEntityAddressRepository.Insert(businessEntityAddressMdl);
            _repositoryManager.Save();
        }

        public void Remove(AddressDto addressDto)
        {
            var addressMdl = _mapper.Map<Address>(addressDto);
            _repositoryManager.AddressRepository.Remove(addressMdl);
            _repositoryManager.Save();
        }
    }
}
