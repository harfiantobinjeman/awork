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
    public class BusinessEntityContactServices : IBusinessEntityContactServices
    {
        private IPersonRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public BusinessEntityContactServices(IPersonRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<BusinessEntityContactDto>> GetAllBusinessEntityContact(bool trackChanges)
        {
            var businessEntityContactMdl = await _repositoryManager.BusinessEntityContactRepository.GetAllBusinessEntityContact(trackChanges);
            var businessEntityContactDto = _mapper.Map<IEnumerable<BusinessEntityContactDto>>(businessEntityContactMdl);
            return businessEntityContactDto;
        }

        public async Task<BusinessEntityContactDto> GetBusinessEntityContactById(int businessEntityContactId, bool trackChanges)
        {
            var businessEntityContactMdl = await _repositoryManager.BusinessEntityContactRepository.GetBusinessEntityContactById(businessEntityContactId, trackChanges);
            var businessEntityContactDto = _mapper.Map<BusinessEntityContactDto>(businessEntityContactMdl);
            return businessEntityContactDto;
        }

        public void insert(BusinessEntityContactForCreateDto businessEntityContactForCreate)
        {
            var businessEntityContactMdl = _mapper.Map<BusinessEntityContact>(businessEntityContactForCreate);
            _repositoryManager.BusinessEntityContactRepository.Insert(businessEntityContactMdl);
            _repositoryManager.Save();
        }

        public void edit(BusinessEntityContactDto businessEntityContactDto)
        {
            throw new NotImplementedException();
        }

        public void delete(BusinessEntityContactDto businessEntityContactDto)
        {
            var businessEntityContactMdl = _mapper.Map<BusinessEntityContact>(businessEntityContactDto);
            _repositoryManager.BusinessEntityContactRepository.Remove(businessEntityContactMdl);
            _repositoryManager.SaveAsync();
        }
    }
}
