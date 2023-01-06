using AWork.Domain.Models;
using AWork.Domain.Repositories.HumanResource;
using AWork.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.HumanResource
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(Department department)
        {
            Update(department);
        }

        public async Task<IEnumerable<Department>> GetAllDepartment(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(d => d.DepartmentId)
                .ToListAsync();
        }

        public async Task<Department> GetDepartmentById(short departmentId, bool trackChanges)
        {
            return await FindByCondition(d => d.DepartmentId.Equals(departmentId), trackChanges)
                .OrderBy(d => d.DepartmentId)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Department>> GetDepartmentPaged(int pageIndex, int pageSize, bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(p => p.DepartmentId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public void Insert(Department department)
        {
            Create(department);
        }

        public void Remove(Department department)
        {
            Delete(department);
        }
    }
}
