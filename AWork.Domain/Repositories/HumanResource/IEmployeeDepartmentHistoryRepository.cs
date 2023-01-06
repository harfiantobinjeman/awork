using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.HumanResource
{
    public interface IEmployeeDepartmentHistoryRepository
    {
        Task<IEnumerable<EmployeeDepartmentHistory>> GetAllEmployeeDepartmentHistories(bool trackChanges);
        Task<EmployeeDepartmentHistory> GetEmployeeDepartmentHistoryById(int BusinessEntityId, bool trackChanges);
      
        void Insert(EmployeeDepartmentHistory employeeDepartmentHistory);
        void Edit(EmployeeDepartmentHistory employeeDepartmentHistory);

        void Remove(EmployeeDepartmentHistory employeeDepartmentHistory);
    }
}
