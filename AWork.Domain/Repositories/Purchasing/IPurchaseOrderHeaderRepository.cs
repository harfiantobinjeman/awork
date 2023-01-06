using AWork.Domain.Dto.Purchasing;
using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Purchasing
{
    public interface IPurchaseOrderHeaderRepository
    {
        Task<IEnumerable<PurchaseOrderHeader>> GetAllPurchaseOH(bool trackChanges);
        IEnumerable<PurchaseOrderHeader> GetAllPurchases(bool trackChanges);
        Task<IEnumerable<PurchaseOrderHeader>> FilterEmpId(int empId, bool trackChanges);
        Task<IEnumerable<PurchaseOrderHeader>> GetPurchaseOHPaged(int pageIndex, int pagedSize, bool trackChanges);
        Task<PurchaseOrderHeader> GetPurchaseOrderHeaderById(int id, bool trackChanges);
        Task<IEnumerable<GetShipMethod>> GetData(int shipMethodId);
        IEnumerable<PurchaseOrderHeader> GetPurchaseOHAllItem();
        IEnumerable<CartPurchaseItem> CartPurchasesItem(int purchaseOrderId);
        Task<IEnumerable<PurchaseOrderHeader>> GetCartItemByCustId(int custId, bool trackChanges);

        void Insert(PurchaseOrderHeader purchaseOrderHeader);

        void Edit(PurchaseOrderHeader purchaseOrderHeader);

        void Remove(PurchaseOrderHeader purchaseOrderHeader);
    }
}
