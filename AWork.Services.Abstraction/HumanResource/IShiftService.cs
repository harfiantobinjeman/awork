using AWork.Contracts.Dto.HumanResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.HumanResource
{
    public interface IShiftService
    {
        Task<IEnumerable<ShiftDto>> GetAllShift(bool trackChanges);

        Task<ShiftDto> GetShiftById(short DepartmentId, bool trackChanges);

        void Insert(ShiftForCreateDto shiftForCreateDto);
        void Edit(ShiftDto shiftDto);
        void Delete(ShiftDto shiftDto);
    }
}
