using AWork.Domain.Models;
using AWork.Domain.Repositories.Production;
using AWork.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.Production
{
    public class UnitMeasureRepository : RepositoryBase<UnitMeasure>, IUnitMeasureRepository
    {
        public UnitMeasureRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(UnitMeasure unitMeasure)
        {
            Update(unitMeasure);
        }


        public async Task<IEnumerable<UnitMeasure>> GetAllUnitMeasure(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(u => u.Name).ToListAsync();
        }

        public async Task<UnitMeasure> GetUnitMeasureById(string UnitMeasureCode, bool trackChanges)
        {
            return await FindByCondition(u => u.UnitMeasureCode.Equals(UnitMeasureCode), trackChanges).SingleOrDefaultAsync();
        }

        public void Insert(UnitMeasure unitMeasure)
        {
            Create(unitMeasure);
        }

        public void Remove(UnitMeasure unitMeasure)
        {
            Delete(unitMeasure);
        }
    }
}
