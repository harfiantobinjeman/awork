using AutoMapper;
using AWork.Contracts.Dto.Production;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Persistence.Base;
using AWork.Services.Abstraction;
using AWork.Services.Abstraction.Production;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Production
{
    public class LocationService : ILocationService
    {
        private IProductionRepositoryManager _repositoryManager;

        private readonly IMapper _mapper;

        public LocationService(IProductionRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LocationDto>> GetAllLocation(bool trackChange)
        {
            //throw new NotImplementedException();
            var locationModel = await _repositoryManager.LocationRepository.GetAllLocation(trackChange);
            var locationDto = _mapper.Map<IEnumerable<LocationDto>>(locationModel);
            return locationDto;
        }

        public async Task<LocationDto> GetLocationById(short locationId, bool trackChange)
        {
            //throw new NotImplementedException();
            var locationModel = await _repositoryManager.LocationRepository.GetLocationById(locationId, trackChange);
            var locationDto = _mapper.Map<LocationDto>(locationModel);
            return locationDto;
        }

        public void Insert(LocationForCreateDto locationForCreateDto)
        {
            //throw new NotImplementedException();
            var locationModel = _mapper.Map<Location>(locationForCreateDto);
            _repositoryManager.LocationRepository.Insert(locationModel);
            _repositoryManager.Save();
        }

        public void Edit(LocationDto locationDto)
        {
            //throw new NotImplementedException();
            var locationModel = _mapper.Map<Location>(locationDto);
            _repositoryManager.LocationRepository.Edit(locationModel);
            _repositoryManager.Save();
        }

        public void Remove(LocationDto locationDto)
        {
            /*throw new NotImplementedException();
             var categoryModel = _mapper.Map<Category>(categoryDto);
            _repositoryManager.CategoryRepository.Remove(categoryModel);
            _repositoryManager.SaveAsync();*/
            var locationModel = _mapper.Map<Location>(locationDto);
            _repositoryManager.LocationRepository.Remove(locationModel);
            _repositoryManager.Save();
        }

        public LocationDto CreateLocation()
        {
            //throw new NotImplementedException();
            var location = new Location
            {
                Name = "gantengkun",
                ModifiedDate = DateTime.Now
            };
            _repositoryManager.LocationRepository.Insert(location);
            _repositoryManager.Save();
            var location_Dto = _mapper.Map<LocationDto>(location);
            return location_Dto;
        }

        public async Task<IEnumerable<LocationDto>> GetSetLocation(bool trackChange)
        {
            //throw new NotImplementedException();
            var locationModel = await _repositoryManager.LocationRepository.GetSetLocation(trackChange);
            var locationDto = _mapper.Map<IEnumerable<LocationDto>>(locationModel);
            return locationDto;

        }
    }
}
