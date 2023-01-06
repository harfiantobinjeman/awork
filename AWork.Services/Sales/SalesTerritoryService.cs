using AutoMapper;
using AWork.Contracts.Dto.Sales.SalesTerritory;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction.Sales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Sales
{
    public class SalesTerritoryService : ISalesTerritoryService
    {
        private readonly ISalesRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public SalesTerritoryService(ISalesRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void Edit(SalesTerritoryDto salesTerritoryDto)
        {
            var edit = _mapper.Map<SalesTerritory>(salesTerritoryDto);
            _repositoryManager.SalesTerritoryRepository.Edit(edit);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<SalesTerritoryDto>> GetAllSalesTerritory(bool trackChanges)
        {
            var salesTerritoryModel = await _repositoryManager.SalesTerritoryRepository.GetAllSalesTerritory(trackChanges);
            // source = categoryModel, target = CategoryDto
            var salesTerritoryDto = _mapper.Map<IEnumerable<SalesTerritoryDto>>(salesTerritoryModel);
            return salesTerritoryDto;
        }

        public async Task<SalesTerritoryDto> GetSalesTerritoryById(int territoryId, bool trackChanges)
        {
            var salesTerritoryModel = await _repositoryManager.SalesTerritoryRepository.GetSalesTerritoryById(territoryId, trackChanges);
            var salesTerritoryDto = _mapper.Map<SalesTerritoryDto>(salesTerritoryModel);
            return salesTerritoryDto;
        }

        public void Insert(SalesTerritoryForCreateDto salesTerritoryForCreateDto)
        {
            var newUser = _mapper.Map<SalesTerritory>(salesTerritoryForCreateDto);
            _repositoryManager.SalesTerritoryRepository.Insert(newUser);
            _repositoryManager.Save();
        }

        public void Remove(SalesTerritoryDto salesTerritoryDto)
        {
            var deleteUser = _mapper.Map<SalesTerritory>(salesTerritoryDto);
            _repositoryManager.SalesTerritoryRepository.Remove(deleteUser);
            _repositoryManager.Save();
        }
    }
}
