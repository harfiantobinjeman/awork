using AWork.Domain.Models;
using AWork.Domain.Repositories.Production;
using AWork.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.Production
{
    internal class ProductProductPhotoRepository : RepositoryBase<ProductProductPhoto>, IProductProductPhotoRepository
    {
        public ProductProductPhotoRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(ProductProductPhoto productProductPhoto)
        {
            Update(productProductPhoto);
        }

        public async Task<IEnumerable<ProductProductPhoto>> GetAllProductProductPhoto(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(p => p.Primary)
                .Include(b => b.Product)
                .Include(b => b.ProductPhoto)
                .ToListAsync();

        }

        public async Task<ProductProductPhoto> GetProductProductPhotoById(int productId, bool trackChanges)
        {
            return await FindByCondition(p => p.ProductId.Equals(productId), trackChanges)
                .Include(e => e.Product)
                .Include(e => e.ProductPhoto)
                .Include(f => f.Product.ProductSubcategory)
                .Include(p => p.Product.SizeUnitMeasureCodeNavigation)
                .Include(h => h.Product.WeightUnitMeasureCodeNavigation)
                .SingleOrDefaultAsync();
        }

        public void Insert(ProductProductPhoto productProductPhoto)
        {
            Create(productProductPhoto);
        }

        public void Remove(ProductProductPhoto productProductPhoto)
        {
            Delete(productProductPhoto);
        }
    }
}
