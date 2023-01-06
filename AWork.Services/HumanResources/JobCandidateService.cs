using AutoMapper;
using AWork.Contracts.Dto.HumanResources.Employee;
using AWork.Contracts.Dto.HumanResources.JobCandidate;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction.HumanResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.HumanResources
{
    public class JobCandidateService : IJobCandidateService
    {
        // ======= depedency injectorr ========== ///

        private readonly IHRRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public JobCandidateService(IHRRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            this._mapper = mapper;
        }

        // ========================================= //
        public async Task<IEnumerable<JobCandidateDto>> GetAllJobCandidate(bool trackChanges)
        {
            var jobModel = await _repositoryManager.JobCandidateRepository.GetAllJobCandidate(trackChanges);
            // source = categoryModel, target categoryDto
            var jobCandidateDto = _mapper.Map<IEnumerable<JobCandidateDto>>(jobModel);
            return jobCandidateDto;
        }

        public async Task<JobCandidateDto> GetJobCandidateById(int BusinessEntityId, bool trackChange)
        {
            var jobModel = await _repositoryManager.JobCandidateRepository.GetJobCandidateById(BusinessEntityId, trackChange);
            // source = categoryModel, target categoryDto
            var jobDto = _mapper.Map<JobCandidateDto>(jobModel);
            return jobDto;
        }

        public void Insert(JobCandidateForCreateDto jobCandidate)
        {
            var jobIn = _mapper.Map<JobCandidate>(jobCandidate);
            _repositoryManager.JobCandidateRepository.Insert(jobIn);
            _repositoryManager.Save();
        }

        public void Edit(JobCandidateDto jobCandidate)
        {
            var jobDel = _mapper.Map<JobCandidate>(jobCandidate);
            _repositoryManager.JobCandidateRepository.Edit(jobDel);
            _repositoryManager.Save();
        }

        public void Remove(JobCandidateDto jobCandidate)
        {
            var jobDel = _mapper.Map<JobCandidate>(jobCandidate);
            _repositoryManager.JobCandidateRepository.Remove(jobDel);
            _repositoryManager.Save();
        }
    }
}
