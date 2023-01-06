using AWork.Contracts.Dto.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProduct(bool trackChanges);
        Task<ProductDto> GetProductById(int UnitMeasureCode, bool trackChanges);
        Task<IEnumerable<ProductDto>> GetProductPaged(int pageIndex, int pageSize, bool trackChanges);
        ProductDto CreateProductId();
        void CreateProductIdPhoto(ProductForCreateDto productForCreateDto, ProductProductPhotoForCreateDto productProductPhotoForCreateDto, List<ProductPhotoForCreateDto> productPhotoForCreateDtos);
        void EditProductIdPhoto(ProductDto productDto, ProductProductPhotoDto productProductPhotoDto, List<ProductPhotoDto> listPhoto);
        void Insert(ProductForCreateDto productForCreateDto);
        //void Edit(ProductDto productDto);
                
        void Edits(ProductDto productEdit, ProductProductPhotoDto productproductEdit, ProductPhotoDto productPhotoEdit);
        void Edit(UploadImages uploadImages);
        void Remove(ProductDto productDto);

    }
}
