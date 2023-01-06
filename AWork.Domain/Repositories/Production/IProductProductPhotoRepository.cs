using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Production
{
    public interface IProductProductPhotoRepository
    {
        Task<IEnumerable<ProductProductPhoto>> GetAllProductProductPhoto(bool trackChanges);
        Task<ProductProductPhoto> GetProductProductPhotoById(int productId, bool trackChanges);
        void Insert(ProductProductPhoto productProductPhoto);
        void Edit(ProductProductPhoto productProductPhoto);
        void Remove(ProductProductPhoto productProductPhoto);
    }
}
