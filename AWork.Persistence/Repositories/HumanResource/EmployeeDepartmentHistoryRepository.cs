using AWork.Domain.Models;
using AWork.Domain.Repositories.HumanResource;
using AWork.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.HumanResource
{
    public class EmployeeDepartmentHistoryRepository : RepositoryBase<EmployeeDepartmentHistory>, IEmployeeDepartmentHistoryRepository
    {
        private short departmentId;

        public EmployeeDepartmentHistoryRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(EmployeeDepartmentHistory employeeDepartmentHistory)
        {
            Update(employeeDepartmentHistory);
        }

        public async Task<IEnumerable<EmployeeDepartmentHistory>> GetAllEmployeeDepartmentHistories(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(x => x.DepartmentId).ToListAsync();
        }

        public async Task<EmployeeDepartmentHistory> GetEmployeeDepartmentHistoryById(int Id, bool trackChanges)
        {
            return await FindByCondition(x => x.BusinessEntityId.Equals(Id), trackChanges)
                .SingleOrDefaultAsync();
        }

        public void Insert(EmployeeDepartmentHistory employeeDepartmentHistory)
        {
            Create(employeeDepartmentHistory);
        }

        public void Remove(EmployeeDepartmentHistory employeeDepartmentHistory)
        {
            Delete(employeeDepartmentHistory);
        }
    }
}
