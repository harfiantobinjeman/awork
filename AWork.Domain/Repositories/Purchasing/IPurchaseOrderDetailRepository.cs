using AWork.Domain.Dto.Purchasing;
using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Purchasing
{
    public interface IPurchaseOrderDetailRepository
    {
        Task<IEnumerable<PurchaseOrderDetail>> GetAllPurchaseOrderDetail(bool trackChanges);
        Task<PurchaseOrderDetail> GetPurchaseOrderDetail(int orderId, int productId, bool trackChanges);
        Task<PurchaseOrderDetail> GetPurchaseOrderDetailById(int id,bool trackChanges);
        Task<IEnumerable<PurchaseOrderDetail>> GetPurchaseOrderDetailByIdd(int id,  bool trackChanges);
        Task<IEnumerable<PurchaseOrderDetail>> GetPurchaseODPaged(int pageIndex, int pagedSize, bool trackChanges);
        /*Task<IEnumerable<GetShipMethod>> GetShipMethode(int ShipMethodID);*/
        void Insert(PurchaseOrderDetail purchaseOrderDetail);

        void Edit(PurchaseOrderDetail purchaseOrderDetail);

        void Remove(PurchaseOrderDetail purchaseOrderDetail);
    }
}
