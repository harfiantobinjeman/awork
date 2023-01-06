using AWork.Domain.Models;
using AWork.Domain.Repositories.HumanResource;
using AWork.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.HumanResource
{
    public class JobCandidateRepository : RepositoryBase<JobCandidate>, IJobCandidateRepository
    {
        public JobCandidateRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }



        public async Task<IEnumerable<JobCandidate>> GetAllJobCandidate(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.JobCandidateId).ToListAsync();
        }

        public async Task<JobCandidate> GetJobCandidateById(int jobCandidateId, bool trackChanges)
        {
            return await FindByCondition(c => c.JobCandidateId.Equals(jobCandidateId), trackChanges).SingleOrDefaultAsync();
        }

        public void Insert(JobCandidate jobCandidate)
        {
            Create(jobCandidate);
        }

        public void Edit(JobCandidate jobCandidate)
        {
            Update(jobCandidate);
        }

        public void Remove(JobCandidate jobCandidate)
        {
            Delete(jobCandidate);
        }
    }
}
