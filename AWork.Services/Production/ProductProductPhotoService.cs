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
    public class ProductProductPhotoService : IProductProductPhotoService
    {
        private IProductionRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ProductProductPhotoService(IProductionRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void Edit(ProductProductPhotoDto productProductPhotoDto)
        {
            var productPhotoModel = _mapper.Map<ProductProductPhoto>(productProductPhotoDto);
            _repositoryManager.ProductProductPhotoRepository.Edit(productPhotoModel);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<ProductProductPhotoDto>> GetAllProductProductPhoto(bool trackChanges)
        {
            var productPhotoModel = await _repositoryManager.ProductProductPhotoRepository.GetAllProductProductPhoto(trackChanges);

            var productProductPhotoDto = _mapper.Map<IEnumerable<ProductProductPhotoDto>>(productPhotoModel);
            return productProductPhotoDto;
        }

        public async Task<ProductProductPhotoDto> GetProductProductPhotoById(int productId, bool trackChanges)
        {
            var productPhotoModel = await _repositoryManager.ProductProductPhotoRepository.GetProductProductPhotoById(productId, trackChanges);
            var productproductPhotoDto = _mapper.Map<ProductProductPhotoDto>(productPhotoModel);
            return productproductPhotoDto;

        }

        public void Insert(ProductProductPhotoForCreateDto productProductPhotoForCreateDto)
        {
            var productPhotoModel = _mapper.Map<ProductProductPhoto>(productProductPhotoForCreateDto);
            _repositoryManager.ProductProductPhotoRepository.Insert(productPhotoModel);
            _repositoryManager.Save();
        }

        public void Remove(ProductProductPhotoDto productProductPhotoDto)
        {
            var productPhotoModel = _mapper.Map<ProductProductPhoto>(productProductPhotoDto);
            _repositoryManager.ProductProductPhotoRepository.Remove(productPhotoModel);
            _repositoryManager.Save();
        }
    }
}
