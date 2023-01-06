using AutoMapper;
using AWork.Domain.Base;
using AWork.Domain.Repositories.Production;
using AWork.Persistence.Base;
using AWork.Services.Abstraction;
using AWork.Services.Abstraction.Production;
using AWork.Services.Production;
using System;

namespace AWork.Services
{
    public class ProductionServiceManager : IProductionServiceManager
    {

        private readonly Lazy<IProductCategoryService> _lazyProductionCategoryService;
        private readonly Lazy<IProductSubCategoryService> _lazyProductSubCategoryService;
        private readonly Lazy<IUnitMeasureService> _lazyUnitMeasureService;
        private readonly Lazy<IProductService> _lazyProductService;
        private readonly Lazy<IProductPhotoService> _lazyProductPhotoService;
        private readonly Lazy<ILocationService> _lazyLocationService;
        private readonly Lazy<IProductInventoryService> _lazyProductInventoryService;
        private readonly Lazy<IScrapReasonService> _lazyScrapReasonService;
        private readonly Lazy<IWorkOrderService> _lazyWorkOrderService;
        private readonly Lazy<IProductProductPhotoService> _lazyProductProductPhotoService;
        private readonly Lazy<IWorkOrderRoutingService> _lazyWorkOrderRoutingService;
        private readonly Lazy<IProductReviewService> _lazyProductReviewService;
        private readonly Lazy<ITransactionHistoryService> _lazyTransactionHistoryService;
        private readonly Lazy<IBillOfMaterialService> _lazyBillOfMaterialService;
        private readonly Lazy<IProductCostHistoryService> _lazyProductCostHistoryService;

        public ProductionServiceManager(IProductionRepositoryManager ProductionRepositoryManager, IMapper mapper)
        {
            _lazyProductionCategoryService = new Lazy<IProductCategoryService>(
                () => new ProductionCategoryService(ProductionRepositoryManager, mapper)
               );
            _lazyProductSubCategoryService = new Lazy<IProductSubCategoryService>(
                () => new ProductSubCategoryService(ProductionRepositoryManager, mapper))
                ;
            _lazyUnitMeasureService = new Lazy<IUnitMeasureService>(
                () => new UnitMeasureService(ProductionRepositoryManager, mapper)
               );
            _lazyProductService = new Lazy<IProductService>(
              () => new ProductService(ProductionRepositoryManager, mapper)
             );

            _lazyLocationService = new Lazy<ILocationService>(
                () => new LocationService(ProductionRepositoryManager, mapper));

            _lazyProductInventoryService = new Lazy<IProductInventoryService>(
                () => new ProductInventoryService(ProductionRepositoryManager, mapper));

            _lazyScrapReasonService = new Lazy<IScrapReasonService>(
                () => new ScrapReasonService(ProductionRepositoryManager, mapper));

            _lazyWorkOrderService = new Lazy<IWorkOrderService>(
                () => new WorkOrderService(ProductionRepositoryManager, mapper)
                );
            _lazyProductPhotoService = new Lazy<IProductPhotoService>(
                () => new ProductPhotoService(ProductionRepositoryManager, mapper)
                );
            _lazyProductProductPhotoService = new Lazy<IProductProductPhotoService>(
                () => new ProductProductPhotoService(ProductionRepositoryManager, mapper)
                );

            _lazyWorkOrderRoutingService = new Lazy<IWorkOrderRoutingService>(
                () => new WorkOrderRoutingService(ProductionRepositoryManager, mapper));

            _lazyProductReviewService = new Lazy<IProductReviewService>(
                () => new ProductReviewService(ProductionRepositoryManager, mapper));
            _lazyTransactionHistoryService = new Lazy<ITransactionHistoryService>(
                () => new TransactionHistoryService(ProductionRepositoryManager, mapper));
            _lazyBillOfMaterialService = new Lazy<IBillOfMaterialService>(
                () => new BillOfMaterialService(ProductionRepositoryManager, mapper));
            _lazyProductCostHistoryService = new Lazy<IProductCostHistoryService>(
                () => new ProductCostHistoryService(ProductionRepositoryManager, mapper));
                
        }

        public IProductPhotoService ProductPhotoService => _lazyProductPhotoService.Value;
        public IUnitMeasureService UnitMeasureservice => _lazyUnitMeasureService.Value;
        public ILocationService LocationService => _lazyLocationService.Value;
        public IProductService ProductService => _lazyProductService.Value;
        public IProductCategoryService ProductCategoryService => _lazyProductionCategoryService.Value;
        public IProductInventoryService ProductInventoryService => _lazyProductInventoryService.Value;
        public IScrapReasonService ScrapReasonService => _lazyScrapReasonService.Value;
        public IWorkOrderService WorkOrderService => _lazyWorkOrderService.Value;
        public IProductProductPhotoService ProductProductPhotoService => _lazyProductProductPhotoService.Value;

        public IWorkOrderRoutingService WorkOrderRoutingService => _lazyWorkOrderRoutingService.Value;

        public IProductReviewService ProductReviewService => _lazyProductReviewService.Value;

        public IProductSubCategoryService ProductSubCategoryService => _lazyProductSubCategoryService.Value;

        public IBillOfMaterialService BillOfMaterialService => _lazyBillOfMaterialService.Value;

        public ITransactionHistoryService TransactionHistoryService => _lazyTransactionHistoryService.Value;

        public IProductCostHistoryService ProductCostHistoryService => _lazyProductCostHistoryService.Value;
    }
}
