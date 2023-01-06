using AWork.Contracts.Dto.Production;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Production
{
    public interface IProductSubCategoryService
    {
        //trackChanges => feature untuk mendekteksi perubahan data diobject category
        Task<IEnumerable<ProductSubCategoryDto>> GetAllProdcSubCategory(bool trackChanges);

        //craete 1 record with this code
        Task<ProductSubCategoryDto> GetProcdSubCateById(int ProductSubcategoryId, bool trackChanges);
        Task<IEnumerable<ProductSubCategoryDto>> GetProCateInSubByID(int ProductCategoryId, bool trackChanges);
        void Insert(ProductSubCategoryForCreateDto productSubCategoryForCreateDto);
        void Edit(ProductSubCategoryDto productSubCategoryDto);
        void Remove(ProductSubCategoryDto productSubCategoryDto);
    }
}
