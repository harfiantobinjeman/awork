using AWork.Domain.Models;
using AWork.Domain.Repositories.Purchasing;
using AWork.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.Purchasing
{
    public class ProductVendorRepository : RepositoryBase<ProductVendor>, IProductVendorRepository
    {
        public ProductVendorRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(ProductVendor productVendor)
        {
            Update(productVendor);
        }

        public async Task<IEnumerable<ProductVendor>> FilterEmplid(int empId, bool trackChanges)
        {
            return await FindAll(trackChanges)
                .Where(e => e.BusinessEntityId == empId)
                .Include(p => p.BusinessEntity)
                .ThenInclude(p=>p.PurchaseOrderHeaders)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductVendor>> GetAllProductVendor(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(pv => pv.BusinessEntityId)
                .Include(p => p.Product)
                .Include(pp => pp.Product.ProductProductPhotos)
                .ThenInclude(ProductProductPhotos => ProductProductPhotos.ProductPhoto)
                .Include(v => v.BusinessEntity)
                .Include(u=>u.UnitMeasureCodeNavigation)
                .Include(v => v.BusinessEntity.PurchaseOrderHeaders).ThenInclude(pod=>pod.PurchaseOrderDetails)
                .Where(v => v.BusinessEntity.ActiveFlag == true)
                .ToListAsync();
        }

        public async Task<ProductVendor> GetProductVendorById(int id, bool trackChanges)
        {
            return await FindByCondition(pv => pv.ProductId.Equals(id), trackChanges)
                .Include(p => p.Product)
                .Include(pp => pp.Product.ProductProductPhotos)
                .ThenInclude(ProductProductPhotos => ProductProductPhotos.ProductPhoto)
                .Include(v => v.BusinessEntity)
                .Include(u => u.UnitMeasureCodeNavigation)
                .Include(v => v.BusinessEntity.PurchaseOrderHeaders).ThenInclude(pod => pod.PurchaseOrderDetails)
                .Where(v => v.BusinessEntity.ActiveFlag == true)
                .SingleOrDefaultAsync();
        }



        public void Insert(ProductVendor productVendor)
        {
            Create(productVendor);
        }

        public void Remove(ProductVendor productVendor)
        {
            Delete(productVendor);
        }
    }
}
