using AWork.Domain.Dto;
using AWork.Domain.Dto.Production;
using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Production
{
    public interface IProductCategoryRepository
    {
        //trackChanges => feature untuk mendekteksi perubahan data diobject category
        Task<IEnumerable<ProductCategory>> GetAllProdcCategory(bool trackChanges);

        //craete 1 record with this code
        Task<ProductCategory> GetProcdCateById(int prodcCategory, bool trackChanges);
        Task<IEnumerable<TotalProductByCategory>> GetTotalProductByCategories(string category);
        Task<IEnumerable<TotalProductBySub>> GetTotalProductBySub(string subcategory);

       
        Task<IEnumerable<GetCategory>> GetCategoryById(string category);

        Task<IEnumerable<GetSubcategory>> GetSubcategoriesById(string subcategory);


        void insert(ProductCategory productCategory);
        void edit(ProductCategory productCategory);
        void remove(ProductCategory productCategory);
    }
}
