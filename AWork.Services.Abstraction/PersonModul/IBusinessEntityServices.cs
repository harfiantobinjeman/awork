using AWork.Contracts.Dto.PersonModule;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.PersonModul
{
    public interface IBusinessEntityServices
    {
        Task<IEnumerable<BusinessEntityDto>> GetAllBusinessEntity(bool trackChanges);

        Task<BusinessEntityDto> GetBusinessEntityById(int businessEntityid, bool trackChanges);

        BusinessEntityDto CreateBusinessEntity();

        void Insert(BusinessEntityForCreateDto businessEntityForCreateDto);
        void Edit(BusinessEntityDto businessEntityDto);
        void Delete(BusinessEntityDto businessEntityDto);
    }
}
