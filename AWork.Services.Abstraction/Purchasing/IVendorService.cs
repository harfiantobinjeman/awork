using AWork.Contracts.Dto.Production;
using AWork.Contracts.Dto.Purchasing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Purchasing
{
    public interface IVendorService
    {
        Task<IEnumerable<VendorDto>> GetAllVendor(bool trackChanges);

        Task<IEnumerable<VendorDto>> GetVendorPaged(int pageIndex, int pageSize, bool trackChanges);

        Task<VendorDto> GetVendorById(int vendorId, bool trackChanges);

        void Insert(VendorForCreateDto vendorForCreateDto);

        void Edit(VendorDto vendorDto);

        void Remove(VendorDto vendorDto);
    }
}
