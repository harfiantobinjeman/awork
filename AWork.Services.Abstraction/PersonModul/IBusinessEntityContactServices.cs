using AWork.Contracts.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.PersonModul
{
    public interface IBusinessEntityContactServices
    {
        Task<IEnumerable<BusinessEntityContactDto>> GetAllBusinessEntityContact(bool trackChanges);

        Task<BusinessEntityContactDto> GetBusinessEntityContactById(int businessEntityContactId, bool trackChanges);

        void insert(BusinessEntityContactForCreateDto businessEntityContactForCreateDto);
        void edit(BusinessEntityContactDto businessEntityContactDto);
        void delete(BusinessEntityContactDto businessEntityContactDto);
    }
}
