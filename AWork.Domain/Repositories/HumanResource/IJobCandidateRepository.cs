using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.HumanResource
{
    public interface IJobCandidateRepository
    {
        Task<IEnumerable<JobCandidate>> GetAllJobCandidate(bool trackChanges);

        Task<JobCandidate> GetJobCandidateById(int BusinessEntityId, bool trackChange);
        void Insert(JobCandidate jobCandidate);
        void Edit(JobCandidate jobCandidate);
        void Remove(JobCandidate jobCandidate);
    }
}
