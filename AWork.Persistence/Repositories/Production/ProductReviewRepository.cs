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
    public class ProductReviewRepository : RepositoryBase<ProductReview>, IProductReviewRepository
    {
        public ProductReviewRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(ProductReview productReview)
        {
            Update(productReview);
        }

        public async Task<IEnumerable<ProductReview>> GetAllProductReview(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(p =>
              p.ReviewerName).Include(s => s.Product).ToListAsync();
        }

        public async Task<ProductReview> GetProductReviewById(int productReviewId, bool trackChanges)
        {
            return await FindByCondition(p => p.ProductReviewId.Equals(productReviewId), trackChanges)
                .Include(e => e.Product).SingleOrDefaultAsync();
        }

        public void Insert(ProductReview productReview)
        {
            Create(productReview);
        }

        public void Remove(ProductReview productReview)
        {
            Delete(productReview);
        }
    }
}
