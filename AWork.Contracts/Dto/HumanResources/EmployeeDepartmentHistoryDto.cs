using AWork.Contracts.Dto.HumanResources.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.HumanResources
{
    public class EmployeeDepartmentHistoryDto
    {
        public int BusinessEntityId { get; set; }
        public short DepartmentId { get; set; }
        public byte ShiftId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual EmployeeDto BusinessEntity { get; set; }
        public virtual DepartmentDto Department { get; set; }
        public virtual ShiftDto Shift { get; set; }

    }
}
