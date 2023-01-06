using AWork.Contracts.Dto.Sales.ShoppingCartItem;
using AWork.Domain.Models;
using AWork.Domain.Repositories.Sales;
using AWork.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace AWork.Persistence.Repositories.Sales
{
    internal class ShoppingCartItemRepository : RepositoryBase<ShoppingCartItem>, IShoppingCartItemRepository
    {
        public ShoppingCartItemRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(ShoppingCartItem shoppingCartItem)
        {
            Update(shoppingCartItem);
        }

        public async Task<IEnumerable<ShoppingCartItem>> FilterCustId(string custId, bool trackChanges)
        {
            return await FindAll(trackChanges)
                .Where(a => a.ShoppingCartId == custId)
                .Include(p => p.Product)
                .ToListAsync();
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetAllShoppingCartItem(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.ShoppingCartItemId).ToListAsync();
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetCartItemByCustId(string custId, bool trackChanges)
        {
            return await FindAll(trackChanges)
                .Where(x => x.ShoppingCartId == custId)
                .Include(p => p.Product)
                .Include(pp => pp.Product.ProductProductPhotos)
                .ThenInclude(p => p.ProductPhoto)
                .Include(p=>p.Product.PurchaseOrderDetails)
                .Include(p => p.Product.PurchaseOrderDetails).ThenInclude(pp=>pp.PurchaseOrder)
                .Include(p => p.Product.PurchaseOrderDetails).ThenInclude(pp => pp.PurchaseOrder.Vendor)
                .ToListAsync();
        }

        public async Task<ShoppingCartItem> GetDataCartCustomerById(int shoppingCartId, int productId, bool trackChanges)
        {
            return await FindByCondition(x => x.ShoppingCartItemId.Equals(shoppingCartId) && x.ProductId.Equals(productId), trackChanges)
                .Include(p => p.Product)
                .SingleOrDefaultAsync();
        }

        public async Task<Product> GetProductById(int productId, bool trackChanges)
        {
            return await _dbContext.Products
                .Where(x => x.ProductId.Equals(productId))
                .Include(p => p.ProductProductPhotos)
                .ThenInclude(x => x.ProductPhoto)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductOnSales(bool trackChanges)
        {
            var product = await _dbContext.Products
                .Include(pp => pp.ProductProductPhotos)
                .ThenInclude(p => p.ProductPhoto)
                .Where(x => x.ProductProductPhotos.Any(y => y.ProductId == x.ProductId) && x.ProductProductPhotos.Any(y => y.ModifiedDate > new DateTime(2022, 01,01)))
                .ToListAsync();
            return product;
        }

        public async Task<ShoppingCartItem> GetShoppingCartItemById(int shopItemId, bool trackChanges)
        {
            return await FindByCondition(c => c.ShoppingCartItemId.Equals(shopItemId), trackChanges).SingleOrDefaultAsync();
        }

        public void Insert(ShoppingCartItem shoppingCartItem)
        {
            Create(shoppingCartItem);
        }

        public void Remove(ShoppingCartItem shoppingCartItem)
        {
            Delete(shoppingCartItem);
        }
    }
}
