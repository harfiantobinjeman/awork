using AWork.Contracts.Dto.HumanResources.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.HumanResources.EmployeePayHistory
{
    public class EmployeePayHistoryDto
    {
        public int BusinessEntityId { get; set; }
        public DateTime RateChangeDate { get; set; }
        public decimal Rate { get; set; }
        public byte PayFrequency { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual EmployeeDto BusinessEntity { get; set; }
    }
}
