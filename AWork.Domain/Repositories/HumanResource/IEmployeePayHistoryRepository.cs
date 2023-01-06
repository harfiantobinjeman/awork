using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.HumanResource
{
    public interface IEmployeePayHistoryRepository
    {
        Task<IEnumerable<EmployeePayHistory>> GetAllEmployeePayHistory(bool trackChanges);

        Task<EmployeePayHistory> GetEmployeePayHistoryById(int BusinessEntityId, bool trackChange);
        void Insert(EmployeePayHistory employeePayHistory);
        void Edit(EmployeePayHistory employeePayHistory);
        void Remove(EmployeePayHistory employeePayHistory);
    }
}
