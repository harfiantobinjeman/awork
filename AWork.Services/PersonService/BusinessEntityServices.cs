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
    public class BusinessEntityServices : IBusinessEntityServices
    {
        private IPersonRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public BusinessEntityServices(IPersonRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            this._mapper = mapper;
        }

        public void Delete(BusinessEntityDto businessEntityDto)
        {
            var businessEntityMdl = _mapper.Map<BusinessEntity>(businessEntityDto);
            _repositoryManager.BusinessEntityRepository.Remove(businessEntityMdl);
            _repositoryManager.SaveAsync();

        }

        public void Edit(BusinessEntityDto businessEntityDto)
        {
            var businessEnityMdl = _mapper.Map<BusinessEntity>(businessEntityDto);
            _repositoryManager.BusinessEntityRepository.Edit(businessEnityMdl);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<BusinessEntityDto>> GetAllBusinessEntity(bool trackChanges)
        {
            var businessEntityMdl = await _repositoryManager.BusinessEntityRepository.GetAllBusinessEntity(trackChanges);
            var businessEntityDto = _mapper.Map<IEnumerable<BusinessEntityDto>>(businessEntityMdl);
            return businessEntityDto;
        }

        public async Task<BusinessEntityDto> GetBusinessEntityById(int businessEntityid, bool trackChanges)
        {
            var businessEntityMdl = await _repositoryManager.BusinessEntityRepository.GetBusinessEntityById(businessEntityid, trackChanges);
            var businessEntityDto = _mapper.Map<BusinessEntityDto>(businessEntityMdl);
            return businessEntityDto;
        }

        public void Insert(BusinessEntityForCreateDto businessEntityForCreateDto)
        {
            var businessEntityMdl = _mapper.Map<BusinessEntity>(businessEntityForCreateDto);
            _repositoryManager.BusinessEntityRepository.Insert(businessEntityMdl);
            _repositoryManager.Save();
        }

        public BusinessEntityDto CreateBusinessEntity()
        {
            var businessEntity = new BusinessEntity
            {
                ModifiedDate = DateTime.Now
            };
            _repositoryManager.BusinessEntityRepository.Insert(businessEntity);
            _repositoryManager.Save();

            var businessEntitydto = _mapper.Map<BusinessEntityDto>(businessEntity);
            return businessEntitydto;
        }

    }
}
