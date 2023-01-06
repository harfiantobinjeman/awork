using AutoMapper;
using AWork.Contracts.Dto.PersonModule;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction.PersonModul;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace AWork.Services.PersonService
{
    public class CountryRegionServices : ICountryRegionServices
    {
        private IPersonRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CountryRegionServices(IPersonRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void Edit(CountryRegionDto countryRegionDto)
        {
            var countryRegionMdl = _mapper.Map<CountryRegion>(countryRegionDto);
            _repositoryManager.CountryRegionRepository.Edit(countryRegionMdl);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<CountryRegionDto>> GetAllCountryRegion(bool trackChanges)
        {
            var countryRegionMdl = await _repositoryManager.CountryRegionRepository.GetAllCountryRegion(trackChanges);
            var countryRegionDto = _mapper.Map<IEnumerable<CountryRegionDto>>(countryRegionMdl);
            return countryRegionDto;
        }

        public async Task<CountryRegionDto> GetAllCountryRegionByCode(string countryCode, bool trackChanges)
        {
            var countryRegionMdl = await _repositoryManager.CountryRegionRepository.GetAllCountryRegionByCode(countryCode, trackChanges);
            var countryRegionDto = _mapper.Map<CountryRegionDto>(countryRegionMdl);
            return countryRegionDto;
        }

        public void Insert(CountryRegionForCreateDto countryRegionForCreateDto)
        {
            var countryRegionMdl = _mapper.Map<CountryRegion>(countryRegionForCreateDto);
            _repositoryManager.CountryRegionRepository.Insert(countryRegionMdl);
            _repositoryManager.Save();
        }

        public void Remove(CountryRegionDto countryRegionDto)
        {
            var countryRegionMdl = _mapper.Map<CountryRegion>(countryRegionDto);
            _repositoryManager.CountryRegionRepository.Remove(countryRegionMdl);
            _repositoryManager.Save();
        }

        public CountryRegionDto CountryRegion()
        {
            var countryRegion = new CountryRegion
            {  
               
                ModifiedDate = DateTime.Now
            };
            _repositoryManager.CountryRegionRepository.Insert(countryRegion);
            _repositoryManager.Save();

            var countryRegionDto = _mapper.Map<CountryRegionDto>(countryRegion);
            return countryRegionDto;
        }
    }
}
