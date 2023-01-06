using AWork.Contracts.Dto.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Production
{
    public interface IProductProductPhotoService
    {
        Task<IEnumerable<ProductProductPhotoDto>> GetAllProductProductPhoto(bool trackChanges);
        Task<ProductProductPhotoDto> GetProductProductPhotoById(int productId, bool trackChanges);
        void Insert(ProductProductPhotoForCreateDto productProductPhotoForCreateDto);
        void Edit(ProductProductPhotoDto productProductPhotoDto);
        void Remove(ProductProductPhotoDto productProductPhotoDto);
    }
}
