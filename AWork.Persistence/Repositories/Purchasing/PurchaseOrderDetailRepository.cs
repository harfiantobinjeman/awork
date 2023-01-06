using AWork.Domain.Dto.Purchasing;
using AWork.Domain.Models;
using AWork.Domain.Repositories.Purchasing;
using AWork.Persistence.Base;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.Purchasing
{
    public class PurchaseOrderDetailRepository : RepositoryBase<PurchaseOrderDetail>, IPurchaseOrderDetailRepository
    {
        public PurchaseOrderDetailRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(PurchaseOrderDetail purchaseOrderDetail)
        {
            Update(purchaseOrderDetail);
        }

        public async Task<IEnumerable<PurchaseOrderDetail>> GetAllPurchaseOrderDetail(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(od=>od.PurchaseOrderDetailId).Include(p=>p.Product)
                .Include(p => p.PurchaseOrder)
                .Include(p => p.PurchaseOrder).ThenInclude(v => v.Vendor).Include(p=>p.PurchaseOrder).ThenInclude(v=>v.Employee).Include(p => p.PurchaseOrder).ThenInclude(s=>s.ShipMethod).ToListAsync();
        }

        public async Task<IEnumerable<PurchaseOrderDetail>> GetPurchaseODPaged(int pageIndex, int pagedSize, bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(p => p.PurchaseOrderDetailId)
                .Include(p => p.PurchaseOrder).ThenInclude(v => v.OrderDate)
                .Include(p => p.PurchaseOrder).ThenInclude(v => v.Vendor.Name).Include(p => p.PurchaseOrder).ThenInclude(v => v.Vendor.AccountNumber)
                .Include(c => c.Product).Include(p=>p.PurchaseOrder.Vendor).Include(p=>p.PurchaseOrder.ShipMethod).Skip((pageIndex - 1) * pagedSize).Take(pagedSize).ToListAsync();
        }

        public async Task<PurchaseOrderDetail> GetPurchaseOrderDetail(int orderId, int productId, bool trackChanges)
        {
            return await FindByCondition(c => c.PurchaseOrderDetailId.Equals(orderId), trackChanges).SingleOrDefaultAsync();

        }

        public async Task<PurchaseOrderDetail> GetPurchaseOrderDetailById(int id, bool trackChanges)
        {
            return await FindByCondition(pod => pod.PurchaseOrderId.Equals(id), trackChanges).Where(p=>p.PurchaseOrderId == id)
                .Include(p => p.PurchaseOrder).ThenInclude(v=>v.Vendor)
                .Include(p=>p.PurchaseOrder).ThenInclude(e=>e.Employee)
                .Include(c => c.Product).Include(p=>p.PurchaseOrder)
                .Include(s=>s.PurchaseOrder.ShipMethod)
                .Include(p => p.PurchaseOrder).ThenInclude(s=>s.ShipMethod).Include(p=>p.PurchaseOrder).ThenInclude(pr=>pr.Vendor).ThenInclude(pr=>pr.ProductVendors)
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<PurchaseOrderDetail>> GetPurchaseOrderDetailByIdd(int id, bool trackChanges)
        {
            return await FindByCondition(pod => pod.PurchaseOrderId.Equals(id),trackChanges).Where(p => p.PurchaseOrderId == id)
                .Include(p => p.PurchaseOrder).ThenInclude(v => v.Vendor)
                .Include(c => c.Product).Include(p => p.PurchaseOrder)
                .Include(p => p.PurchaseOrder).ThenInclude(s => s.ShipMethod).Include(p => p.PurchaseOrder).ThenInclude(pr => pr.Vendor).ThenInclude(pr => pr.ProductVendors)
                .AsNoTracking()
                .ToListAsync();
        }
/*
        public async Task<IEnumerable<GetShipMethod>> GetShipMethode(int ShipMethodID)
        {
            var pName = new SqlParameter("@ShipMethodID", ShipMethodID);
            var rawSQL = await _dbContext.GetShipMethods
                .FromSqlRaw("select s.ShipBase, s.ShipRate, s.ShipMethodID "
                + "from Purchasing.PurchaseOrderDetail p join Purchasing.PurchaseOrderHeader pp on p.PurchaseOrderID = pp.PurchaseOrderID "
                + "join Purchasing.ShipMethod s on pp.ShipMethodID = s.ShipMethodID "
                + "group by s.ShipBase, s.ShipRate, s.ShipMethodID", pName).Select(x => new GetShipMethod
                {
                    ShipBase = x.ShipBase,
                    ShipRate = x.ShipRate,
                    ShipMethodId =x.ShipMethodId
                }).
                OrderBy(x => x.ShipMethodId)
                .ToListAsync();
            return rawSQL;
        }*/

        public void Insert(PurchaseOrderDetail purchaseOrderDetail)
        {
            Create(purchaseOrderDetail);
        }

        public void Remove(PurchaseOrderDetail purchaseOrderDetail)
        {
            Remove(purchaseOrderDetail);
        }
    }
}
