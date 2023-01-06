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
    public class SalesOrderDetailRepository : RepositoryBase<SalesOrderDetail>, ISalesOrderDetailRepository
    {
        public SalesOrderDetailRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(SalesOrderDetail salesOrderDetail)
        {
            Update(salesOrderDetail);
        }

        public async Task<IEnumerable<SalesOrderDetail>> GetAllSalesOrderDetail(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.SalesOrderDetailId).ToListAsync();
        }

        public async Task<SalesOrderDetail> GetSalesOrderDetailById(int salesOrderDetailId, bool trackChanges)
        {
            return await FindByCondition(c => c.SalesOrderDetailId.Equals(salesOrderDetailId), trackChanges).SingleOrDefaultAsync();
        }

        public void Insert(SalesOrderDetail salesOrderDetail)
        {
            Create(salesOrderDetail);
        }

        public void Remove(SalesOrderDetail salesOrderDetail)
        {
            Delete(salesOrderDetail);
        }
    }
}
