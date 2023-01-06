using AWork.Domain.Dto;
using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Sales
{
    public interface ISalesPersonRepository
    {
        Task<IEnumerable<SalesPerson>> GetAllSalesPerson(bool trackChanges);

        Task<IEnumerable<Person>> GetSalesPersonNotExistsEmployee(bool trackChanges);

        Task<IEnumerable<PersonEmployeeData>> GetDataEmployeeTypeSP();

        Task<IEnumerable<GetDataTerritory>> GetDataTerritoryByName();

        Task<SalesPerson> GetSalesPersonById(int businessEntityId, bool trackChanges);

        void Insert(SalesPerson salesTeritory);

        void Edit(SalesPerson salesTeritory);

        void Remove(SalesPerson salesTeritory);
    }
}
