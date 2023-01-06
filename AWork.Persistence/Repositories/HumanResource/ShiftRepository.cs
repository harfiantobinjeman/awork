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
    public class ShiftRepository : RepositoryBase<Shift>, IShiftRepository
    {
   
        public ShiftRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }
        public void Edit(Shift shift)
        {
            Update(shift);
        }

        public async Task<IEnumerable<Shift>> GetAllShift(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(s => s.Name).ToListAsync();
        }
        public async Task<Shift> GetShiftById(byte shiftId, bool trackChanges)
        {
            return await FindByCondition(s => s.ShiftId.Equals(shiftId), trackChanges).SingleOrDefaultAsync();
        }
        
        public void Insert(Shift shift)
        {
            Create(shift);
        }


        public void Remove(Shift shift)
        {
            Delete(shift);
        }

        
    }
}
