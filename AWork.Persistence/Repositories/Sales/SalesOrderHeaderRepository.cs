using AWork.Contracts.Dto.Sales.SalesOrderHeader;
using AWork.Domain.Models;
using AWork.Domain.Repositories.Sales;
using AWork.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.Sales
{
    internal class SalesOrderHeaderRepository : RepositoryBase<SalesOrderHeader>, ISalesOrderHeaderRepository
    {
        public SalesOrderHeaderRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(SalesOrderHeader salesorderHeader)
        {
            Update(salesorderHeader);
        }

        public async Task<IEnumerable<SalesOrderHeader>> GetAllSalesOrderHeader(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(c => c.SalesOrderId)
                .Include(b => b.CreditCard)
                .Include(s => s.CurrencyRate)
                .Include(s => s.Customer)
                .Include(s => s.SalesPerson)
                .Include(s => s.ShipMethod)
                .Include(s => s.ShipToAddress)
                .Include(s => s.Territory)
                .Include(s=>s.BillToAddress).

                /* .Include(a=>a.CreditCard)
                 .Include(d=>d.ShipToAddressId)
                 .Include(e=>e.Status)
                 .Include(f=>f.BillToAddressId)
                 .Include(f=>f.Comment)
                 .*/
                ToListAsync();
        }

        public async Task<SalesOrderHeader> GetByIdSalesOrderHeader(int salesorderHeaderId, bool trackChanges)
        {
            return await FindByCondition(c => c.SalesOrderId.Equals(salesorderHeaderId), trackChanges).SingleOrDefaultAsync();
        }

        public void Insert(SalesOrderHeader salesorderHeader)
        {
            Create(salesorderHeader);
        }

        public void Remove(SalesOrderHeader salesorderHeader)
        {
            Delete(salesorderHeader);
        }
    }
}
