using AWork.Contracts.Dto.HumanResources;
using AWork.Contracts.Dto.HumanResources.Employee;
using AWork.Contracts.Dto.HumanResources.EmployeePayHistory;
using AWork.Contracts.Dto.PersonModule;
using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.HumanResource
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployee(bool trackChanges);
        Task<EmployeeDto> GetEmployeeDtoById(int BusinessEntityId, bool trackChange);
        Task<IEnumerable<EmployeeDto>> GetEmployeePaged(int pageIndex, int pageSize, bool trackChanges);
/*
        Task<EmployeeEditGroupDto> EmployeeEditGroupDto(int BusinessEntityId, bool trackChange);*/

        Task<IEnumerable<PersonDto>> GetPersonByType(bool trackChange);
        Task<IEnumerable<EmployeeDto>> GetJobTitle(bool trackChange);

        //Task<EmployeeDto> GetEmployeeDtoById(int BusinessEntityId, bool trackChange);

        Task<IEnumerable<EmployeeDto>> GetEmployeeDtoByPerson(bool trackChanges);

        void create3table(EmployeeForCreateDto employeeForCreateDto, EmployeeDepartmentHistoryForCreateDto departmentHistoryForCreateDto, EmployeePHForCreateDto employeePHForCreateDto);
        void edit3table(EmployeeDto employeeDto, EmployeeDepartmentHistoryDto departmentHistoryDto, EmployeePayHistoryDto employeePHDto);
        void Insert(EmployeeForCreateDto employee);
        void Edit(EmployeeDto employee);
        void Remove(EmployeeDto employee);
    }
}
