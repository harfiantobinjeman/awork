using AWork.Contracts.Dto.PersonModule;
using AWork.Contracts.Dto.Sales.SalesPerson;
using AWork.Contracts.Dto.Sales.Store;
using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Sales
{
    public interface ISalesPersonService
    {
        Task<IEnumerable<SalesPersonDto>> GetAllSalesPerson(bool trackChanges);

        Task<IEnumerable<PersonDto>> GetSalesPersonNotExistsEmployee(bool trackChanges);

        Task<SalesPersonDto> GetSalesPersonById(int bussinessEntityId, bool trackChanges);

        Task<SalesPersonGroupEditDto> GetSalesPersonEditById(int bussinessEntityId, bool trackChanges);

        void CreateSalesPersonStore(SalesPersonForCreateDto salesPersonForCreateDto, List<StoreForCreateDto> storesForCreate);

        void DeleteSalesPersonStore(SalesPersonDto salesPerson, List<StoreDto> stores);

        void EditSalesPersonStore(SalesPersonDto salesPersonDto, List<StoreDto> storesDto);

        void Insert(SalesPersonForCreateDto salesPersonForCreateDto);

        void Edit(SalesPersonDto salesPersonDto);

        void Remove(SalesPersonDto salesPersonDto);
    }
}
