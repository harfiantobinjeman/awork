using AWork.Domain.Base;
using AWork.Domain.Repositories.Production;
using AWork.Persistence.Repositories.Production;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AWork.Persistence.Base
{
    public class ProductionRepositoryManager : IProductionRepositoryManager
    {
        private AdventureWorks2019Context _dbContext;
        private ILocationRepository _locationRepository;
        public IProductPhotoRepository _productPhotoRepository;
        private IUnitMeasureRepository _unitMeasureRepository;
        private IProductRepository _productRepository;
        private IProductCategoryRepository _productCategoryRepository;
        private IProductSubCategoryRepository _productSubCategoryRepository;
        private IProductInventoryRepository _productInventoryRepository;
        private IScrapReasonRepository _scrapReasonRepository;
        private IWorkOrderRepository _workOrderRepository;
        private IProductProductPhotoRepository _productProductPhotoRepository;
        private IProductReviewRepository _productReviewRepository;
        private IWorkOrderRoutingRepository _workOrderRoutingRepository;
        private ITransactionHistoryRepository _transactionHistoryRepository;
        private IBillOfMaterialRepository _billOfMaterialRepository;
        private IProductCostHistoryRepository _productCostHistoryRepository;

        public ProductionRepositoryManager(AdventureWorks2019Context dbContext)
        {
            _dbContext = dbContext;
        }

        public ILocationRepository LocationRepository
        {
            get
            {
                if (_locationRepository == null)
                {
                    _locationRepository = new LocationRepository(_dbContext);
                }
                return _locationRepository;
            }
        }

        public IProductPhotoRepository ProductPhotoRepository
        {
            get
            {
                if (_locationRepository == null)
                {
                    _productPhotoRepository = new ProductPhotoRepository(_dbContext);
                }
                return _productPhotoRepository;
            }
        }
        public IUnitMeasureRepository UnitMeasureRepository
        {
            get
            {
                if (_unitMeasureRepository == null)
                {
                    _unitMeasureRepository = new UnitMeasureRepository(_dbContext);
                }
                return _unitMeasureRepository;
            }

        }
        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_dbContext);
                }
                return _productRepository;
            }
        }
        public IProductProductPhotoRepository ProductProductPhotoRepository
        {
            get
            {
                if (_productProductPhotoRepository == null)
                {
                    _productProductPhotoRepository = new ProductProductPhotoRepository(_dbContext);
                }
                return _productProductPhotoRepository;
            }
        }
        

        public IProductCategoryRepository ProductCategoryRepository
        {
            get
            {
                if (_productCategoryRepository == null)
                {
                    _productCategoryRepository = new ProductCategoryRepository(_dbContext);
                }
                return _productCategoryRepository;
            }
        }

        public IProductSubCategoryRepository ProductSubCategoryRepository
        {
            get
            {
                if (_productSubCategoryRepository == null)
                {
                    _productSubCategoryRepository = new ProductSubCategoryRepository(_dbContext);
                }
                return _productSubCategoryRepository;
            }
        }


        public IProductInventoryRepository ProductInventoryRepository
        {
            get
            {
                if (_productInventoryRepository == null)
                {
                    _productInventoryRepository = new ProductInventoryRepository(_dbContext);
                }
                return _productInventoryRepository;
            }
        }

        public IScrapReasonRepository ScrapReasonRepository
        {
            get
            {
                if (_scrapReasonRepository == null)
                {
                    _scrapReasonRepository = new ScrapReasonRepository(_dbContext);
                }
                return _scrapReasonRepository;
            }
        }

        public IWorkOrderRepository WorkOrderRepository
        {
            get
            {
                if (_workOrderRepository == null)
                {
                    _workOrderRepository = new WorkOrderRepository(_dbContext);
                }
                return _workOrderRepository;
            }
        }
        public IWorkOrderRoutingRepository WorkOrderRoutingRepository
        {
            get
            {
                if (_workOrderRoutingRepository == null)
                {
                    _workOrderRoutingRepository = new WorkOrderRoutingRepository(_dbContext);
                }
                return _workOrderRoutingRepository;
            }
        }

        public IProductReviewRepository ProductReviewRepository
        {
            get
            {
                if (_productReviewRepository == null)
                {
                    _productReviewRepository = new ProductReviewRepository(_dbContext);
                }
                return _productReviewRepository;
            }
        }

        public ITransactionHistoryRepository TransactionHistoryRepository
        {
            get
            {
                if (_transactionHistoryRepository == null)
                {
                    _transactionHistoryRepository = new TransactionHistoryRepository(_dbContext);
                }
                return _transactionHistoryRepository;
            }
        }

        public IBillOfMaterialRepository billOfMaterialRepository
        {
            get
            {
                if (_billOfMaterialRepository == null)
                {
                    _billOfMaterialRepository = new BillOfMaterialRepository(_dbContext);
                }
                return _billOfMaterialRepository;
            }
        }

        public IProductCostHistoryRepository ProductCostHistoryRepository
        {
            get
            {
                if (_productCostHistoryRepository == null)
                {
                    _productCostHistoryRepository = new ProductCostHistoryRepository(_dbContext);
                }
                return _productCostHistoryRepository;
            }
        }

        public void Save() => _dbContext.SaveChanges();
        public async Task SaveAsync() => await _dbContext.SaveChangesAsync();

}
}
