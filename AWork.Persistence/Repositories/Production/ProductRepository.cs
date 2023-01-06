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
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(Product product)
        {
            Update(product);
        }

        public async Task<IEnumerable<Product>> GetAllProduct(bool trackChanges)
        {

            return await FindAll(trackChanges).OrderBy(p => p.ProductSubcategoryId)
                .Include(r=>r.SpecialOfferProducts)
                .Include(x => x.WorkOrders)
                .Include(z => z.ProductProductPhotos)
                .ThenInclude(ProductProductPhotos => ProductProductPhotos.ProductPhoto)
                .Include(s => s.ProductSubcategory)
                .ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductPaged(int pageIndex, int pageSize, bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .Include(o => o.ProductSubcategory)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

        }
        public async Task<Product> GetProductById(int ProductId, bool trackChanges)
        {
            var products = await FindByCondition(x => x.ProductId.Equals(ProductId), trackChanges)
                //.Where(y => y.ProductPhotos.Any(p => p.PhotoProductId == productId))
                .Include(c => c.ProductSubcategory)
                .Include(p => p.SizeUnitMeasureCodeNavigation)
                .Include(h => h.WeightUnitMeasureCodeNavigation)
                .Include(x => x.WorkOrders)
                .Include(z => z.ProductProductPhotos)
               // .Include(d=>d.SpecialOfferProducts)
                .ThenInclude(i => i.ProductPhoto)
                .Where(k => k.ProductProductPhotos.Any(x => x.ProductId == k.ProductId))
                .SingleOrDefaultAsync();
            return products;
       }

        public void Insert(Product product)
        {
            Create(product);
        }

        public void Remove(Product product)
        {
            Delete(product);
        }
    }
}
