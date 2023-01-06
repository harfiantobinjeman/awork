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
    public class WorkOrderRepository : RepositoryBase<WorkOrder>, IWorkOrderRepository
    {
        public WorkOrderRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(WorkOrder workOrder)
        {
            //throw new NotImplementedException();
            Update(workOrder);
        }

        public async Task<IEnumerable<WorkOrder>> GerPencarian(int workId, int productId, bool trackChange)
        {
            //throw new NotImplementedException();
            var cari = await _dbContext.WorkOrders
                .FromSqlRaw("select * from production.workOrder " +
                "where ProductID = "+productId+" and workorderid like '%"+workId+"%'")
                .ToListAsync();
            return cari;
        }

        public async Task<IEnumerable<WorkOrder>> GetAllOrder(bool trackChange)
        {
            //throw new NotImplementedException();
            var findAll = await FindAll(trackChange)
                .OrderBy(c => c.WorkOrderId)
                .Include(x => x.Product)
                .Include(y => y.WorkOrderRoutings)
                .Include(z => z.ScrapReason)
                .ToListAsync();

            return findAll;
        }

        //include
        public async Task<IEnumerable<WorkOrder>> GetIncludeID(int workOrderIncludeId, bool trackChanges)
        {
            //throw new NotImplementedException();
            return await FindAll(trackChanges).Where(x => x.WorkOrderRoutings.FirstOrDefault().WorkOrderId == workOrderIncludeId)
                .OrderBy(q =>q.WorkOrderId)
                .Include(a => a.WorkOrderRoutings)
                .Include(y => y.Product)
                .ToListAsync();

            /*var include = await _dbContext.WorkOrders
                .FromSqlRaw("select * from Production.Product p " +
                "join Production.WorkOrder w on p.ProductID = w.ProductID " +
                "join Production.WorkOrderRouting wr on w.WorkOrderID = wr.WorkOrderID " +
                "where p.ProductID ="+ workOrderIncludeId)
                .ToListAsync();
            return include;*/
        }

        public async Task<WorkOrder> GetOrderById(int workId, bool trackChange)
        {
            //throw new NotImplementedException();
            return await FindByCondition(c => c.WorkOrderId.Equals(workId), trackChange).SingleOrDefaultAsync();
        }
        //paged
        public async Task<IEnumerable<WorkOrder>> GetWorkOPaged(int pageIndex, int pageSize, bool trackChanges, int id)
        {
            //throw new NotImplementedException();
            //return await FindByCondition(c => c.WorkOrderId.Equals(workId), trackChange).SingleOrDefaultAsync();
            var aku =  await FindByCondition(c => c.ProductId.Equals(id), trackChanges)
                .OrderBy(p => p.WorkOrderId)
                .Include(x => x.Product)
                .Include(y => y.ScrapReason)
                .Include(a => a.WorkOrderRoutings)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return aku;
        }

        public async Task<IEnumerable<WorkOrder>> GetWorkOrder(int workId, bool trackChange)
        {
            //throw new NotImplementedException();
            return await FindAll(trackChange).Where(x => x.ProductId == workId)
                .OrderByDescending(q => q.WorkOrderId)
                .ToListAsync();
        }

        public void Insert(WorkOrder workOrder)
        {
            //throw new NotImplementedException();
            Create(workOrder);
        }

        public void Remove(WorkOrder workOrder)
        {
            //throw new NotImplementedException();
            Delete(workOrder);
        }
    }
}