using AWork.Contracts.Dto.HumanResources.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.HumanResources.JobCandidate
{
    public class JobCandidateDto
    {
        public int JobCandidateId { get; set; }
        public int? BusinessEntityId { get; set; }
        public string Resume { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual EmployeeDto BusinessEntity { get; set; }
    }
}
