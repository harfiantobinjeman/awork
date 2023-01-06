using AWork.Domain.Dto.Purchasing;
using AWork.Domain.Models;
using AWork.Domain.Repositories.Purchasing;
using AWork.Persistence.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.Purchasing
{
    public class PurchaseOrderHeaderRepository : RepositoryBase<PurchaseOrderHeader>, IPurchaseOrderHeaderRepository
    {
        public PurchaseOrderHeaderRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(PurchaseOrderHeader purchaseOrderHeader)
        {
            Update(purchaseOrderHeader);
        }

        public async Task<IEnumerable<PurchaseOrderHeader>> FilterEmpId(int empId, bool trackChanges)
        {
            return await FindByCondition(x=>x.EmployeeId.Equals(empId), trackChanges)
                .Where(z=>z.EmployeeId == empId)
                .Include(e=>e.Employee)
                .Include(v=>v.Vendor)
                .ThenInclude(v=>v.BusinessEntity)
                .Include(s=>s.ShipMethod)
                .Include(od=>od.PurchaseOrderDetails)
                .ToListAsync();
        }

        public async Task<IEnumerable<PurchaseOrderHeader>> GetAllPurchaseOH(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(v => v.PurchaseOrderId)
                .Include(v=>v.Vendor).Include(e=>e.Employee.BusinessEntity)
                .Include(e=>e.Employee).ThenInclude(b=>b.BusinessEntity)
                .Include(s=>s.ShipMethod).Include(p=>p.Vendor.BusinessEntity)
                .Include(pp=>pp.PurchaseOrderDetails).ThenInclude(PurchaseOrderDetails=> PurchaseOrderDetails.Product.ProductProductPhotos)
                .ThenInclude(ProductProductPhotos => ProductProductPhotos.ProductPhoto)
            .ToListAsync();
        }

        public IEnumerable<PurchaseOrderHeader> GetAllPurchases(bool trackChanges)
        {
            return FindAll(trackChanges).OrderBy(c => c.PurchaseOrderId).ToList();
        }

        public async Task<IEnumerable<PurchaseOrderHeader>> GetPurchaseOHPaged(int pageIndex, int pagedSize, bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(p=>p.PurchaseOrderId)
                .Include(v => v.Vendor).Include(e => e.Employee).Include(s => s.ShipMethod).Include(p => p.Vendor.BusinessEntity)
                .Skip((pageIndex-1) * pagedSize).Take(pagedSize).ToListAsync();
        }

        public async Task<PurchaseOrderHeader> GetPurchaseOrderHeaderById(int id, bool trackChanges)
        {
            return await FindByCondition(poh => poh.PurchaseOrderId.Equals(id), trackChanges)
                .Include(p=>p.PurchaseOrderDetails)
                .Include(p=>p.PurchaseOrderDetails).ThenInclude(p=>p.Product)
                .Include(pv=>pv.Vendor.ProductVendors)
                .Include(v=>v.Vendor)
                .Include(s=>s.ShipMethod)
                .Include(e=>e.Employee)
                .Include(e=>e.Employee).ThenInclude(b=>b.BusinessEntity)
                .Include(pp => pp.PurchaseOrderDetails).ThenInclude(PurchaseOrderDetails => PurchaseOrderDetails.Product.ProductProductPhotos)
                .ThenInclude(ProductProductPhotos => ProductProductPhotos.ProductPhoto)
                .SingleOrDefaultAsync();
        }
        
        public async Task<IEnumerable<PurchaseOrderHeader>> GetVendorId(int id, bool trackChanges)
        {
            return await FindAll(trackChanges).Where(e => e.VendorId == id)
                .Include(p => p.Vendor)
                .Include(p => p.Employee)
                .Include(p => p.PurchaseOrderDetails)
                .ToListAsync();
        }
        public IEnumerable<PurchaseOrderHeader> GetPurchaseOHAllItem()
        {
            var raw = _dbContext.PurchaseOrderHeaders.FromSqlRaw("select COUNT(*) from Purchasing.PurchaseOrderHeader")
                .ToList();
            return raw;
        }
        public async Task<IEnumerable<GetShipMethod>> GetData(int shipMethodId)
        {
            var shipId = new SqlParameter("@paramId", shipMethodId);
            var rawSQL = await _dbContext.GetShipMethods.FromSqlRaw("select s.ShipMethodId ,s.ShipBase, s.ShipRate from Purchasing.PurchaseOrderHeader p " +
                "join Purchasing.ShipMethod s on p.ShipMethodID = s.ShipMethodID " +
                "where s.ShipMethodId = @paramId " +
                "group by s.ShipMethodId ,s.ShipBase, s.ShipRate", shipId)
                .Select(p => new GetShipMethod
                {
                    ShipMethodId = p.ShipMethodId,
                    ShipBase = p.ShipBase,
                    ShipRate = p.ShipRate
                })
                .OrderBy(s => s.ShipMethodId).ToListAsync();
            return rawSQL;
        }
        public IEnumerable<CartPurchaseItem> CartPurchasesItem(int purchaseOrderId)
        {
            var purchaseId = new SqlParameter("@paramId", purchaseOrderId);
            var raw = _dbContext.CartPurchaseItems.FromSqlRaw("select * from Purchasing.PurchaseOrderHeader p join Purchasing.PurchaseOrderDetail pp on p.PurchaseOrderID = pp.PurchaseOrderID", purchaseId)
                .Select(c => new CartPurchaseItem
                {
                    Name = c.Name,
                    OrderQty = c.OrderQty,
                    UnitPrice = c.UnitPrice,
                    Freight = c.Freight,
                    TaxAmt = c.TaxAmt,
                    LineTotal = c.LineTotal,
                    ShipBase = c.ShipBase,
                    ShipRate = c.ShipRate,
                    ShipMethodId = c.ShipMethodId,
                    SubTotal = c.SubTotal,
                    TotalDue = c.TotalDue,
                })
                .ToList();
            return raw;
        }
        public void Insert(PurchaseOrderHeader purchaseOrderHeader)
        {
            Create(purchaseOrderHeader);
        }

        public void Remove(PurchaseOrderHeader purchaseOrderHeader)
        {
            Delete(purchaseOrderHeader);
        }

        public async Task<IEnumerable<PurchaseOrderHeader>> GetCartItemByCustId(int custId, bool trackChanges)
        {
            return await FindAll(trackChanges)
                .Where(x => x.EmployeeId == custId)
                .Include(p=>p.PurchaseOrderDetails).ThenInclude(p=>p.Product)
                .Include(p => p.PurchaseOrderDetails)
                .ThenInclude(pp => pp.Product.ProductProductPhotos)
                .ThenInclude(p => p.ProductPhoto)
                .Include(v=>v.Vendor)
                .Include(p=>p.ShipMethod)
                .Include(p=>p.Employee)
                .ToListAsync();
        }
    }
}