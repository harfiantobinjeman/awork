using AWork.Contracts.Dto.Production;
using AWork.Domain.Dto;
using AWork.Domain.Dto.Production;
using AWork.Domain.Models;
using AWork.Domain.Repositories.Production;
using AWork.Persistence.Base;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.Production
{
    internal class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void edit(ProductCategory productCategory)
        {
            Update(productCategory);
        }

        public async Task<IEnumerable<ProductCategory>> GetAllProdcCategory(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.ProductCategoryId).ToListAsync();
        }

        

        public async Task<ProductCategory> GetProcdCateById(int prodcCategory, bool trackChanges)
        {
            return await FindByCondition(c => c.ProductCategoryId.Equals(prodcCategory), trackChanges)
                .Include(c=>c.ProductSubcategories)
                .SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<TotalProductByCategory>> GetTotalProductByCategories(string category)
        {
            
            var rawSQL = await _dbContext.GetTotalProductByCategories
                .FromSqlRaw("select (c.Name)Category,  count(ProductID)TotalProduct " +
                "from Production.ProductCategory c " +
                "join Production.ProductSubcategory s " +
                "on c.ProductCategoryID = s.ProductSubcategoryID " +
                "join Production.Product p on s.ProductSubcategoryID " +
                "= p.ProductSubcategoryID group by c.Name")
                .Select(x => new TotalProductByCategory
                {
                    Category = x.Category,
                    TotalProduct = x.TotalProduct

                })
                .OrderBy(x => x.Category)
                .ToListAsync();

            return rawSQL;
        }
        public async Task<IEnumerable<GetCategory>> GetCategoryById(string category)
        {
            var categoryId = new SqlParameter("@Id", category);
            var rawSQL = await _dbContext.GetCategoryById
                .FromSqlRaw("select (p.Name)ProductName, count(ProductID)TotalProduct " +
                "from Production.ProductCategory c " +
                "join Production.ProductSubcategory s on c.ProductCategoryID = s.ProductCategoryID " +
                "join Production.Product p on s.ProductSubcategoryID = p.ProductSubcategoryID " +
                "where c.Name = @Id " +
                "group by p.Name ", categoryId)
                .Select(x => new GetCategory
                {

                    ProductName = x.ProductName,
                    TotalProduct = x.TotalProduct

                })
                .OrderBy(x => x.ProductName)
                .ToListAsync();

            return rawSQL;
        }
        public  async Task<IEnumerable<TotalProductBySub>> GetTotalProductBySub(string subcategory)
        {
            var categoryId = new SqlParameter("@Id", subcategory);
            var rawSQL = await _dbContext.GetTotalProductBySub
                .FromSqlRaw("select (p.Name)Subcategory, count(s.ProductSubcategoryID)TotalProduct  " +
                "from Production.ProductSubcategory s join Production.Product p " +
                "on s.ProductSubcategoryID= p.ProductSubcategoryID " +
                "where s.Name = @Id group by p.Name", categoryId)
                .Select(x => new TotalProductBySub
                {
                    Subcategory = x.Subcategory,
                    TotalProduct = x.TotalProduct
                })
                .OrderBy(x => x.Subcategory)
                .ToListAsync();

            return rawSQL;
        }

        public void insert(ProductCategory productCategory)
        {
            Create(productCategory);
        }

        public void remove(ProductCategory productCategory)
        {
            Delete(productCategory);
        }

        public async Task<IEnumerable<GetSubcategory>> GetSubcategoriesById(string subcategory)
        {
            var rawSQL = await _dbContext.GetSubcategoriesById
               .FromSqlRaw("select (s.Name)SubCategory,COUNT(ProductID)TotalProduct " +
               "from Production.ProductSubcategory s join Production.Product p " +
               "on s.ProductSubcategoryID=p.ProductSubcategoryID group by s.Name")
               .Select(x => new GetSubcategory
               {
                   SubCategory = x.SubCategory,
                   TotalProduct = x.TotalProduct

               })
               .OrderBy(x => x.SubCategory)
               .ToListAsync();

            return rawSQL;
        }
    }
}
