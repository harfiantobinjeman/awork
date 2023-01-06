using AWork.Contracts.Dto;
using AWork.Contracts.Dto.PersonModule;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.PersonModul
{
    public interface IAddressServices
    {
        Task<IEnumerable<AddressDto>> GetAllAddress(bool trackChanges);
        Task<AddressDto> GetAllAddressById(int addressId, bool trackChanges);
        void Insert(AddressForCreateDto addressForCreateDto, BusinessEntityAddressForCreateDto businessEntityAddressForCreateDto);
        void Edit(AddressDto addressDto);
        void Remove(AddressDto addressDto);
    }
}
