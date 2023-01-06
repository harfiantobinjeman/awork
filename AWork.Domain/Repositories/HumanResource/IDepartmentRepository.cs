using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.HumanResource
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartment(bool trackChanges);
        Task<Department> GetDepartmentById(short departmentId, bool trackChanges);
        Task<IEnumerable<Department>> GetDepartmentPaged(int pageIndex, int pageSize, bool trackChanges);

        void Insert(Department department);

        void Edit(Department department);

        void Remove(Department department);


    }
}
