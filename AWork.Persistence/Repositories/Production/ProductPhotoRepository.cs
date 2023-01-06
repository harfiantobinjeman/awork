using AWork.Domain.Models;
using AWork.Domain.Repositories.Production;
using AWork.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.Production
{
    internal class ProductPhotoRepository : RepositoryBase<ProductPhoto>, IProductPhotoRepository
    {
        public ProductPhotoRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(ProductPhoto productPhoto)
        {
            Update(productPhoto);
        }

        public async Task<IEnumerable<ProductPhoto>> GetAllProductPhoto(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(p => p.ThumbnailPhotoFileName)
                .Include(x=>x.ProductProductPhotos)
                .ToListAsync();
        }

        public async Task<ProductPhoto> GetProductPhotoById(int ProductPhotoId, bool trackChanges)
        {
            return await FindByCondition(p => p.ProductPhotoId.Equals(ProductPhotoId), trackChanges)
                .Include(x => x.ProductProductPhotos)
                .SingleOrDefaultAsync();
        }

        public void Insert(ProductPhoto productPhoto)
        {
            Create(productPhoto);
        }

        public void Remove(ProductPhoto productPhoto)
        {
            Delete(productPhoto);
        }
    }
}
