using AutoMapper;
using AWork.Contracts.Dto.Production;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Production
{
    public class ScrapReasonService : IScrapReasonService
    {
        private IProductionRepositoryManager _repositoryManager;

        private readonly IMapper _mapper;

        public ScrapReasonService(IProductionRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void Edit(ScrapReasonDto scrapReasonDto)
        {
            //throw new NotImplementedException();
            var scrapModel = _mapper.Map<ScrapReason>(scrapReasonDto);
            _repositoryManager.ScrapReasonRepository.Edit(scrapModel);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<ScrapReasonDto>> GetAllScrapReason(bool trackChange)
        {
            //throw new NotImplementedException();
            var scrapModel = await _repositoryManager.ScrapReasonRepository.GetAllScrapReason(trackChange);
            var scrapDto = _mapper.Map<IEnumerable<ScrapReasonDto>>(scrapModel);
            return scrapDto;
        }

        public async Task<ScrapReasonDto> GetLocationById(short reasonId, bool trackChange)
        {
            //throw new NotImplementedException();
            var scrapModel = await _repositoryManager.ScrapReasonRepository.GetLocationById(reasonId, trackChange);
            var scrapDto =  _mapper.Map<ScrapReasonDto>(scrapModel);
            return scrapDto;
        }

        public void Insert(ScrapReasonForCreateDto scrapReasonDtoForCreate)
        {
            //throw new NotImplementedException();
            var scrapModel =_mapper.Map<ScrapReason>(scrapReasonDtoForCreate);
            _repositoryManager.ScrapReasonRepository.Insert(scrapModel);
            _repositoryManager.Save();
        }

        public void Remove(ScrapReasonDto scrapReasonDto)
        {
            //throw new NotImplementedException();
            /*var locationModel = _mapper.Map<Location>(locationDto);
            _repositoryManager.LocationRepository.Edit(locationModel);
            _repositoryManager.Save();*/
            var scrapModel = _mapper.Map<ScrapReason>(scrapReasonDto);
            _repositoryManager.ScrapReasonRepository.Remove(scrapModel);
            _repositoryManager.Save();
        }
    }
}
