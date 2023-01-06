using AutoMapper;
using AWork.Contracts.Dto.HumanResources.Employee;
using AWork.Contracts.Dto.HumanResources.EmployeePayHistory;
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
    public class EmployeePayHistoryService : IEmployeePayHistoryService
    {
        // ===========================  Injectorr  ================================

        private readonly IHRRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public EmployeePayHistoryService(IHRRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            this._mapper = mapper;
        }

        // ========================================================================
        public async Task<IEnumerable<EmployeePayHistoryDto>> GetAllEmployeePayHistory(bool trackChanges)
        {
            var employeeModel = await _repositoryManager.EmployeePayHistoryRepository.GetAllEmployeePayHistory(trackChanges);
            // source = categoryModel, target categoryDto
            var employeePHDto = _mapper.Map<IEnumerable<EmployeePayHistoryDto>>(employeeModel);
            return employeePHDto;
        }

        public async Task<EmployeePayHistoryDto> GetEmployeePayHistoryDtoById(int BusinessEntityId, bool trackChange)
        {
            var employeeModel = await _repositoryManager.EmployeePayHistoryRepository.GetEmployeePayHistoryById(BusinessEntityId, trackChange);
            // source = categoryModel, target categoryDto
            var employeePHDto = _mapper.Map<EmployeePayHistoryDto>(employeeModel);
            return employeePHDto;
        }

        public void Insert(EmployeePHForCreateDto employeePayHistory)
        {
            var employeePHIn = _mapper.Map<EmployeePayHistory>(employeePayHistory);
            _repositoryManager.EmployeePayHistoryRepository.Insert(employeePHIn);
            _repositoryManager.Save();
        }

        public void Edit(EmployeePayHistoryDto employeePayHistory)
        {
            var employeePHIn = _mapper.Map<EmployeePayHistory>(employeePayHistory);
            _repositoryManager.EmployeePayHistoryRepository.Edit(employeePHIn);
            _repositoryManager.Save();
        }

        public void Remove(EmployeePayHistoryDto employeePayHistory)
        {
            var employeePHDel = _mapper.Map<EmployeePayHistory>(employeePayHistory);
            _repositoryManager.EmployeePayHistoryRepository.Remove(employeePHDel);
            _repositoryManager.Save();
        }
    }
}
