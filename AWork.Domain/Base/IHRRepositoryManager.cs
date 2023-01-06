using AWork.Domain.Repositories.HumanResource;

using AWork.Domain.Repositories.Sales;
using System.Threading.Tasks;

namespace AWork.Domain.Base
{
    public interface IHRRepositoryManager
    {
        IDepartmentRepository DepartmentRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IEmployeeDepartmentHistoryRepository EmployeeDepartmentHistoryRepository { get; }
        IJobCandidateRepository JobCandidateRepository { get; }
        IShiftRepository ShiftRepository { get; }
        IEmployeePayHistoryRepository EmployeePayHistoryRepository { get; }

        void Save();

        Task SaveAsync();
    }
}
