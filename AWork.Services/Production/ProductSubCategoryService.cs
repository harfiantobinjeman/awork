using AutoMapper;
using AWork.Contracts.Dto.Production;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction.Production;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Production
{
    internal class ProductSubCategoryService : IProductSubCategoryService
    {
        private IProductionRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ProductSubCategoryService(IProductionRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductSubCategoryDto>> GetAllProdcSubCategory(bool trackChanges)
        {
            var procdSubCategory = await _repositoryManager.ProductSubCategoryRepository.GetAllProdcSubCategory(trackChanges);
            var procdSubCategoryDto = _mapper.Map<IEnumerable<ProductSubCategoryDto>>(procdSubCategory);
            return procdSubCategoryDto;
        }
        public void Edit(ProductSubCategoryDto productSubCategoryDto)
        {
            var productSubCateModel = _mapper.Map<ProductSubcategory>(productSubCategoryDto);
            _repositoryManager.ProductSubCategoryRepository.edit(productSubCateModel);
            _repositoryManager.Save();


            //var prodcSubCategoryDto = _productionRepositoryManager.productSubCategory.GetProcdSubCateById(productSubCategoryDto,false);
            //throw new NotImplementedException();
        }

        public async Task<ProductSubCategoryDto> GetProcdSubCateById(int prodcSubCategory, bool trackChanges)
        {
            var productSubCateModel = await _repositoryManager.ProductSubCategoryRepository.GetProdcSubCateById(prodcSubCategory, trackChanges);
            var productSubCateDto = _mapper.Map<ProductSubCategoryDto>(productSubCateModel);
            return productSubCateDto;
        }

        public void Insert(ProductSubCategoryForCreateDto productSubCategoryForCreate)
        {
            var productSubCateModel = _mapper.Map<ProductSubcategory>(productSubCategoryForCreate);
            _repositoryManager.ProductSubCategoryRepository.insert(productSubCateModel);
            _repositoryManager.Save();
        }

        public void Remove(ProductSubCategoryDto productSubCategoryDto)
        {
            var productSubCateModel = _mapper.Map<ProductSubcategory>(productSubCategoryDto);
            _repositoryManager.ProductSubCategoryRepository.remove(productSubCateModel);
            _repositoryManager.Save();
        }
        public async Task<IEnumerable<ProductSubCategoryDto>> GetProCateInSubByID(int ProductCategoryId, bool trackChanges)
        {
            var procdSubCategory = await _repositoryManager.ProductSubCategoryRepository.GetProCateInSubByID(ProductCategoryId, trackChanges);
            var procdSubCategoryDto = _mapper.Map<IEnumerable<ProductSubCategoryDto>>(procdSubCategory);
            return procdSubCategoryDto;
        }
    }
}
