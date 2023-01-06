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
    internal class ProductCostHistoryService : IProductCostHistoryService
    {
        private IProductionRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ProductCostHistoryService(IProductionRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void Edit(ProductCostHistoryDto productCostHistoryDto)
        {
            var productCH = _mapper.Map<ProductCostHistory>(productCostHistoryDto);
            _repositoryManager.ProductCostHistoryRepository.Edit(productCH);
            _repositoryManager.Save();

            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductCostHistoryDto>> GetAllProductCostHistory(bool trackChanges)
        {
            var procdHC = await _repositoryManager.ProductCostHistoryRepository.GetAllProductCostHistory(trackChanges);
            var procdHCDto = _mapper.Map<IEnumerable<ProductCostHistoryDto>>(procdHC);
            return procdHCDto;
        }

        public async Task<ProductCostHistoryDto> GetProductCostHistoryById(int ProductId, bool trackChanges)
        {
            var productHC = await _repositoryManager.ProductCostHistoryRepository.GetProductCostHistoryById(ProductId, trackChanges);
            var productHCDto = _mapper.Map<ProductCostHistoryDto>(productHC);
            return productHCDto;
        }

        public void Insert(ProductCostHistoryForCreateDto productCostHistoryForCreateDto)
        {
            var productCH = _mapper.Map<ProductCostHistory>(productCostHistoryForCreateDto);
            _repositoryManager.ProductCostHistoryRepository.Insert(productCH);
            _repositoryManager.Save();
        }

        public void Remove(ProductCostHistoryDto productCostHistoryDto)
        {
            var productCH = _mapper.Map<ProductCostHistory>(productCostHistoryDto);
            _repositoryManager.ProductCostHistoryRepository.Remove(productCH);
            _repositoryManager.Save();
        }
    }
}
