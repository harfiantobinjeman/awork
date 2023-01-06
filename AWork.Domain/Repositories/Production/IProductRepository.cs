using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AWork.Domain.Models;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Production
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProduct(bool trackChanges);

        Task<Product> GetProductById(int ProductId, bool trackChanges);

        Task<IEnumerable<Product>> GetProductPaged(int pageIndex, int pageSize, bool trackChanges);

        void Insert(Product product);
        void Edit(Product product);
        void Remove(Product product);
    }
}
