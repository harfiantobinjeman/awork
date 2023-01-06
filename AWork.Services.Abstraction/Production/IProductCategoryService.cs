using AWork.Contracts.Dto.Production;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Production
{
    public interface IProductCategoryService
    {
        //trackChanges => feature untuk mendekteksi perubahan data diobject category
        Task<IEnumerable<ProductCategoryDto>> GetAllProdcCategory(bool trackChanges);

        //craete 1 record with this code
        Task<ProductCategoryDto> GetProcdCateById(int prodcCategory, bool trackChanges);
        void Insert(ProductCategoryForCreatDto productCategoryForCreat);
        void Edit(ProductCategoryDto productCategoryDto);
        void Remove(ProductCategoryDto productCategoryDto);
    }
}
