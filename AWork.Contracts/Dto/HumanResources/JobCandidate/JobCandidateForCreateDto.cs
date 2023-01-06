using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.HumanResources.JobCandidate
{
    public class JobCandidateForCreateDto
    {
        public int JobCandidateId { get; set; }
        public string Resume { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
