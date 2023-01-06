using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.PersonModul
{
    public interface IBusinessEntityAddressRepository
    {
        Task<IEnumerable<BusinessEntityAddress>> GetAllBusinessEntityAddress(bool trackChanges);

        Task<BusinessEntityAddress> GetBusinessEntityAddressById(int businessEntityAddressId, int addressID, bool trackChanges);

        Task<BusinessEntityAddress>GetBusinessEntityAddressByBusinessEntityId(int businessEntityAddressId, bool trackChanges);

        void Insert(BusinessEntityAddress businessEntityAddress);

        void Edit(BusinessEntityAddress businessEntityAddress);

        void Remove(BusinessEntityAddress businessEntityAddress);
    }
}
