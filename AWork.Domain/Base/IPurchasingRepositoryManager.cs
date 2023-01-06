using AWork.Domain.Repositories.Purchasing;
using System.Threading.Tasks;

namespace AWork.Domain.Base
{
    public interface IPurchasingRepositoryManager
    {
        IVendorRepository VendorRepository { get; }
        IPurchaseOrderHeaderRepository PurchaseOrderHeaderRepository { get; }
        IPurchaseOrderDetailRepository PurchaseOrderDetailRepository { get; }

        IShipMethodRepository ShipMethodRepository { get; }
        IProductVendorRepository ProductVendorRepository { get; }

        void Save();

        Task SaveAsync();
    }
}
