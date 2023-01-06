using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Production
{
    public interface IProductSubCategoryRepository
    {
        //trackChanges => feature untuk mendekteksi perubahan data diobject category
        Task<IEnumerable<ProductSubcategory>> GetAllProdcSubCategory(bool trackChanges);

        //craete 1 record with this code
        Task<ProductSubcategory> GetProdcSubCateById(int ProductSubcategoryId, bool trackChanges);
        Task<IEnumerable<ProductSubcategory>> GetProCateInSubByID(int ProductCategoryId, bool trackChanges);
        void insert(ProductSubcategory productSubCategory);
        void edit(ProductSubcategory productSubCategory);
        void remove(ProductSubcategory productSubCategory);
    }
}
