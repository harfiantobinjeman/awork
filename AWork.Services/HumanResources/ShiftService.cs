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
    internal class ShiftService : IShiftService
    {
        private IHRRepositoryManager _iHRRepositoryManager;
        private readonly IMapper _mapper;

        public ShiftService(IHRRepositoryManager iHRRepositoryManager, IMapper mapper)
        {
            _iHRRepositoryManager = iHRRepositoryManager;
            _mapper = mapper;
        }

        public void Delete(ShiftDto shiftDto)
        {
            var shift = _mapper.Map<Shift>(shiftDto);
            _iHRRepositoryManager.ShiftRepository.Remove(shift);
            _iHRRepositoryManager.Save();
        }

        public void Edit(ShiftDto shiftDto)
        {
            var shift = _mapper.Map<Shift>(shiftDto);
            _iHRRepositoryManager.ShiftRepository.Edit(shift);
            _iHRRepositoryManager.Save();
        }

        public async Task<IEnumerable<ShiftDto>> GetAllShift(bool trackChanges)
        {
            var shift = await _iHRRepositoryManager.ShiftRepository.GetAllShift(trackChanges);
            var shiftDto = _mapper.Map<IEnumerable<ShiftDto>>(shift);
            return shiftDto;
        }

        public async Task<ShiftDto> GetShiftById(byte ShiftId, bool trackChanges)
        {
            var shift = await _iHRRepositoryManager.ShiftRepository.GetShiftById(ShiftId, trackChanges);
            var shiftDto = _mapper.Map<ShiftDto>(shift);
            return shiftDto;
        }

        public void Insert(ShiftForCreateDto shiftForCreateDto)
        {
            var shift = _mapper.Map<Shift>(shiftForCreateDto);
            _iHRRepositoryManager.ShiftRepository.Insert(shift);
            _iHRRepositoryManager.Save();
        }

        public Task<ShiftDto> GetShiftById(short DepartmentId, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
