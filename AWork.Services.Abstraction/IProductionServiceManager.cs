using AWork.Services.Abstraction.Production;

namespace AWork.Services.Abstraction
{
    public interface IProductionServiceManager
    {
        ILocationService LocationService { get; }

        IUnitMeasureService UnitMeasureservice { get; }

        IProductPhotoService ProductPhotoService { get; }

        IProductService ProductService { get; }
        IProductCategoryService ProductCategoryService { get; }

        IProductSubCategoryService ProductSubCategoryService { get; }
        IProductInventoryService ProductInventoryService { get; }
        IScrapReasonService ScrapReasonService { get; }
        IWorkOrderService WorkOrderService { get; }
        IProductProductPhotoService ProductProductPhotoService { get; }
        IProductReviewService ProductReviewService { get; }

        IWorkOrderRoutingService WorkOrderRoutingService { get; }
        IBillOfMaterialService BillOfMaterialService { get; }
        ITransactionHistoryService TransactionHistoryService { get; }
        IProductCostHistoryService ProductCostHistoryService { get; }

    }
}
