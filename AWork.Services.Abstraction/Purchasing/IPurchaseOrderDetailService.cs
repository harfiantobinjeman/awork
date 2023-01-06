using AWork.Contracts.Dto.Production;
using AWork.Contracts.Dto.Purchasing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Purchasing
{
    public interface IPurchaseOrderDetailService
    {
        Task<IEnumerable<PurchaseOrderDetailsDto>> GetAllPurchaseOD(bool trackChanges);

        Task<PurchaseOrderDetailsDto> GetPurchaseOrderDetails(int orderId, int productId, bool trackChanges);
        Task<PurchaseOrderDetailsDto> GetPurchaseODById(int purchaseOHId, bool trackChanges);
        Task<IEnumerable<PurchaseOrderDetailsDto>> GetPurchaseODByIdd(int purchaseId,bool trackChanges);
        Task<IEnumerable<PurchaseOrderDetailsDto>> GetPurchaseODPaged(int pageIndex, int pagedSize, bool trackChanges);
        void EditPurchase(PurchaseOrderDetailsDto purchaseOrderDetailsDto, ShipMethodDto shipMethodDto, PurchaseOrderHeaderDto purchaseOrderHeaderDto);
        void EditPurchases(IEnumerable<PurchaseOrderDetailsDto> purchaseOrderDetailsDtos);

        void CreateOrderDetail(PurchaseOrderDetailsDto purchaseOrderDetailsDto);
        void Insert(PurchaseOrderDetailsForCreateDto purchaseOrderDetailsForCreateDto);

        void Edit(PurchaseOrderDetailsDto purchaseOrderDetailsDto);

        void Remove(PurchaseOrderDetailsDto purchaseOrderDetailsDto);
    }
}
