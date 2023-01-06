using AutoMapper;
using AWork.Domain.Base;
using AWork.Services.Abstraction;
using AWork.Services.Abstraction.HumanResource;
using AWork.Services.Abstraction.PersonModul;
using AWork.Services.HumanResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services
{
    public class HumanResourceServiceManager : IHumanResourceServiceManager
    {
        private readonly Lazy<IDepartmentService> _lazyDepartmentService;
        private readonly Lazy<IShiftService> _lazyShiftService;
        private readonly Lazy<IEmployeeDepartmentHistoryService> _lazyEmployeeDepartmentHistoryService;
        private readonly Lazy<IEmployeeService> _lazyEmployeeServices;
        private readonly Lazy<IEmployeePayHistoryService> _lazyEmployeePayHistoryServices;
        private readonly Lazy<IJobCandidateService> _lazyJobCandidateServices;

        public HumanResourceServiceManager(IHRRepositoryManager repositoryManager, IMapper mapper)
        {
            _lazyDepartmentService = new Lazy<IDepartmentService>(
               () => new DepartmentService(repositoryManager, mapper)
               );

            _lazyEmployeeServices = new Lazy<IEmployeeService>(
                () => new EmployeeService(repositoryManager, mapper)
               );

            _lazyShiftService = new Lazy<IShiftService>(
               () => new ShiftService(repositoryManager, mapper)
               );
            _lazyEmployeePayHistoryServices = new Lazy<IEmployeePayHistoryService>(
                () => new EmployeePayHistoryService(repositoryManager, mapper)
               );

            _lazyEmployeeDepartmentHistoryService = new Lazy<IEmployeeDepartmentHistoryService>(
               () => new EmployeeDepartmentHistoryService(repositoryManager, mapper)
               );
            _lazyJobCandidateServices = new Lazy<IJobCandidateService>(
                () => new JobCandidateService(repositoryManager, mapper)
               );
        }

        public IDepartmentService DepartmentService => _lazyDepartmentService.Value;
        public IEmployeeService EmployeeService => _lazyEmployeeServices.Value;

        public IShiftService ShiftService => _lazyShiftService.Value;
        public IEmployeePayHistoryService EmployeePayHistoryService => _lazyEmployeePayHistoryServices.Value;

        public IEmployeeDepartmentHistoryService EmployeeDepartmentHistoryService => _lazyEmployeeDepartmentHistoryService.Value;
        public IJobCandidateService JobCandidateService => _lazyJobCandidateServices.Value;
    }
}
