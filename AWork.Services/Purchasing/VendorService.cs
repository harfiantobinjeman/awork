using AutoMapper;
using AWork.Contracts.Dto.Production;
using AWork.Contracts.Dto.Purchasing;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction;
using AWork.Services.Abstraction.Purchasing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace AWork.Services.Purchasing
{
    public class VendorService : IVendorService
    {
        private IPurchasingRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IPersonServiceManager _personService;

        public VendorService(IPurchasingRepositoryManager repositoryManager, IMapper mapper, IPersonServiceManager personService)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _personService = personService;
        }

        public void Edit(VendorDto vendorDto)
        {
            VendorDto vendor = new VendorDto()
            {
                BusinessEntityId = vendorDto.BusinessEntityId,
                AccountNumber = vendorDto.AccountNumber,
                Name = vendorDto.Name,
                CreditRating = vendorDto.CreditRating,
                PurchasingWebServiceUrl = vendorDto.PurchasingWebServiceUrl,
                ActiveFlag = vendorDto.ActiveFlag,
                PreferredVendorStatus = vendorDto.PreferredVendorStatus,
                ModifiedDate = DateTime.Now
            };
            var vendorModel = _mapper.Map<Vendor>(vendor);
            _repositoryManager.VendorRepository.Edit(vendorModel);
            _repositoryManager.Save();

            //insert productVendor baru

            foreach (var item in vendorDto.ProductVendorForCreateDtos)
            {
                if (item.MinOrderQty != 0)
                {
                    item.BusinessEntityId = vendor.BusinessEntityId;
                    item.AverageLeadTime = 5;
                    var productVendorModel = _mapper.Map<ProductVendor>(item);
                    _repositoryManager.ProductVendorRepository.Insert(productVendorModel);
                }
            }
            _repositoryManager.Save();

            //cek apakah ada data yang diupdate atau tidak
            if (vendorDto.ProductVendorDto != null)
            {
                //update data from table productVendor
                foreach (var item in vendorDto.ProductVendorDto)
                {
                    item.BusinessEntityId = vendorDto.BusinessEntityId;
                    item.AverageLeadTime = 5;
                    item.ModifiedDate = DateTime.Now;
                    var productVendorModel = _mapper.Map<ProductVendor>(item);
                    _repositoryManager.ProductVendorRepository.Edit(productVendorModel);
                };
                _repositoryManager.Save();
            }

        }

        public async Task<IEnumerable<VendorDto>> GetAllVendor(bool trackChanges)
        {
            var vendorModel = await _repositoryManager.VendorRepository.GetAllVendor(false);
            var vendorDto = _mapper.Map<IEnumerable<VendorDto>>(vendorModel);
            return vendorDto;
        }

        public async Task<VendorDto> GetVendorById(int vendorId, bool trackChanges)
        {
            var vendorModel = await _repositoryManager.VendorRepository.GetVendorById(vendorId, trackChanges);
            var vendorDto = _mapper.Map<VendorDto>(vendorModel);
            return vendorDto;
        }

        public void Insert(VendorForCreateDto vendorForCreateDto)
        {
            // create BusinessEntityId 
            var businessEntityId = _personService.BusinessEntityServices.CreateBusinessEntity();
            //input data to table vendor
            var vendor = new VendorForCreateDto()
            {
                BusinessEntityId = businessEntityId.BusinessEntityId,
                AccountNumber = vendorForCreateDto.AccountNumber,
                Name = vendorForCreateDto.Name,
                CreditRating = vendorForCreateDto.CreditRating,
                PreferredVendorStatus = vendorForCreateDto.PreferredVendorStatus,
                ActiveFlag = vendorForCreateDto.ActiveFlag,
                PurchasingWebServiceUrl = vendorForCreateDto.PurchasingWebServiceUrl,
            };
            var vendorModel = _mapper.Map<Vendor>(vendor);
            _repositoryManager.VendorRepository.Insert(vendorModel);
            _repositoryManager.Save();

            //input data to table productVendor
            foreach (var item in vendorForCreateDto.ProductVendorForCreateDtos)
            {
                item.BusinessEntityId = vendor.BusinessEntityId;
                item.AverageLeadTime = 5;
                var productVendorModel = _mapper.Map<ProductVendor>(item);
                _repositoryManager.ProductVendorRepository.Insert(productVendorModel);
            };
            _repositoryManager.Save();
        }



        public void Remove(VendorDto vendorDto)
        {
            VendorDto vendor = new VendorDto()
            {
                BusinessEntityId = vendorDto.BusinessEntityId,
                AccountNumber = vendorDto.AccountNumber,
                Name = vendorDto.Name,
                CreditRating = vendorDto.CreditRating,
                PurchasingWebServiceUrl = vendorDto.PurchasingWebServiceUrl,
                ActiveFlag = false,
                PreferredVendorStatus = vendorDto.PreferredVendorStatus,
                ModifiedDate = DateTime.Now
            };
            var vendorModel = _mapper.Map<Vendor>(vendor);
            _repositoryManager.VendorRepository.Edit(vendorModel);
            _repositoryManager.Save();

            /*var vendorModel = _mapper.Map<Vendor>(vendorDto);
            _repositoryManager.VendorRepository.Remove(vendorModel);
            _repositoryManager.Save();*/

        }

        public async Task<IEnumerable<VendorDto>> GetVendorPaged(int pageIndex, int pageSize, bool trackChanges)
        {
            var vendorModel = await _repositoryManager.VendorRepository.GetVendorPaged(pageIndex, pageSize, trackChanges);
            var vendorDto = _mapper.Map<IEnumerable<VendorDto>>(vendorModel);
            return vendorDto;
        }
    }
}
