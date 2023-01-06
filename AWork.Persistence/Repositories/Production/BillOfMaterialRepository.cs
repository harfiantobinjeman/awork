using AWork.Domain.Models;
using AWork.Domain.Repositories.Production;
using AWork.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.Production
{
    public class BillOfMaterialRepository : RepositoryBase<BillOfMaterial>, IBillOfMaterialRepository
    {
        public BillOfMaterialRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<BillOfMaterial>> GetAllBillOfMaterial(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(o => o.BillOfMaterialsId)
                .Include(p => p.ProductAssembly)
                .Include(o => o.Component)
                .Include(u => u.UnitMeasureCodeNavigation)
                .ToListAsync();
        }

        public async Task<BillOfMaterial> GetBillOfMaterialById(int BillOfMaterialsId, bool trackChanges)
        {
            return await FindByCondition(o => o.BillOfMaterialsId.Equals(BillOfMaterialsId), trackChanges)
                .Include(p => p.ProductAssembly)
                .Include(o => o.Component)
                .Include(u => u.UnitMeasureCodeNavigation)
                .SingleOrDefaultAsync();
        }

        public void Insert(BillOfMaterial billOfMaterial)
        {
            Create(billOfMaterial);
        }

        public void Remove(BillOfMaterial billOfMaterial)
        {
            Delete(billOfMaterial);
        }
        public void Edit(BillOfMaterial billOfMaterial)
        {
            Update(billOfMaterial);
        }
    }
}
