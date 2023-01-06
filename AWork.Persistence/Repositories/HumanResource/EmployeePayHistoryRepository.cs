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
    public class EmployeePayHistoryRepository : RepositoryBase<EmployeePayHistory>, IEmployeePayHistoryRepository
    {
        public EmployeePayHistoryRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<EmployeePayHistory>> GetAllEmployeePayHistory(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.BusinessEntityId)
                .Include(p => p.BusinessEntity)
                .ToListAsync();
        }

        public async Task<EmployeePayHistory> GetEmployeePayHistoryById(int businessEntityId, bool trackChanges)
        {
            return await FindByCondition(c => c.BusinessEntityId.Equals(businessEntityId), trackChanges)
                .Include(p => p.BusinessEntity)
                .SingleOrDefaultAsync();
        }

        public void Insert(EmployeePayHistory employeePayHistory)
        {
            Create(employeePayHistory);
        }
        public void Edit(EmployeePayHistory employeePayHistory)
        {
            Update(employeePayHistory);
        }

        public void Remove(EmployeePayHistory employeePayHistory)
        {
            Delete(employeePayHistory);
        }
    }
}
