using AWork.Domain.Models;
using AWork.Domain.Repositories.Production;
using AWork.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.Production
{
    internal class ProductSubCategoryRepository : RepositoryBase<ProductSubcategory>, IProductSubCategoryRepository
    {
        public ProductSubCategoryRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void edit(ProductSubcategory productSubCategory)
        {
            Update(productSubCategory);
        }

        public async Task<IEnumerable<ProductSubcategory>> GetAllProdcSubCategory(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<IEnumerable<ProductSubcategory>> GetProCateInSubByID(int ProductCategoryId, bool trackChanges)
        {
            return await FindAll(trackChanges).Where(x=>x.ProductCategoryId == ProductCategoryId)
                .OrderBy(x=>x.ProductSubcategoryId)
                .Include(a => a.ProductCategory)
                .ToListAsync();

            /*return await FindAll(trackChanges).OrderBy(x=>x.Name)
                                 .Include(c=>c.ProductCategory)
                                .ToListAsync();*/
                                                

            /*return await FindAll(trackChanges).OrderBy(x=>x.ProductCategoryId)
                .Include(n=>n.Name).ToListAsync();*/
            /*var subcate = await _dbContext.ProductSubcategories
                            .Include(i => i.ProductCategoryId)
                            .Include(j => j.ProductSubcategoryId)
                            .OrderBy(n => n.Name).ToListAsync();
            return subcate;*/

            /*
            var GPCT = await FindAll(trackChanges)
                .Include(c => c.ProductSubcategoryId)
                .OrderBy(n=>n.Name)
                .SingleOrDefaultAsync();
            return GPCT;*/
            //return await FindByCondition(c => c.ProductSubcategoryId.Equals(ProductCategoryId), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<ProductSubcategory> GetProdcSubCateById(int ProductSubcategoryId, bool trackChanges)
        {
            return await FindByCondition(c => c.ProductSubcategoryId.Equals(ProductSubcategoryId), trackChanges).SingleOrDefaultAsync();
        }

        public void insert(ProductSubcategory productSubCategory)
        {
            Create(productSubCategory);
        }

        public void remove(ProductSubcategory productSubCategory)
        {
            Delete(productSubCategory);
        }
    }
}
