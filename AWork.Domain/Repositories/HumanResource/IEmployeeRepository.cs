using AWork.Domain.Dto;
using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.HumanResource
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployee(bool trackChanges);
        Task<Employee> GetEmployeeById(int BusinessEntityId, bool trackChange);
        Task<IEnumerable<Employee>> GetEmployeePaged(int pageIndex, int pageSize, bool trackChanges);

        public Task<IEnumerable<Person>> GetPersonByType(bool trackChange);

        public Task<IEnumerable<JobTitleType>> GetJobTitle(bool trackChange);

        /*Task<IEnumerable<string>> GetJobTitle(bool trackChange);*/

        //Task<Employee> GetEmployeeById(int BusinessEntityId, bool trackChange);

        Task<IEnumerable<Employee>> GetEmployeeByPerson(bool trackChange);
        IEnumerable<JmlhEmployeTiapDepartement> GetEmployeeTiapDepartement();
        IEnumerable<SalaryRateEmployeeDto> GetSalaryRateEmp();
        void Insert(Employee employee);
        void Edit(Employee employee);
        void Remove(Employee employee);
    }
}
