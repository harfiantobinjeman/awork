using AWork.Contracts.Dto.Production;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Production
{
    public interface IProductPhotoService
    {
        Task<IEnumerable<ProductPhotoDto>> GetAllProductPhoto(bool trackChanges);
        Task<ProductPhotoDto> GetProductPhotoById(int ProductPhotoId, bool trackChanges);
        ProductPhotoDto CreateProductPhotoId();
        void Insert(ProductPhotoDto productPhotoDto);
        void Edit(ProductPhotoDto productPhotoDto);
        void Remove(ProductPhotoDto productPhotoDto);
    }
}
