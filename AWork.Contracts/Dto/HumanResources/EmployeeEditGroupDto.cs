using AWork.Contracts.Dto.HumanResources.Employee;
using AWork.Contracts.Dto.HumanResources.EmployeePayHistory;
using AWork.Contracts.Dto.PersonModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.HumanResources
{
    public class EmployeeEditGroupDto
    {
        public EmployeeDto employeeDtos { get; set; }
        public EmployeeDepartmentHistoryDto employeeDepartmentDtos { get; set; }
        public EmployeePayHistoryDto employeePHDtos { get; set; }
    }
}
