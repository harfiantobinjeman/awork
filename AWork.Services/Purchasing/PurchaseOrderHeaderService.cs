using AutoMapper;
using AWork.Contracts.Dto.Purchasing;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction.Purchasing;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Purchasing
{
    public class PurchaseOrderHeaderService : IPurchaseOrderHeaderService
    {
        private readonly IPurchasingRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public PurchaseOrderHeaderService(IPurchasingRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PurchaseOrderHeaderDto>> GetAllPurchaseOH(bool trackChanges)
        {
            var purchaseOHModel = await _repositoryManager.PurchaseOrderHeaderRepository.GetAllPurchaseOH(trackChanges);
            var purchaseOHDto = _mapper.Map<IEnumerable<PurchaseOrderHeaderDto>>(purchaseOHModel);
            return purchaseOHDto;
        }

        public async Task<PurchaseOrderHeaderDto> GetPurchaseOHById(int purchaseOrderOHId, bool trackChanges)
        {
            var purchaseModel = await _repositoryManager.PurchaseOrderHeaderRepository.GetPurchaseOrderHeaderById(purchaseOrderOHId, trackChanges);
            var purchaseDto = _mapper.Map<PurchaseOrderHeaderDto>(purchaseModel);
            return purchaseDto;
        }

        public void Insert(PurchaseOrderHeaderForCreateDto purchaseOrderHeaderForCreateDto)
        {
            var purchaseModel = _mapper.Map<PurchaseOrderHeader>(purchaseOrderHeaderForCreateDto);
            purchaseOrderHeaderForCreateDto.PurchaseOrderId = purchaseModel.PurchaseOrderId;
            purchaseModel.PurchaseOrderId = purchaseOrderHeaderForCreateDto.PurchaseOrderId;
            _repositoryManager.PurchaseOrderHeaderRepository.Insert(purchaseModel);
            _repositoryManager.Save();
        }

        public void Edit(PurchaseOrderHeaderDto purchaseOrderHeaderDto)
        {
            var purchaseModel = _mapper.Map<PurchaseOrderHeader>(purchaseOrderHeaderDto);
            _repositoryManager.PurchaseOrderHeaderRepository.Edit(purchaseModel);
            _repositoryManager.Save();
        }


        public void Remove(PurchaseOrderHeaderDto purchaseOrderHeaderDto)
        {
            var purchaseModel = _mapper.Map<PurchaseOrderHeader>(purchaseOrderHeaderDto);
            _repositoryManager.PurchaseOrderHeaderRepository.Remove(purchaseModel);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<PurchaseOrderHeaderDto>> FilterEmpId(int empId, bool trackChanges)
        {
            var model = await _repositoryManager.PurchaseOrderHeaderRepository.FilterEmpId(empId, trackChanges);
            var dto = _mapper.Map<IEnumerable<PurchaseOrderHeaderDto>>(model);
            return dto;
        }

        public void CreateOrder(PurchaseOrderHeaderDto purchaseOrderHeaderDto)
        {
            var purchaseModel = _mapper.Map<PurchaseOrderHeader>(purchaseOrderHeaderDto);
            _repositoryManager.PurchaseOrderHeaderRepository.Insert(purchaseModel);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<PurchaseOrderHeaderDto>> GetPurchaseOHPaged(int pageIndex, int pagedSize, bool trackChanges)
        {
            var purchaseODModel = await _repositoryManager.PurchaseOrderHeaderRepository.GetPurchaseOHPaged(pageIndex, pagedSize, trackChanges);
            var purchaseODDto = _mapper.Map<IEnumerable<PurchaseOrderHeaderDto>>(purchaseODModel);
            return purchaseODDto;
        }

        public void EditPurchaseOH(PurchaseOrderHeaderDto purchaseOrderHeaderDto, ICollection<PurchaseOrderDetailsDto> purchaseOrderDetailsDtos)
        {
            var purchaseModel = _mapper.Map<PurchaseOrderHeader>(purchaseOrderHeaderDto);
            _repositoryManager.PurchaseOrderHeaderRepository.Edit(purchaseModel);
            _repositoryManager.Save();

            foreach (var item in purchaseOrderDetailsDtos)
            {
                var purchases = _mapper.Map<PurchaseOrderDetail>(item);
                _repositoryManager.PurchaseOrderDetailRepository.Edit(purchases);
            }
            _repositoryManager.Save();
        }

        public void CreatePurchaseHeaderDetail(PurchaseOrderHeaderForCreateDto purchaseOrderHeaderDto, ICollection<PurchaseOrderDetailsForCreateDto> purchaseOrderDetailsForCreateDtos)
        {
            var purchase = _mapper.Map<PurchaseOrderHeader>(purchaseOrderHeaderDto);
            _repositoryManager.PurchaseOrderHeaderRepository.Insert(purchase);
            _repositoryManager.Save();

            foreach (var item in purchaseOrderDetailsForCreateDtos)
            {
                var pod = _mapper.Map<PurchaseOrderDetail>(item);
                pod.PurchaseOrderId = purchase.PurchaseOrderId;
                _repositoryManager.PurchaseOrderDetailRepository.Insert(pod);
            }
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<PurchaseOrderHeaderDto>> GetCartItemEmpId(int empId, bool trackChanges)
        {
            var cartItem = await _repositoryManager.PurchaseOrderHeaderRepository.GetCartItemByCustId(empId, false);
            var cartModel = _mapper.Map<IEnumerable<PurchaseOrderHeaderDto>>(cartItem);
            return cartModel;
        }
    }
}
