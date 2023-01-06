using AutoMapper;
using AWork.Contracts.Dto.HumanResources.Employee;
using AWork.Contracts.Dto.Production;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Production
{
    public class ProductService : IProductService
    {
        private IProductionRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
       
        public ProductService(IProductionRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProduct(bool trackChanges)
        {
            var productModel = await _repositoryManager.ProductRepository.GetAllProduct(trackChanges);
            var productDto = _mapper.Map<IEnumerable<ProductDto>>(productModel);

            return productDto;

        }

        public async Task<ProductDto> GetProductById(int ProductId, bool trackChanges)
        {
            var productModel = await _repositoryManager.ProductRepository.GetProductById(ProductId, trackChanges);
            var productDto = _mapper.Map<ProductDto>(productModel);

            return productDto;
        }
        public async Task<IEnumerable<ProductDto>> GetProductPaged(int pageIndex, int pageSize, bool trackChanges)
        {
            var productModel = await _repositoryManager.ProductRepository.GetProductPaged(pageIndex, pageSize, trackChanges);
            //source = Category model, target Category Dto
            var productDto = _mapper.Map<IEnumerable<ProductDto>>(productModel);
            return productDto;
        }
        public void Insert(ProductForCreateDto productForCreateDto)
        {
            var productModel = _mapper.Map<Product>(productForCreateDto);
            _repositoryManager.ProductRepository.Insert(productModel);
            _repositoryManager.Save();
        }

        public void Remove(ProductDto productDto)
        {
            var productModel = _mapper.Map<Product>(productDto);
            _repositoryManager.ProductRepository.Remove(productModel);
            _repositoryManager.Save();
        }
        /*  public void Edit(ProductDto productDto)
          {
              var productModel = _mapper.Map<Product>(productDto);
              _repositoryManager.ProductRepository.Edit(productModel);
              _repositoryManager.Save();
          }*/
        public void Edit(UploadImages uploadImages)
        {
            var productModel = _mapper.Map<Product>(uploadImages);
  
            _repositoryManager.ProductRepository.Edit(productModel);
            _repositoryManager.Save();
        }

        public ProductDto CreateProductId()
        {
            var product = new Product
            {
                ModifiedDate = DateTime.Now,
                //SellStartDate = DateTime.Now,
                //SellEndDate = DateTime.Now
                
            };
            _repositoryManager.ProductRepository.Insert(product);
            _repositoryManager.Save();
            var product_Dto = _mapper.Map<ProductDto>(product);
            return product_Dto;
        }
        public void CreateProductIdPhoto(ProductForCreateDto productForCreateDto, ProductProductPhotoForCreateDto productProductPhotoForCreateDto, List<ProductPhotoForCreateDto> productPhotoForCreateDtos)
        {
            //1. insert into table production
            var ModifierDate = DateTime.Now;
            var productModel = _mapper.Map<Product>(productForCreateDto);
            productModel.ModifiedDate = ModifierDate;
            productModel.Rowguid = Guid.NewGuid();
            _repositoryManager.ProductRepository.Insert(productModel);
            _repositoryManager.Save();

            //3. insert into table product Photo

            /*foreach (var item in productPhotoForCreateDtos)
            {
                var photoModel2 = _mapper.Map<ProductPhoto>(item);
                _repositoryManager.ProductPhotoRepository.Insert(photoModel2);

            }

            _repositoryManager.Save();*/
            //single foto 

            /*var productPhoto_Dto1 = _mapper.Map<ProductPhoto>(photos1);

            _repositoryManager.ProductPhotoRepository.Insert(productPhoto_Dto1);
            _repositoryManager.Save();*/


            //2 foto sisa
            foreach (var item in productPhotoForCreateDtos)
            {

                var productPhoto_Dto = _mapper.Map<ProductPhoto>(item);

                _repositoryManager.ProductPhotoRepository.Insert(productPhoto_Dto);
                _repositoryManager.Save();

                var productModel1 = _mapper.Map<ProductProductPhoto>(productProductPhotoForCreateDto);
                //_repositoryManager.ProductPhotoRepository.Insert(productPhoto_Dto);
                /*var list1 = productPhotoForCreateDtos[0];
                var list2 = productPhotoForCreateDtos[0];*/
                var primary = productProductPhotoForCreateDto.Primary;
                if (item == productPhotoForCreateDtos[0] && primary == true)
                {
                    productModel1.Primary = true;
                }
                else if(item == productPhotoForCreateDtos[0] && primary == false)
                {
                    productModel1.Primary = false;
                }
                else
                {
                    productModel1.Primary= false;
                }

                productModel1.ProductId = productModel.ProductId;
                productModel1.ProductPhotoId = productPhoto_Dto.ProductPhotoId;
                //var pri = productProductPhotoForCreateDto.Primary;
                //pri = Convert.ToBoolean(productProductPhotoForCreateDto.FirstOrDefault().productModel1.ProductPhotoId);
                               
                //productModel1.ProductPhotoId = productPhotoForCreateDtos.FirstOrDefault().ProductPhotoId;
                productModel1.ModifiedDate = ModifierDate;
                _repositoryManager.ProductProductPhotoRepository.Insert(productModel1);

                _repositoryManager.Save();
            }



            //2. Insert  into table product2 Photo

          /*  foreach (var item in productPhotoForCreateDtos)
            {
                var productPhoto_Dto = _mapper.Map<ProductPhoto>(item);

                var productModel1 = _mapper.Map<ProductProductPhoto>(productProductPhotoForCreateDto);
                //_repositoryManager.ProductPhotoRepository.Insert(productPhoto_Dto);

                productModel1.ProductId = productModel.ProductId;
                productModel1.ProductPhotoId = productPhoto_Dto.ProductPhotoId;
                //productModel1.ProductPhotoId = productPhotoForCreateDtos.FirstOrDefault().ProductPhotoId;
                productModel1.ModifiedDate = ModifierDate;
                _repositoryManager.ProductProductPhotoRepository.Insert(productModel1);
            }
           
            _repositoryManager.Save();
*/


        }

        public void EditProductIdPhoto(ProductDto productDto, ProductProductPhotoDto productProductPhotoDto, List<ProductPhotoDto> listPhoto)
        {
            var ModifierDate = DateTime.Now;
            var productModel = _mapper.Map<Product>(productDto);
            productModel.ModifiedDate = ModifierDate;
            productModel.Rowguid = Guid.NewGuid();
            _repositoryManager.ProductRepository.Edit(productModel);
            _repositoryManager.Save();

            foreach (var item in listPhoto)
            {

                var productPhoto_Dto = _mapper.Map<ProductPhoto>(item);

                _repositoryManager.ProductPhotoRepository.Edit(productPhoto_Dto);
                _repositoryManager.Save();

                var productModel1 = _mapper.Map<ProductProductPhoto>(item);
                //var productModel2 = _mapper.Map<ProductProductPhoto>(item);
                
                /*var primary = productProductPhotoDto.Primary;
                if (item == listPhoto[0] && primary == true)
                {
                    productModel1.Primary = true;
                }
                else if (item == listPhoto[0] && primary == false)
                {
                    productModel1.Primary = false;
                }
                else
                {
                    productModel1.Primary = false;
                }*/

                productModel1.ProductId = productModel.ProductId;
                productModel1.ProductPhotoId = productPhoto_Dto.ProductPhotoId;
                //var pri = productProductPhotoForCreateDto.Primary;
                //pri = Convert.ToBoolean(productProductPhotoForCreateDto.FirstOrDefault().productModel1.ProductPhotoId);

                //productModel1.ProductPhotoId = productPhotoForCreateDtos.FirstOrDefault().ProductPhotoId;
                productModel1.ModifiedDate = ModifierDate;
                _repositoryManager.ProductProductPhotoRepository.Edit(productModel1);

                _repositoryManager.Save();
            }

        }

        public void Edits(ProductDto productEdit, ProductProductPhotoDto productproductEdit, ProductPhotoDto productPhotoEdit)
        {
            var productModel = _mapper.Map<Product>(productEdit);
            _repositoryManager.ProductRepository.Edit(productModel);
            _repositoryManager.Save();

            var productProductPhotoModel = _mapper.Map<ProductProductPhoto>(productproductEdit);
            _repositoryManager.ProductProductPhotoRepository.Edit(productProductPhotoModel);
            _repositoryManager.Save();

            var productPhoto = _mapper.Map<ProductPhoto>(productPhotoEdit);
            _repositoryManager.ProductPhotoRepository.Edit(productPhoto);
            _repositoryManager.Save();
        }
    }
}
