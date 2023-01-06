using AWork.Domain.Models;
using AWork.Domain.Repositories.PersonModul;
using AWork.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.PersonModul
{
    public class StateProvinceRepository : RepositoryBase<StateProvince>, IStateProvinceRepository
    {
        public StateProvinceRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(StateProvince stateProvince)
        {
            Update(stateProvince);
        }

        public async Task<IEnumerable<StateProvince>> GetAllStateProvince(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.StateProvinceId)
                .Include(a => a.CountryRegionCodeNavigation)
                .Include(s => s.Territory)
                .ToListAsync();
        }

        public async Task<StateProvince> GetAllStateProvinceById(int StateId, bool trackChanges)
        {
            return await FindByCondition(c => c.StateProvinceId.Equals(StateId), trackChanges)
                .Include(a => a.CountryRegionCodeNavigation)
                .Include(s => s.Territory)
                .SingleOrDefaultAsync();
        }

        public void Insert(StateProvince stateProvince)
        {
            Create(stateProvince);
        }

        public void Remove(StateProvince stateProvince)
        {
            Delete(stateProvince);
        }
    }
}
