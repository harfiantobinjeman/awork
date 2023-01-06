using AutoMapper;
using AWork.Contracts.Dto.HumanResources;
using AWork.Contracts.Dto.HumanResources.Employee;
using AWork.Contracts.Dto.HumanResources.EmployeePayHistory;
using AWork.Contracts.Dto.PersonModule;
using AWork.Contracts.Dto.Sales.SalesPerson;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction.HumanResource;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.HumanResources
{
    public class EmployeeService : IEmployeeService
    {
        // ======= depedency injectorr ========== ///

        private readonly IHRRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public EmployeeService(IHRRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            this._mapper = mapper;
        }

        // ========================================= //

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployee(bool trackChanges)
        {
            var employeeModel = await _repositoryManager.EmployeeRepository.GetAllEmployee(trackChanges);
            // source = categoryModel, target categoryDto
            var employeeDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeeModel);
            return employeeDto;
        }

        public async Task<EmployeeDto> GetEmployeeDtoById(int BusinessEntityId, bool trackChange)
        {
            var employeeModel = await _repositoryManager.EmployeeRepository.GetEmployeeById(BusinessEntityId, trackChange);
            // source = categoryModel, target categoryDto
            var employeeDto = _mapper.Map<EmployeeDto>(employeeModel);
            return employeeDto;
        }

        public void Insert(EmployeeForCreateDto employee)
        {
            var employeeIn = _mapper.Map<Employee>(employee);
            _repositoryManager.EmployeeRepository.Insert(employeeIn);
            _repositoryManager.Save();
        }

        public void Remove(EmployeeDto employee)
        {
            var employeeModel = _mapper.Map<Employee>(employee);
            _repositoryManager.EmployeeRepository.Remove(employeeModel);
            _repositoryManager.Save();
        }

        public void Edit(EmployeeDto employee)
        {
            var employeeModel = _mapper.Map<Employee>(employee);
            _repositoryManager.EmployeeRepository.Edit(employeeModel);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeePaged(int pageIndex, int pageSize, bool trackChanges)
        {
            var employeeModel = await _repositoryManager.EmployeeRepository.GetEmployeePaged(pageIndex, pageSize, trackChanges);
            var employeeDto = _mapper.Map <IEnumerable<EmployeeDto>>(employeeModel);
            return employeeDto;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeeDtoByPerson(bool trackChanges)
        {
            var employeeModel = await _repositoryManager.EmployeeRepository.GetEmployeeByPerson(trackChanges);
            var employeeDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeeModel);
            return employeeDto;
        }

        public async Task<IEnumerable<PersonDto>> GetPersonByType(bool trackChange)
        {
            var personModel = await _repositoryManager.EmployeeRepository.GetPersonByType(trackChange);
            var personTypeDto = _mapper.Map<IEnumerable<PersonDto>>(personModel);
            return personTypeDto;
        }

        public async Task<IEnumerable<EmployeeDto>> GetJobTitle(bool trackChange)
        {
            var titleModel = await _repositoryManager.EmployeeRepository.GetJobTitle(trackChange);
            var jobTypeDto = _mapper.Map<IEnumerable<EmployeeDto>>(titleModel);
            return jobTypeDto;
        }

        public void create3table(EmployeeForCreateDto employeeForCreateDto, EmployeeDepartmentHistoryForCreateDto departmentHistoryForCreateDto, EmployeePHForCreateDto employeePHForCreateDto)
        {
            var insertEmployee = _mapper.Map<Employee>(employeeForCreateDto);
            _repositoryManager.EmployeeRepository.Insert(insertEmployee);
            _repositoryManager.Save();

            var insertEDepartment = _mapper.Map<EmployeeDepartmentHistory>(departmentHistoryForCreateDto);
            _repositoryManager.EmployeeDepartmentHistoryRepository.Insert(insertEDepartment);
            _repositoryManager.Save();

            var insertEPH = _mapper.Map<EmployeePayHistory>(employeePHForCreateDto);
            _repositoryManager.EmployeePayHistoryRepository.Insert(insertEPH);
            _repositoryManager.Save();
        }

        public void edit3table(EmployeeDto employeeDto, EmployeeDepartmentHistoryDto departmentHistoryDto, EmployeePayHistoryDto employeePHDto)
        {
            var employeeModel = _mapper.Map<Employee>(employeeDto);
            _repositoryManager.EmployeeRepository.Edit(employeeModel);
            _repositoryManager.Save();

            var updateEmployeDepart = _mapper.Map<EmployeeDepartmentHistory>(departmentHistoryDto);
            _repositoryManager.EmployeeDepartmentHistoryRepository.Edit(updateEmployeDepart);
            _repositoryManager.Save();

            var updateEPH = _mapper.Map<EmployeePayHistory>(employeePHDto);
            _repositoryManager.EmployeePayHistoryRepository.Edit(updateEPH);
            _repositoryManager.Save();
        }
    }
}
