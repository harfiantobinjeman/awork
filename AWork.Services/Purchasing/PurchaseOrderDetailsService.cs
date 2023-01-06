using AutoMapper;
using AWork.Contracts.Dto.Purchasing;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Persistence;
using AWork.Services.Abstraction.Purchasing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Purchasing
{
    public class PurchaseOrderDetailsService : IPurchaseOrderDetailService
    {
        private IPurchasingRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public PurchaseOrderDetailsService(IPurchasingRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public void Edit(PurchaseOrderDetailsDto purchaseOrderDetailsDto)
        {
            var PurchaseODModel = _mapper.Map<PurchaseOrderDetail>(purchaseOrderDetailsDto);
            _repositoryManager.PurchaseOrderDetailRepository.Edit(PurchaseODModel);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<PurchaseOrderDetailsDto>> GetAllPurchaseOD(bool trackChanges)
        {
            var PurchaseODModel = await _repositoryManager.PurchaseOrderDetailRepository.GetAllPurchaseOrderDetail(false);
            var PurchaseODDto = _mapper.Map<IEnumerable<PurchaseOrderDetailsDto>>(PurchaseODModel);
            return PurchaseODDto;
        }

        public async Task<PurchaseOrderDetailsDto> GetPurchaseODById(int purchaseODId, bool trackChanges)
        {
            var PurchaseODModel = await _repositoryManager.PurchaseOrderDetailRepository.GetPurchaseOrderDetailById(purchaseODId, trackChanges);
            var PurchaseODDto = _mapper.Map<PurchaseOrderDetailsDto>(PurchaseODModel);
            return PurchaseODDto;
        }

        public void Insert(PurchaseOrderDetailsForCreateDto purchaseOrderDetailsForCreateDto)
        {
            var PurchaseODModel = _mapper.Map<PurchaseOrderDetail>(purchaseOrderDetailsForCreateDto);
            _repositoryManager.PurchaseOrderDetailRepository.Insert(PurchaseODModel);
            _repositoryManager.Save();
        }

        public void Remove(PurchaseOrderDetailsDto purchaseOrderDetailsDto)
        {
            var PurchaseODModel = _mapper.Map<PurchaseOrderDetail>(purchaseOrderDetailsDto);
            _repositoryManager.PurchaseOrderDetailRepository.Remove(PurchaseODModel);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<PurchaseOrderDetailsDto>> GetPurchaseODPaged(int pageIndex, int pagedSize, bool trackChanges)
        {
            var purchaseODModel = await _repositoryManager.PurchaseOrderDetailRepository.GetPurchaseODPaged(pageIndex, pagedSize, trackChanges);
            var purchaseODDto = _mapper.Map<IEnumerable<PurchaseOrderDetailsDto>>(purchaseODModel);
            return purchaseODDto;
        }

        public async Task<PurchaseOrderDetailsDto> GetPurchaseOrderDetails(int orderId, int productId, bool trackChanges)
        {
            var model = await _repositoryManager.PurchaseOrderDetailRepository.GetPurchaseOrderDetail(orderId, productId, trackChanges);
            var dto = _mapper.Map<PurchaseOrderDetailsDto>(model);
            return dto;
        }

        public void CreateOrderDetail(PurchaseOrderDetailsDto purchaseOrderDetailsDto)
        {
            var PurchaseODModel = _mapper.Map<PurchaseOrderDetail>(purchaseOrderDetailsDto);
            _repositoryManager.PurchaseOrderDetailRepository.Insert(PurchaseODModel);
            _repositoryManager.Save();
        }

        public void EditPurchase(PurchaseOrderDetailsDto purchaseOrderDetailsDto, ShipMethodDto shipMethodDto, PurchaseOrderHeaderDto purchaseOrderHeaderDto)
        {
            // edit into table purchaseod
            var purchaseEdit = _mapper.Map<PurchaseOrderDetail>(purchaseOrderDetailsDto);
            /*var pur = _repositoryManager.PurchaseOrderDetailRepository.GetPurchaseOrderDetailById((int)id, true);
            var puro = _repositoryManager.PurchaseOrderHeaderRepository.GetPurchaseOrderHeaderById((int)id, true);
            var shi = _repositoryManager.ShipMethodRepository.GetShipMethodById((int)id, true);*/
            if(purchaseEdit != null)
            {
                purchaseEdit.OrderQty = purchaseOrderDetailsDto.OrderQty;
                purchaseEdit.PurchaseOrderId = purchaseOrderDetailsDto.PurchaseOrderId;
                purchaseEdit.PurchaseOrderDetailId = purchaseOrderDetailsDto.PurchaseOrderDetailId;
                purchaseEdit.ProductId = purchaseOrderDetailsDto.ProductId;
                purchaseEdit.UnitPrice = purchaseOrderDetailsDto.UnitPrice;
                purchaseEdit.LineTotal = purchaseOrderDetailsDto.LineTotal;
                /*purchaseEdit = purchaseOrderDetailsDto.PurchaseOrder.PurchaseOrderDetails;*/
                /*purchaseEdit.Product.Equals(purchaseOrderDetailsDto.Product);*/
            }
            _repositoryManager.PurchaseOrderDetailRepository.Edit(purchaseEdit);
            _repositoryManager.Save();

            // edit into table purchaseoh
            var purchaseohEdit = _mapper.Map<PurchaseOrderHeader>(purchaseOrderHeaderDto);
            if(purchaseohEdit != null)
            {
                purchaseohEdit.PurchaseOrderId = purchaseEdit.PurchaseOrderId;
                purchaseohEdit.VendorId = purchaseEdit.PurchaseOrder.VendorId;
                purchaseohEdit.ShipMethodId = purchaseEdit.PurchaseOrder.ShipMethodId;
                purchaseohEdit.ShipDate = DateTime.Now.AddDays(3);
            }
            /*purchaseohEdit.PurchaseOrderId = purchaseEdit.PurchaseOrderId;
            purchaseohEdit.VendorId = purchaseEdit.PurchaseOrder.VendorId;
            purchaseohEdit.ShipMethodId = purchaseEdit.PurchaseOrder.ShipMethodId;
            purchaseohEdit.EmployeeId = purchaseEdit.PurchaseOrder.EmployeeId;*/
            _repositoryManager.PurchaseOrderHeaderRepository.Edit(purchaseohEdit);
            _repositoryManager.Save();

            //edit into table shipmethod
            var shipEdit = _mapper.Map<ShipMethod>(shipMethodDto);
            if(shipEdit != null)
            {
                shipEdit.ShipMethodId = shipMethodDto.ShipMethodId;
                shipEdit.Name = shipMethodDto.Name;

            }
            _repositoryManager.ShipMethodRepository.Edit(shipEdit);
            _repositoryManager.Save();

            
        }

        public void UpdateData(int id, PurchaseOrderDetailsDto purchaseOrderDetail, PurchaseOrderHeaderDto purchaseOrderHeader, ShipMethodDto shipMethod)
        {

        }
        public async Task<IEnumerable<PurchaseOrderDetailsDto>> GetPurchaseODByIdd(int purchaseId, bool trackChanges)
        {
            var PurchaseODModel = await _repositoryManager.PurchaseOrderDetailRepository.GetPurchaseOrderDetailByIdd(purchaseId,trackChanges);
            /*var PurchaseODDto = _mapper.Map<IEnumerable<PurchaseOrderDetail>>(PurchaseODModel);*/
            var purchaseModel = _mapper.Map<IEnumerable<PurchaseOrderDetailsDto>>(PurchaseODModel);
            return purchaseModel;
        }

        public void EditPurchases(IEnumerable<PurchaseOrderDetailsDto> purchaseOrderDetailsDtos)
        {
            var purchase = _mapper.Map<IEnumerable<PurchaseOrderDetailsDto>>(purchaseOrderDetailsDtos);
            var purchaseOrderDetail = _mapper.Map<PurchaseOrderDetail>(purchase);
            _repositoryManager.PurchaseOrderDetailRepository.Edit(purchaseOrderDetail);
            _repositoryManager.Save();
        }
    }
}
