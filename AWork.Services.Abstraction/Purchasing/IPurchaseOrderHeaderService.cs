using AWork.Contracts.Dto.Purchasing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Purchasing
{
    public interface IPurchaseOrderHeaderService
    {
        Task<IEnumerable<PurchaseOrderHeaderDto>> GetAllPurchaseOH(bool trackChanges);
        Task<PurchaseOrderHeaderDto> GetPurchaseOHById(int purchaseOHId, bool trackChanges);
        Task<IEnumerable<PurchaseOrderHeaderDto>> FilterEmpId(int empId, bool trackChanges);
        Task<IEnumerable<PurchaseOrderHeaderDto>> GetPurchaseOHPaged(int pageIndex, int pagedSize, bool trackChanges);
        Task<IEnumerable<PurchaseOrderHeaderDto>> GetCartItemEmpId(int empId, bool trackChanges);
        void EditPurchaseOH(PurchaseOrderHeaderDto purchaseOrderHeaderDto, ICollection<PurchaseOrderDetailsDto> purchaseOrderDetailsDtos);
        void Insert(PurchaseOrderHeaderForCreateDto purchaseOrderHeaderForCreateDto);
        void CreateOrder(PurchaseOrderHeaderDto purchaseOrderHeaderDto);

        void Edit(PurchaseOrderHeaderDto purchaseOrderHeaderDto);

        void Remove(PurchaseOrderHeaderDto purchaseOrderHeaderDto);
        void CreatePurchaseHeaderDetail(PurchaseOrderHeaderForCreateDto purchaseOrderHeaderDto, ICollection<PurchaseOrderDetailsForCreateDto> purchaseOrderDetailsForCreateDtos);
    }
}
