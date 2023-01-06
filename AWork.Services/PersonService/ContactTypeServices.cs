using AutoMapper;
using AWork.Contracts.Dto.PersonModule;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction.PersonModul;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.PersonService
{
    public class ContactTypeServices : IContactTypeServices
    {
        private IPersonRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ContactTypeServices(IPersonRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void Edit(ContactTypeDto contactTypeDto)
        {
            var contactTypeMdl = _mapper.Map<ContactType>(contactTypeDto);
            _repositoryManager.ContactTypeRepository.Edit(contactTypeMdl);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<ContactTypeDto>> GetAllContactType(bool trackChanges)
        {
            var contactTypesMdl = await _repositoryManager.ContactTypeRepository.GetAllContactType(trackChanges);
            var contactTypesDto = _mapper.Map<IEnumerable<ContactTypeDto>>(contactTypesMdl);
            return contactTypesDto;
        }

        public async Task<ContactTypeDto> GetAllContactTypeById(int contactId, bool trackChanges)
        {
            var contactTypeMdl = await _repositoryManager.ContactTypeRepository.GetAllContactTypeById(contactId, trackChanges);
            var contactTypeDto = _mapper.Map<ContactTypeDto>(contactTypeMdl);
            return contactTypeDto;
        }

        public void Insert(ContactTypeForCreateDto contactTypeForCreateDto)
        {
            var contactTypeMdl = _mapper.Map<ContactType>(contactTypeForCreateDto);
            _repositoryManager.ContactTypeRepository.Insert(contactTypeMdl);
            _repositoryManager.Save();
        }

        public void Remove(ContactTypeDto contactTypeDto)
        {
            var contactTypeMdl = _mapper.Map<ContactType>(contactTypeDto);
            _repositoryManager.ContactTypeRepository.Remove(contactTypeMdl);
            _repositoryManager.Save();
        }
    }
}
