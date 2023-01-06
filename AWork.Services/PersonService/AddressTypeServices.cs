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
    public class AddressTypeServices : IAddressTypeServices
    {
        private IPersonRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AddressTypeServices(IPersonRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AddressTypeDto>> GetAllAddressType(bool trackChanges)
        {
            var addresTypeMdl = await _repositoryManager.AddressTypeRepository.GetAllAddressType(trackChanges);
            var addresTypeDto = _mapper.Map<IEnumerable<AddressTypeDto>>(addresTypeMdl);
            return addresTypeDto;
        }

        public async Task<AddressTypeDto> GetAllAddressTypeById(int addressTypeId, bool trackChanges)
        {
            var addresTypeMdl = await _repositoryManager.AddressTypeRepository.GetAddressTypeId(addressTypeId, trackChanges);
            var addresTypeDto = _mapper.Map<AddressTypeDto>(addresTypeMdl);
            return addresTypeDto;
        }

        public void Insert(AddressTypeForCreateDto addressTypeForCreateDto)
        {
            var addresTypeMdl = _mapper.Map<AddressType>(addressTypeForCreateDto);
            _repositoryManager.AddressTypeRepository.Insert(addresTypeMdl);
            _repositoryManager.Save();
        }

        public void Edit(AddressTypeDto addressTypeDto)
        {
            var addresTypeMdl = _mapper.Map<AddressType>(addressTypeDto);
            _repositoryManager.AddressTypeRepository.Edit(addresTypeMdl);
            _repositoryManager.Save();
        }

        public void Remove(AddressTypeDto addressTypeDto)
        {
            var addresTypeMdl = _mapper.Map<AddressType>(addressTypeDto);
            _repositoryManager.AddressTypeRepository.Remove(addresTypeMdl);
            _repositoryManager.SaveAsync();
        }
    }
}
