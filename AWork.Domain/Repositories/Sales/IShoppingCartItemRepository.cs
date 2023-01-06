using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Sales
{
    public interface IShoppingCartItemRepository
    {
        Task<IEnumerable<ShoppingCartItem>> GetAllShoppingCartItem(bool trackChanges);

        Task<IEnumerable<Product>> GetProductOnSales(bool trackChanges);

        Task<IEnumerable<ShoppingCartItem>> FilterCustId(string custId, bool trackChanges);

        Task<IEnumerable<ShoppingCartItem>> GetCartItemByCustId(string custId, bool trackChanges);

        Task<ShoppingCartItem> GetShoppingCartItemById(int shopItemId, bool trackChanges);

        Task<ShoppingCartItem> GetDataCartCustomerById(int shoppingCartId, int productId, bool trackChanges);

        Task<Product> GetProductById(int productId, bool trackChanges);

        void Insert(ShoppingCartItem shoppingCartItem);

        void Edit(ShoppingCartItem shoppingCartItem);

        void Remove(ShoppingCartItem shoppingCartItem);
    }
}
