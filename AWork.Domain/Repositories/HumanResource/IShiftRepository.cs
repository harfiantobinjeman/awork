using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.HumanResource
{
    public interface IShiftRepository
    {
        Task<IEnumerable<Shift>> GetAllShift(bool trackChanges);

        Task<Shift> GetShiftById(byte shiftId, bool trackChanges);
        void Insert(Shift shift);

        void Edit(Shift shift);

        void Remove(Shift shift);
    }
}
