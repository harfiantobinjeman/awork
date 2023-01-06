using AWork.Services.Abstraction.HumanResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction
{
    public interface IHumanResourceServiceManager
    {
        IDepartmentService DepartmentService { get; }

        IShiftService ShiftService { get; }

        IEmployeeDepartmentHistoryService EmployeeDepartmentHistoryService { get; }
        IEmployeeService EmployeeService { get; }
        IEmployeePayHistoryService EmployeePayHistoryService { get; }
        IJobCandidateService JobCandidateService { get; }
    }
}
