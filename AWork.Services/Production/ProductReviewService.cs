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
    public class ProductReviewService : IProductReviewService
    {
        private IProductionRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ProductReviewService(IProductionRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void Edit(ProductReviewDto productReviewDto)
        {
            var productModel = _mapper.Map<ProductReview>(productReviewDto);
            _repositoryManager.ProductReviewRepository.Edit(productModel);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<ProductReviewDto>> GetAllProductReview(bool trackChanges)
        {
            var productModel = await _repositoryManager.ProductReviewRepository.GetAllProductReview(trackChanges);
            var productReviewDto = _mapper.Map<IEnumerable<ProductReviewDto>>(productModel);

            return productReviewDto;
        }

        public async Task<ProductReviewDto> GetProductReviewById(int ProductReviewId, bool trackChanges)
        {
            var productModel = await _repositoryManager.ProductReviewRepository.GetProductReviewById(ProductReviewId, trackChanges);
            var productReviewDto = _mapper.Map<ProductReviewDto>(productModel);

            return productReviewDto;
        }

        public void Insert(ProductReviewForCreateDto productReviewForCreateDto)
        {
            var productModel = _mapper.Map<ProductReview>(productReviewForCreateDto);
            _repositoryManager.ProductReviewRepository.Insert(productModel);
            _repositoryManager.Save();
        }

        public void Remove(ProductReviewDto productReviewDto)
        {
            var productModel = _mapper.Map<ProductReview>(productReviewDto);
            _repositoryManager.ProductReviewRepository.Remove(productModel);
            _repositoryManager.Save();
        }

    }
}
