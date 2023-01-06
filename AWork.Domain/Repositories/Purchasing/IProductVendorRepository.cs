using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Purchasing
{
    public interface IProductVendorRepository
    {
        Task<IEnumerable<ProductVendor>> GetAllProductVendor(bool trackChanges);
        Task<ProductVendor> GetProductVendorById(int id, bool trackChanges);
        Task<IEnumerable<ProductVendor>> FilterEmplid(int empId, bool trackChanges);
        void Insert(ProductVendor productVendor);
        void Edit(ProductVendor productVendor);
        void Remove(ProductVendor productVendor);
    }
}
