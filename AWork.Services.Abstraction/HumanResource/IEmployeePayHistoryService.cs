using AWork.Contracts.Dto.HumanResources.Employee;
using AWork.Contracts.Dto.HumanResources.EmployeePayHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.HumanResource
{
    public interface IEmployeePayHistoryService
    {
        Task<IEnumerable<EmployeePayHistoryDto>> GetAllEmployeePayHistory(bool trackChanges);

        Task<EmployeePayHistoryDto> GetEmployeePayHistoryDtoById(int BusinessEntityId, bool trackChange);
        void Insert(EmployeePHForCreateDto employeePayHistory);
        void Edit(EmployeePayHistoryDto employeePayHistory);
        void Remove(EmployeePayHistoryDto employeePayHistory);
    }
}
