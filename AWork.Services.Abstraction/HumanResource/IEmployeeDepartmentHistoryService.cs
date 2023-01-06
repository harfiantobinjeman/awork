using AWork.Contracts.Dto.HumanResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.HumanResource
{
    public interface IEmployeeDepartmentHistoryService
    {
        Task<IEnumerable<EmployeeDepartmentHistoryDto>> GetAllEmployeeDepartmentHistory(bool trackChanges);
        Task<EmployeeDepartmentHistoryDto> GetEmployeeDepartmentHistoryById(int BusinessEntityId, bool trackChanges);

        void Insert(EmployeeDepartmentHistoryForCreateDto employeeDepartmentHistoryForCreateDto);
        void Edit(EmployeeDepartmentHistoryDto employeeDepartmentHistoryDto);
        void Delete(EmployeeDepartmentHistoryDto employeeDepartmentHistoryDto);
    }
}
