using AWork.Contracts.Dto.HumanResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.HumanResource
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartment(bool trackChanges);

        Task<DepartmentDto> GetDepartmentById(short departmentId, bool trackChanges);

        Task<IEnumerable<DepartmentDto>> GetDepartmentPaged(int pageIndex, int pageSize, bool trackChanges);

        void insert(DepartmentForCreateDto departmentForCreateDto);
        void edit(DepartmentDto departmentDto);
        void delete(DepartmentDto departmentDto);
    }
}
