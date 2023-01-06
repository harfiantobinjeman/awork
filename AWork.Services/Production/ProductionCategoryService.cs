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
    internal class ProductionCategoryService : IProductCategoryService
    {
        private IProductionRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ProductionCategoryService(IProductionRepositoryManager productionRepositoryManager, IMapper mapper)
        {
            _repositoryManager = productionRepositoryManager;
            _mapper = mapper;
        }

        public void Edit(ProductCategoryDto productCategoryDto)
        {
            ProductCategoryDto product = new ProductCategoryDto()
            {
                ProductCategoryId = productCategoryDto.ProductCategoryId,
                Name = productCategoryDto.Name,
                //ModifiedDate = DateTime.Now,
                Rowguid = System.Guid.NewGuid()

            };
            var productCateModel = _mapper.Map<ProductCategory>(product);
            _repositoryManager.ProductCategoryRepository.edit(productCateModel);
            _repositoryManager.Save();

            //insert subcategory baru

            foreach (var item in productCategoryDto.ProductSubCategoryForCreateDto)
            {
                if (item.Name != null)
                {
                    item.ProductCategoryId = product.ProductCategoryId;
                    var productSubcategory = _mapper.Map<ProductSubcategory>(item);
                    _repositoryManager.ProductSubCategoryRepository.insert(productSubcategory);
                    //item.ModifiedDate = DateTime.Now;
                    item.Rowguid = new Guid();

                }
            }
            _repositoryManager.Save();

            //data update
            if (productCategoryDto.ProductSubcategories != null)
            {
                foreach (var item in productCategoryDto.ProductSubcategories)
                {
                    item.ProductCategoryId = productCategoryDto.ProductCategoryId;
                    item.Name = productCategoryDto.Name;
                    //item.ModifiedDate = DateTime.Now;
                    var productSubcategory = _mapper.Map<ProductSubcategory>(item);
                    _repositoryManager.ProductSubCategoryRepository.edit(productSubcategory);
                }
                _repositoryManager.Save();
            }
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetAllProdcCategory(bool trackChanges)
        {
            var procdCategory = await _repositoryManager.ProductCategoryRepository.GetAllProdcCategory(trackChanges);
            var prodcCategoryDto = _mapper.Map<IEnumerable<ProductCategoryDto>>(procdCategory);
            return prodcCategoryDto;
        }

        public async Task<ProductCategoryDto> GetProcdCateById(int prodcCategory, bool trackChanges)
        {
            var productCateModel = await _repositoryManager.ProductCategoryRepository.GetProcdCateById(prodcCategory, trackChanges);
            var productCateDto = _mapper.Map<ProductCategoryDto>(productCateModel);
            return productCateDto;
        }

        public void Insert(ProductCategoryForCreatDto productCategoryForCreat)
        {

            /*  var prodCate = new ProductCategoryForCreatDto()
              {
                  Name = productCategoryForCreat.Name,
              };*/

            var productCateModel = _mapper.Map<ProductCategory>(productCategoryForCreat);
            _repositoryManager.ProductCategoryRepository.insert(productCateModel);
            _repositoryManager.Save();

            //Input data to table 
            foreach (var item in productCategoryForCreat.ProductSubCategoryForCreateDto)
            {
                item.ProductCategoryId = productCateModel.ProductCategoryId;
                var productSubcategory = _mapper.Map<ProductSubcategory>(item);
                _repositoryManager.ProductSubCategoryRepository.insert(productSubcategory);

            }
            _repositoryManager.Save();
        }


        public void Remove(ProductCategoryDto productCategoryDto)
        {
            var productCateModel = _mapper.Map<ProductCategory>(productCategoryDto);
            _repositoryManager.ProductCategoryRepository.remove(productCateModel);
            _repositoryManager.Save();
        }
    }
}
