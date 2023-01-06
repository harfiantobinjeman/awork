using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Production
{
    public interface IProductReviewRepository
    {
        Task<IEnumerable<ProductReview>> GetAllProductReview(bool trackChanges);

        Task<ProductReview> GetProductReviewById(int productReviewId, bool trackChanges);

        void Insert(ProductReview productReview);
        void Edit(ProductReview productReview);
        void Remove(ProductReview productReview);
    }
}
