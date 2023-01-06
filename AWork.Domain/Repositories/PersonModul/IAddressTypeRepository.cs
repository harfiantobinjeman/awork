using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.PersonModul
{
    public interface IAddressTypeRepository
    {
        Task<IEnumerable<AddressType>> GetAllAddressType(bool trackChanges);

        Task<AddressType> GetAddressTypeId(int addressTypeId, bool trackChanges);

        void Insert(AddressType addressType);

        void Edit(AddressType addressType);

        void Remove(AddressType addressType);
    }
}
