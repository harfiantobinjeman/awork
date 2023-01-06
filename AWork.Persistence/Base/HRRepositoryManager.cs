using AWork.Domain.Base;
using AWork.Domain.Repositories.HumanResource;
using AWork.Domain.Repositories.PersonModul;
using AWork.Domain.Repositories.Sales;
using AWork.Persistence.Repositories.HumanResource;
using AWork.Persistence.Repositories.PersonModul;
using AWork.Persistence.Repositories.Sales;
using System.Threading.Tasks;

namespace AWork.Persistence.Base
{
    public class HRRepositoryManager : IHRRepositoryManager
    {
        private AdventureWorks2019Context _dbContext;
        private IDepartmentRepository _departmentRepository;
        private IShiftRepository _shiftRepository;
        private IEmployeeDepartmentHistoryRepository _employeeDepartmentHistoryRepository;
        private IEmployeeRepository _employeeRepository;
        private IEmployeePayHistoryRepository _employeePayHistoryRepository;
        private IJobCandidateRepository _jobCandidateRepository;
    


        public HRRepositoryManager(AdventureWorks2019Context dbContext)
        {
            _dbContext = dbContext;
        }
      
      
        public IEmployeeRepository EmployeeRepository
        {
            get
            {
                if (_employeeRepository == null)
                {
                    _employeeRepository = new EmployeeRepository(_dbContext);
                }
                return _employeeRepository;
            }

        }
   

        public IShiftRepository ShiftRepository
        {
            get
            {
                if (_shiftRepository == null)
                {
                    _shiftRepository = new ShiftRepository(_dbContext);
                }
                return _shiftRepository;
            }
        }

        public IDepartmentRepository DepartmentRepository
        {
            get
            {
                if (_departmentRepository == null)
                {
                    _departmentRepository = new DepartmentRepository(_dbContext);
                }
                return _departmentRepository;
            }
        }

        public IEmployeePayHistoryRepository EmployeePayHistoryRepository
        {
            get
            {
                if (_employeePayHistoryRepository == null)
                {
                    _employeePayHistoryRepository = new EmployeePayHistoryRepository(_dbContext);
                }
                return _employeePayHistoryRepository;
            }
        }


       
        public IJobCandidateRepository JobCandidateRepository
        {
            get
            {
                if (_jobCandidateRepository == null)
                {
                    _jobCandidateRepository = new JobCandidateRepository(_dbContext);
                }
                return _jobCandidateRepository;
            }
        }

        public IEmployeeDepartmentHistoryRepository EmployeeDepartmentHistoryRepository
        {
            get
            {
                if (_employeeDepartmentHistoryRepository == null)
                {
                    _employeeDepartmentHistoryRepository = new EmployeeDepartmentHistoryRepository(_dbContext);
                }
                return _employeeDepartmentHistoryRepository;
            }
        }

        public void Save() => _dbContext.SaveChanges();
        public async Task SaveAsync() => await _dbContext.SaveChangesAsync();
    }
}
