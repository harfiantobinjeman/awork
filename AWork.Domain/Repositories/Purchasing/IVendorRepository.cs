using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Purchasing
{
    public interface IVendorRepository
    {
        Task<IEnumerable<Vendor>> GetAllVendor(bool trackChanges);

        Task<IEnumerable<Vendor>> GetVendorPaged(int pageIndex, int pageSize, bool trackChanges);

        Task<Vendor> GetVendorById(int id, bool trackChanges);

        void Insert(Vendor vendor);

        void Edit(Vendor vendor);

        void Remove(Vendor vendor);
        Task<IEnumerable<Vendor>> FindVendorByName(string vendorName, bool trackChanges);
    }
}
