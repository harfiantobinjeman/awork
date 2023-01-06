using AWork.Services.Abstraction.Sales;

namespace AWork.Services.Abstraction
{
    public interface ISalesServiceManager
    {
        ISalesTerritoryService SalesTerritoryService { get; }
        ISalesPersonService SalesPersonService { get; }
        IShoppingCartItemService ShoppingCartItemService { get; }
        ISpecialOfferService SpecialOfferService { get; }
        ISpecialOfferProductService SpecialOfferProductService { get; }
        ICustomerService CustomerService { get; }
        IStoreService StoreService { get; }
        ICurrencyService CurrencyService { get; }
        ISalesOrderHeaderService SalesOrderHeaderService { get; }
        ISalesOrderDetailService SalesOrderOrderService { get; }
        ICreditCardService CreditCardService { get; }
        IPersonCreditCardService PersonCreditCardService { get; }

    }
}
