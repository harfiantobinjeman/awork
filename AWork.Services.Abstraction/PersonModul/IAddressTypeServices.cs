using AWork.Contracts.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.PersonModul
{
    public interface IAddressTypeServices
    {
        Task<IEnumerable<AddressTypeDto>> GetAllAddressType(bool trackChanges);
        Task<AddressTypeDto> GetAllAddressTypeById(int addressTypeId, bool trackChanges);
        void Insert(AddressTypeForCreateDto addressTypeForCreateDto);
        void Edit(AddressTypeDto addressTypeDto);
        void Remove(AddressTypeDto addressTypeDto);
    }
}
