using AWork.Services.Abstraction.Purchasing;

namespace AWork.Services.Abstraction
{
    public interface IPurchasingServiceManager
    {
        IVendorService VendorService { get; }
        IPurchaseOrderHeaderService PurchaseOrderHeaderService { get; }

        IShipMethodService ShipMethodService { get; }
        IProductVendorService ProductVendorService { get; }
        IPurchaseOrderDetailService PurchaseOrderDetailService { get; }
    }
}
