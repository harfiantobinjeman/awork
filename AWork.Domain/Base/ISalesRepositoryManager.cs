using AWork.Domain.Repositories.Sales;
using System.Threading.Tasks;

namespace AWork.Domain.Base
{
    public interface ISalesRepositoryManager
    {
        ISalesTerritoryRepository SalesTerritoryRepository { get; }
        ISalesPersonRepository SalesPersonRepository { get; }
        IShoppingCartItemRepository ShoppingCartItemRepository { get; }
        ICustomerRepository CustomerRepository { get; }

        ICreditCardRepository CreditCardRepository { get; }
        IPersonCreditCardRepository PersonCreditCardRepository { get; }
        ISalesOrderHeaderRepository SalesOrderHeaderRepository { get; }

        ISpecialOfferRepository SpecialOfferRepository { get; }
        ISpecialOfferProductRepository SpecialOfferProductRepository { get; }
        ISalesOrderDetailRepository SalesOrderDetailRepository { get; }

        ICurrencyRepository CurrencyRepository { get; }
        ICurrencyRateRepository CurrencyRateRepository { get; }
        IStoreRepository StoreRepository { get; }


        void Save();

        Task SaveAsync();

    }
}
