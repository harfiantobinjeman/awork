using AWork.Domain.Models;
using AWork.Domain.Repositories.Purchasing;
using AWork.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.Purchasing
{
    internal class ShipMethodRepository : RepositoryBase<ShipMethod>, IShipMethodRepository
    {
        public ShipMethodRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(ShipMethod shipMethod)
        {
            Update(shipMethod);
        }

        public async Task<IEnumerable<ShipMethod>> GetAllShipMethod(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(sm => sm.Name).ToListAsync();
        }

        public async Task<ShipMethod> GetShipMethodById(int shipMethodID, bool trackChanges)
        {
            return await FindByCondition(sm => sm.ShipMethodId.Equals(shipMethodID), trackChanges).SingleOrDefaultAsync();
        }

        public void Insert(ShipMethod shipMethod)
        {
            Create(shipMethod);
        }

        public void Remove(ShipMethod shipMethod)
        {
            Delete(shipMethod);
        }
    }
}
