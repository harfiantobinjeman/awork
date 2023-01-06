using AWork.Contracts.Dto.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Production
{
    public interface IProductReviewService
    {
        Task<IEnumerable<ProductReviewDto>> GetAllProductReview(bool trackChanges);
        Task<ProductReviewDto> GetProductReviewById(int ProductReviewId, bool trackChanges);
        void Insert(ProductReviewForCreateDto productReviewForCreateDto);
        void Edit(ProductReviewDto productReviewDto);
        void Remove(ProductReviewDto productReviewDto);
    }
}
