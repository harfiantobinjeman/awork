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
    internal class DepartmentService : IDepartmentService
    {
        private IHRRepositoryManager _iHRRepositoryManager;
        private readonly IMapper _mapper;

        public DepartmentService(IHRRepositoryManager iHRRepositoryManager, IMapper mapper)
        {
            _iHRRepositoryManager = iHRRepositoryManager;
            _mapper = mapper;
        }

        public void delete(DepartmentDto departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);
            _iHRRepositoryManager.DepartmentRepository.Remove(department);
            _iHRRepositoryManager.Save();
        }

        public void edit(DepartmentDto departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);
            _iHRRepositoryManager.DepartmentRepository.Edit(department);
            _iHRRepositoryManager.Save();
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartment(bool trackChanges)
        {
            var department = await _iHRRepositoryManager.DepartmentRepository.GetAllDepartment(trackChanges);
            var departmentDto = _mapper.Map<IEnumerable<DepartmentDto>>(department);
            return departmentDto;
        }

        public async Task<DepartmentDto> GetDepartmentById(short DepartmentId, bool trackChanges)
        {
            var department = await _iHRRepositoryManager.DepartmentRepository.GetDepartmentById(DepartmentId, trackChanges);
            var departmentDto = _mapper.Map<DepartmentDto>(department);
            return departmentDto;
        }

        public void insert(DepartmentForCreateDto departmentForCreateDto)
        {
            var department = _mapper.Map<Department>(departmentForCreateDto);
            _iHRRepositoryManager.DepartmentRepository.Insert(department);
            _iHRRepositoryManager.Save();
        }

        public async Task<IEnumerable<DepartmentDto>> GetDepartmentPaged(int pageIndex, int pageSize, bool trackChanges)
        {
            var department = await _iHRRepositoryManager.DepartmentRepository.GetDepartmentPaged(pageIndex, pageSize, trackChanges);
            var departmentDto = _mapper.Map<IEnumerable<DepartmentDto>>(department);
            return departmentDto;
        }
    }
}
