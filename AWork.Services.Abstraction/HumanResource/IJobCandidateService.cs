using AWork.Contracts.Dto.HumanResources.Employee;
using AWork.Contracts.Dto.HumanResources.JobCandidate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.HumanResource
{
    public interface IJobCandidateService
    {
        Task<IEnumerable<JobCandidateDto>> GetAllJobCandidate(bool trackChanges);

        Task<JobCandidateDto> GetJobCandidateById(int BusinessEntityId, bool trackChange);
        void Insert(JobCandidateForCreateDto jobCandidate);
        void Edit(JobCandidateDto jobCandidate);
        void Remove(JobCandidateDto jobCandidate);
    }
}
