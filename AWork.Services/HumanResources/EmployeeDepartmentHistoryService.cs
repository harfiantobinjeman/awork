using AutoMapper;
using AWork.Contracts.Dto.HumanResources;
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
    internal class EmployeeDepartmentHistoryService : IEmployeeDepartmentHistoryService
    {
        private IHRRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public EmployeeDepartmentHistoryService(IHRRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDepartmentHistoryDto>> GetAllEmployeeDepartmentHistory(bool trackChanges)
        {
            var employeeDepartmentHistory = await _repositoryManager
                .EmployeeDepartmentHistoryRepository
                .GetAllEmployeeDepartmentHistories(trackChanges);
            var employeeDepartmentHistoryDto = _mapper.Map<IEnumerable<EmployeeDepartmentHistoryDto>>(employeeDepartmentHistory);
            return employeeDepartmentHistoryDto;
        }

        public async Task<EmployeeDepartmentHistoryDto> GetEmployeeDepartmentHistoryById(int BusinessEntityId, bool trackChanges)
        {
            var employeeDepartmentHistory = await _repositoryManager
                .EmployeeDepartmentHistoryRepository
                .GetEmployeeDepartmentHistoryById(BusinessEntityId, trackChanges);
            var employeeDepartmentHistoryDto = _mapper.Map<EmployeeDepartmentHistoryDto>(employeeDepartmentHistory);
            return employeeDepartmentHistoryDto;
        }

        public void Insert(EmployeeDepartmentHistoryForCreateDto employeeDepartmentHistoryForCreateDto)
        {
            var employeeDepartmentHistory = _mapper.Map<EmployeeDepartmentHistory>(employeeDepartmentHistoryForCreateDto);
            _repositoryManager.EmployeeDepartmentHistoryRepository.Insert(employeeDepartmentHistory);
            _repositoryManager.Save();
        }

        public void Edit(EmployeeDepartmentHistoryDto employeeDepartmentHistoryDto)
        {
            var employeeDepartmentHistory = _mapper.Map<EmployeeDepartmentHistory>(employeeDepartmentHistoryDto);
            _repositoryManager.EmployeeDepartmentHistoryRepository.Edit(employeeDepartmentHistory);
            _repositoryManager.Save();
        }

        public void Delete(EmployeeDepartmentHistoryDto employeeDepartmentHistoryDto)
        {
            var employeeDepartmentHistory = _mapper.Map<EmployeeDepartmentHistory>(employeeDepartmentHistoryDto);
            _repositoryManager.EmployeeDepartmentHistoryRepository.Remove(employeeDepartmentHistory);
            _repositoryManager.Save();
        }

    }
}
