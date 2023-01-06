using AWork.Contracts.Dto.Purchasing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Purchasing
{
    public interface IProductVendorService
    {
        Task<IEnumerable<ProductVendorDto>> GetAllProductVendor(bool trackChanges);

        Task<ProductVendorDto> GetProductVendorById(int productVendorId, bool trackChanges);
        Task<IEnumerable<ProductVendorDto>> FilterEmpId(int empId, bool trackChanges);

        void Insert(ProductVendorDto productVendorForCreateDto);

        void Edit(ProductVendorDto productVendorDto);

        void Remove(ProductVendorDto productVendorDto);
    }
}
