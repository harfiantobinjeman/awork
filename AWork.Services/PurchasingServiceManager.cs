using AutoMapper;
using AWork.Domain.Base;
using AWork.Services.Abstraction;
using AWork.Services.Abstraction.Purchasing;
using AWork.Services.Purchasing;
using System;

namespace AWork.Services
{
    public class PurchasingServiceManager : IPurchasingServiceManager
    {
        private readonly IPurchasingRepositoryManager _repositoryManager;
        private readonly Lazy<IVendorService> _LazyServiceVendor;
        private readonly Lazy<IPurchaseOrderHeaderService> _LazyServicePurchaseOrderHeader;
        private readonly Lazy<IShipMethodService> _lazyShipMethodService;
        private readonly Lazy<IProductVendorService> _LazyServiceProductVendor;
        private readonly Lazy<IPurchaseOrderDetailService> _LazyServicePurchaseOrderDetail;

        public PurchasingServiceManager(IPurchasingRepositoryManager repositoryManager, IMapper mapper, IPersonServiceManager personService)
        {
            _lazyShipMethodService = new Lazy<IShipMethodService>(() => new ShipMethodService(repositoryManager, mapper));
            _LazyServiceVendor = new Lazy<IVendorService>(() => new VendorService(repositoryManager, mapper, personService));
            _LazyServicePurchaseOrderHeader = new Lazy<IPurchaseOrderHeaderService>(() => new PurchaseOrderHeaderService(repositoryManager, mapper));
            _LazyServicePurchaseOrderDetail = new Lazy<IPurchaseOrderDetailService>(() => new PurchaseOrderDetailsService(repositoryManager, mapper));
            _LazyServiceProductVendor = new Lazy<IProductVendorService>(() => new ProductVendorService(repositoryManager, mapper));
        }

        public IShipMethodService ShipMethodService => _lazyShipMethodService.Value;

        public IVendorService VendorService => _LazyServiceVendor.Value;

        public IPurchaseOrderHeaderService PurchaseOrderHeaderService => _LazyServicePurchaseOrderHeader.Value;

        public IProductVendorService ProductVendorService => _LazyServiceProductVendor.Value;

        public IPurchaseOrderDetailService PurchaseOrderDetailService => _LazyServicePurchaseOrderDetail.Value;
    }

};
