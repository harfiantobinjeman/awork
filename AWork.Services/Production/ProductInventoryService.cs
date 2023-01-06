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
    internal class ProductInventoryService : IProductInventoryService 
    {
        private IProductionRepositoryManager _repositoryManager;

        private readonly IMapper _mapper;

        public ProductInventoryService(IProductionRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductInventoryDto>> GetAllInventory(bool trackChange)
        {
            //throw new NotImplementedException();
            var inventoryModel = await _repositoryManager.ProductInventoryRepository.GetAllInventory(trackChange);
            var inventoryDto = _mapper.Map<IEnumerable<ProductInventoryDto>>(inventoryModel);
            return inventoryDto;
        }


        public void Insert(ProductInventoryDtoForCreate inventoryDtoForCreate)
        {
            //throw new NotImplementedException();
            var inventoryDto = _mapper.Map<ProductInventory>(inventoryDtoForCreate);
            _repositoryManager.ProductInventoryRepository.Insert(inventoryDto);
            _repositoryManager.Save();
        }

        public void Edit(ProductInventoryDto productInventoryDto)
        {
            //throw new NotImplementedException();
            var inventoryDto = _mapper.Map<ProductInventory>(productInventoryDto);
            _repositoryManager.ProductInventoryRepository.Edit(inventoryDto);
            _repositoryManager.Save();
        }

        public void Remove(ProductInventoryDto productInventoryDto)
        {
            //throw new NotImplementedException();
            var inventoryDto = _mapper.Map<ProductInventory>(productInventoryDto);
            _repositoryManager.ProductInventoryRepository.Remove(inventoryDto);
            _repositoryManager.Save();
        }

        public async Task<ProductInventoryDto> GetInventoryById(int id, int locationId, bool trackChange)
        {
            var inventoryModel = await _repositoryManager.ProductInventoryRepository.GetInventoryById(id, locationId, trackChange);
            var inventriDto = _mapper.Map<ProductInventoryDto>(inventoryModel);
            return inventriDto;
        }

        /*public async Task<ProductInventoryDto> GetInventoryById(int id, bool trackChange)
        {
            //throw new NotImplementedException();
            var locationModel = await _repositoryManager.LocationRepository.GetLocationById(locationId, trackChange);
            var locationDto = _mapper.Map<LocationDto>(locationModel);
            return locationDto;
        }*/
    }
}
