using AutoMapper;
using AWork.Domain.Base;
using AWork.Services.Abstraction;
using AWork.Services.Abstraction.Sales;
using AWork.Services.Sales;
using System;

namespace AWork.Services
{
    public class SalesServiceManager : ISalesServiceManager
    {

        private readonly Lazy<ISalesTerritoryService> _lazySalesTerritoryService;
        private readonly Lazy<ISalesPersonService> _lazySalesPersonService;
        private readonly Lazy<ISalesOrderHeaderService> _lazySalesOrderHeaderService;
        private readonly Lazy<ISalesOrderDetailService> _lazySalesOrderDetailService;
        private readonly Lazy<IShoppingCartItemService> _lazyShoppingCartItemService;
        private readonly Lazy<ICreditCardService> _lazyCreditCardService;
        private readonly Lazy<IPersonCreditCardService> _lazypersonCreditCardService;
        private readonly Lazy<ICustomerService> _lazyCustomerService;
        private readonly Lazy<IStoreService> _lazyStoreService;
        private readonly Lazy<ICurrencyService> _lazyCurrencyService;
        private readonly Lazy<ISpecialOfferProductService> _lazySpecialOfferProductService;
        private readonly Lazy<ISpecialOfferService> _lazySpecialOfferService;


        public SalesServiceManager(ISalesRepositoryManager repositoryManager, IMapper mapper)
        {
            _lazySalesTerritoryService = new Lazy<ISalesTerritoryService>(() => new SalesTerritoryService(repositoryManager, mapper));
            _lazySalesPersonService = new Lazy<ISalesPersonService>(() => new SalesPersonService(repositoryManager, mapper));
            _lazypersonCreditCardService = new Lazy<IPersonCreditCardService>(() => new PersonCreditCardService(repositoryManager, mapper));
            _lazyCreditCardService = new Lazy<ICreditCardService>(() => new CreditCardService(repositoryManager, mapper));
            _lazyShoppingCartItemService = new Lazy<IShoppingCartItemService>(() => new ShoppingCartItemService(repositoryManager, mapper));
            _lazyCustomerService = new Lazy<ICustomerService>(() => new CustomerService(repositoryManager, mapper));
            _lazyStoreService = new Lazy<IStoreService>(() => new StoreService(repositoryManager, mapper));
            _lazySpecialOfferProductService = new Lazy<ISpecialOfferProductService>(() => new SpecialOfferProductService(repositoryManager, mapper));
            _lazySpecialOfferService = new Lazy<ISpecialOfferService>(() => new SpecialOfferService(repositoryManager, mapper));
            _lazySalesOrderHeaderService = new Lazy<ISalesOrderHeaderService>(() => new SalesOrderHeaderService(repositoryManager, mapper));
            _lazyCurrencyService = new Lazy<ICurrencyService>(() => new CurrencyService(repositoryManager, mapper));
            _lazySalesOrderDetailService = new Lazy<ISalesOrderDetailService>(() => new SalesOrderDetailService(repositoryManager, mapper));
        }

        public ISalesTerritoryService SalesTerritoryService => _lazySalesTerritoryService.Value;

        public ISalesPersonService SalesPersonService => _lazySalesPersonService.Value;

        public ISalesOrderHeaderService SalesOrderHeaderService => _lazySalesOrderHeaderService.Value;

        public ICreditCardService CreditCardService => _lazyCreditCardService.Value;

        public IPersonCreditCardService PersonCreditCardService => _lazypersonCreditCardService.Value;

        public IShoppingCartItemService ShoppingCartItemService => _lazyShoppingCartItemService.Value;

        public ISpecialOfferService SpecialOfferService => _lazySpecialOfferService.Value;

        public ISpecialOfferProductService SpecialOfferProductService => _lazySpecialOfferProductService.Value;

        public ICustomerService CustomerService => _lazyCustomerService.Value;

        public IStoreService StoreService => _lazyStoreService.Value;
        public ICurrencyService CurrencyService => _lazyCurrencyService.Value;

        public ISalesOrderDetailService SalesOrderOrderService => _lazySalesOrderDetailService.Value;
    }
}
