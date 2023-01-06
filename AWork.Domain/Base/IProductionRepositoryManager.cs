using AWork.Domain.Repositories.Production;
using System.Threading.Tasks;

namespace AWork.Domain.Base
{
    public interface IProductionRepositoryManager
    {
        IProductPhotoRepository ProductPhotoRepository { get; }
        ILocationRepository LocationRepository { get; }

        IProductCategoryRepository ProductCategoryRepository { get; }

        IProductSubCategoryRepository ProductSubCategoryRepository { get; }

        IUnitMeasureRepository UnitMeasureRepository { get; }
        IProductProductPhotoRepository ProductProductPhotoRepository { get; }

        IProductInventoryRepository ProductInventoryRepository { get; }
        IScrapReasonRepository ScrapReasonRepository { get; }
        IWorkOrderRepository WorkOrderRepository { get; }
        IProductRepository ProductRepository { get; }

        IWorkOrderRoutingRepository WorkOrderRoutingRepository { get; }
        ITransactionHistoryRepository TransactionHistoryRepository { get; }

        IBillOfMaterialRepository billOfMaterialRepository { get; }

        IProductReviewRepository ProductReviewRepository { get; }
        IProductCostHistoryRepository ProductCostHistoryRepository { get; }

        void Save();

        Task SaveAsync();
    }
}
