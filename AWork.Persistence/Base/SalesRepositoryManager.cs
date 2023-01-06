using AWork.Domain.Base;
using AWork.Domain.Repositories.Sales;
using AWork.Persistence.Repositories.Sales;
using System.Threading.Tasks;

namespace AWork.Persistence.Base
{
    public class SalesRepositoryManager : ISalesRepositoryManager
    {
        private AdventureWorks2019Context _dbContext;
        private ISalesTerritoryRepository _salesTerritoryRepository;
        private ISalesPersonRepository _salesPersonRepository;
        private IShoppingCartItemRepository _shoppingCartItemRepository;
        private ICustomerRepository _customerRepository;

        private ICreditCardRepository _creditcardRepository;
        private IPersonCreditCardRepository _personcreditcardRepository;
        private ISalesOrderHeaderRepository _salesorderheaderRepository;
        


        private ICurrencyRepository _currencyRepository;
        private ICurrencyRateRepository _currencyRateRepository;
        private IStoreRepository _storeRepository;

        private ISpecialOfferRepository _specialOfferRepository;
        private ISpecialOfferProductRepository _specialOfferProductRepository;

        private ISalesOrderDetailRepository _salesOrderDetailRepository;

        public SalesRepositoryManager(AdventureWorks2019Context dbContext)
        {
            _dbContext = dbContext;
        }

        public ISalesTerritoryRepository SalesTerritoryRepository
        {
            get
            {
                if (_salesTerritoryRepository == null)
                {
                    _salesTerritoryRepository = new SalesTerritoryRepository(_dbContext);
                }
                return _salesTerritoryRepository;
            }
        }

        public ISalesPersonRepository SalesPersonRepository
        {
            get
            {
                if (_salesPersonRepository == null)
                {
                    _salesPersonRepository = new SalesPersonRepository(_dbContext);
                }
                return _salesPersonRepository;
            }
        }

        public IShoppingCartItemRepository ShoppingCartItemRepository
        {
            get
            {
                if (_shoppingCartItemRepository == null)
                {
                    _shoppingCartItemRepository = new ShoppingCartItemRepository(_dbContext);
                }
                return _shoppingCartItemRepository;
            }
        }

        public ICustomerRepository CustomerRepository
        {
            get
            {
                if (_customerRepository == null)
                {
                    _customerRepository = new CustomerRepository(_dbContext);
                }
                return _customerRepository;
            }
        }

        public ICreditCardRepository CreditCardRepository
        {
            get
            {
                if (_creditcardRepository == null)
                {
                    _creditcardRepository = new CreditCardRepository(_dbContext);
                }
                return _creditcardRepository;
            }
        }

        public IPersonCreditCardRepository PersonCreditCard
        {
            get
            {
                if (_personcreditcardRepository == null)
                {
                    _personcreditcardRepository = new PersonCreditCardRepository(_dbContext);
                }
                return _personcreditcardRepository;
            }
        }


        public ISalesOrderHeaderRepository SalesOrderHeader
        {
            get
            {
                if (_salesorderheaderRepository == null)
                {
                    _salesorderheaderRepository = new SalesOrderHeaderRepository(_dbContext);
                }
                return _salesorderheaderRepository;
            }
        }
        public ISpecialOfferRepository SpecialOfferRepository
        {
            get
            {
                if (_specialOfferRepository == null)
                {
                    _specialOfferRepository = new SpecialOfferRepository(_dbContext);
                }
                return _specialOfferRepository;
            }
        }

        public ISpecialOfferProductRepository SpecialOfferProductRepository
        {
            get
            {
                if (_specialOfferProductRepository == null)
                {
                    _specialOfferProductRepository = new SpecialOfferProductRepository(_dbContext);
                }
                return _specialOfferProductRepository;
            }
        }

        public ISalesOrderDetailRepository SalesOrderDetailRepository 
        { 
            get
            {
                if (_salesOrderDetailRepository == null)
                {
                    _salesOrderDetailRepository = new SalesOrderDetailRepository(_dbContext);
                }
                return _salesOrderDetailRepository;
            }
        }

        public ICurrencyRepository CurrencyRepository 
        {
            get
            {
                if (_currencyRepository==null)
                {
                    _currencyRepository= new CurrencyRepository(_dbContext);

                }
                return _currencyRepository;
            }
        }

        public ICurrencyRateRepository CurrencyRateRepository 
        {
            get
            {
                if (_currencyRateRepository==null)
                {
                    _currencyRateRepository= new CurrencyRateRepository(_dbContext);
                }
                return _currencyRateRepository;
            }
        }

        public IStoreRepository StoreRepository 
        {
            get
            {
                if (_storeRepository==null)
                {
                    _storeRepository = new StoreRepository(_dbContext);
                }
                return _storeRepository;
            }
        }

        public IPersonCreditCardRepository PersonCreditCardRepository 
        { 
            get
            {
                if (_personcreditcardRepository == null)
                {
                    _personcreditcardRepository = new PersonCreditCardRepository(_dbContext);
                }
                return _personcreditcardRepository;
            }
        }

        public ISalesOrderHeaderRepository SalesOrderHeaderRepository 
        { 
            get
            {
                if (_salesorderheaderRepository == null)
                {
                    _salesorderheaderRepository= new SalesOrderHeaderRepository(_dbContext);
                }
                return _salesorderheaderRepository;
            }
        }

        public void Save() => _dbContext.SaveChanges();
        public async Task SaveAsync() => await _dbContext.SaveChangesAsync();

    }
}
