using AutoMapper;

using AWork.Contracts.Dto.Production;
using AWork.Contracts.Dto.Sales.SalesPerson;
using AWork.Contracts.Dto.Sales.SalesTerritory;
using AWork.Domain.Models;
using AWork.Contracts.Dto.Purchasing;
using AWork.Contracts.Dto.Sales.SalesOrderHeader;
using AWork.Contracts.Dto.Sales.CreditCard;
using AWork.Contracts.Dto.Sales.PersonCreditCard;
using AWork.Contracts.Dto.PersonModule;
using AWork.Contracts.Dto.Sales.SpecialOffer;
using AWork.Contracts.Dto.Sales.SpecialOfferProduct;
using AWork.Contracts.Dto.HumanResources.Employee;
using AWork.Contracts.Dto.HumanResources;
using AWork.Contracts.Dto.HumanResources.EmployeePayHistory;
using AWork.Contracts.Dto.HumanResources.JobCandidate;
using AWork.Contracts.Dto.Authentication;
using AWork.Contracts.Dto;
using AWork.Contracts.Dto.PersonModule.Profile;
using AWork.Contracts.Dto.Sales.ShoppingCartItem;
using AWork.Contracts.Dto.Sales.Store;
using AWork.Contracts.Dto.Sales.SalesCustomer;
using AWork.Contracts.Dto.Sales.SalesOrderDetail;
using System.Linq;

namespace AWork.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, EmployeeForCreateDto>().ReverseMap();

            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Department, DepartmentForCreateDto>().ReverseMap();

            CreateMap<Shift, ShiftDto>().ReverseMap();
            CreateMap<Shift, ShiftForCreateDto>().ReverseMap();

            CreateMap<EmployeeDepartmentHistory, EmployeeDepartmentHistoryDto>().ReverseMap();
            CreateMap<EmployeeDepartmentHistory, EmployeeDepartmentHistoryForCreateDto>().ReverseMap();

            CreateMap<EmployeePayHistory, EmployeePayHistoryDto>().ReverseMap();
            CreateMap<EmployeePayHistory, EmployeePHForCreateDto>().ReverseMap();

            CreateMap<EmailAddress, EmailAddressDto>().ReverseMap();
            CreateMap<EmailAddressDto, ProfileDto>().ReverseMap();

            CreateMap<BusinessEntityAddress, BusinessEntityAddressDto>().ReverseMap();
            CreateMap<BusinessEntityAddress, BusinessEntityAddressForCreateDto>().ReverseMap();
            CreateMap<BusinessEntity, ProfileDto>().ReverseMap();
            


            CreateMap<EmailAddress, EmailAddressDto>()
                .ForPath(e => e.BusinessEntity, ee => ee.MapFrom(e => e.BusinessEntity))
                .ReverseMap();
            CreateMap<PersonPhone, PersonPhoneDto>()
                .ForPath(p => p.BusinessEntityId, pp => pp.MapFrom(p => p.BusinessEntityId))
                .ReverseMap();

            CreateMap<PersonPhone, PersonPhoneForCreateDto>().ReverseMap();

            CreateMap<EmailAddress, EmailAddressForCreateDto>().ReverseMap();

            CreateMap<JobCandidate, JobCandidateDto>().ReverseMap();
            CreateMap<JobCandidate, JobCandidateForCreateDto>().ReverseMap();

            CreateMap<BusinessEntity, BusinessEntityDto>().ReverseMap();
            CreateMap<BusinessEntity, BusinessEntityForCreateDto>().ReverseMap();

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Address, AddressForCreateDto>().ReverseMap();

            CreateMap<AddressType, AddressTypeDto>().ReverseMap();
            CreateMap<AddressType, AddressTypeForCreateDto>().ReverseMap();

            CreateMap<PhoneNumberType, PhoneNumberTypeDto>().ReverseMap();
            CreateMap<PhoneNumberType, PhoneNumberTypeForCreateDto>().ReverseMap();

            CreateMap<ContactType, ContactTypeDto>().ReverseMap();
            CreateMap<ContactType, ContactTypeForCreateDto>().ReverseMap();

            CreateMap<CountryRegion, CountryRegionDto>().ReverseMap();
            CreateMap<CountryRegion, CountryRegionForCreateDto>().ReverseMap();

            CreateMap<Person, PersonDto>()
                .ForPath(p => p.EmailAddressesDto , pp => pp.MapFrom(p => p.EmailAddresses))
                .ForPath(p => p.PersonPhonesDto, pp => pp.MapFrom(p => p.PersonPhones))
                .ReverseMap();
            
            CreateMap<Person, ProfileDto>()
                .ReverseMap();
            CreateMap<Person, PersonForCreateDto>().ReverseMap();

            CreateMap<StateProvince, StateProvinceDto>().ReverseMap();
            CreateMap<StateProvince, StateProvinceForCreateDto>().ReverseMap();

            /* CreateMap<ProductPhoto, ProductPhotoDto>().ReverseMap();
             CreateMap<ProductPhoto, ProductPhotoForCreateDto>().ReverseMap();
             CreateMap<ProductProductPhoto, ProductProductPhotoDto>().ReverseMap();


            CreateMap<ShipMethod, ShipMethodDto>().ReverseMap();
            CreateMap<ShipMethod, ShipMethodForCreateDto>().ReverseMap();*

             CreateMap<Vendor, VendorDto>()
                 .ForPath(p => p.ProductVendorDto, pp => pp.MapFrom(p => p.ProductVendors))
                 .ReverseMap();
             CreateMap<Vendor, VendorForCreateDto>().ReverseMap();
             CreateMap<ProductVendor, ProductVendorDto>().ReverseMap();
             CreateMap<ProductVendor, ProductVendorForCreateDto>().ReverseMap();
             CreateMap<PurchaseOrderHeader, PurchaseOrderHeaderDto>().ReverseMap();
             CreateMap<PurchaseOrderHeader, PurchaseOrderHeaderForCreateDto>().ReverseMap();
             CreateMap<PurchaseOrderDetail, PurchaseOrderDetailsDto>().ReverseMap();
             CreateMap<PurchaseOrderDetail, PurchaseOrderDetailsForCreateDto>().ReverseMap();


             CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();
             CreateMap<ProductCategory, ProductCategoryForCreatDto>().ReverseMap();
             CreateMap<ProductSubcategory, ProductSubCategoryDto>().ReverseMap();
             CreateMap<ProductSubcategory, ProductSubCategoryForCreateDto>().ReverseMap();



            CreateMap<UnitMeasure, UnitMeasureDto>().ReverseMap();
            CreateMap<UnitMeasure, UnitMeasureForCreateDto>().ReverseMap();*/

            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Department, DepartmentForCreateDto>().ReverseMap();

            CreateMap<Shift, ShiftDto>().ReverseMap();
            CreateMap<Shift, ShiftForCreateDto>().ReverseMap();

            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<Location, LocationForCreateDto>().ReverseMap();

            CreateMap<SalesTerritory, SalesTerritoryDto>().ReverseMap();
            CreateMap<SalesTerritory, SalesTerritoryForCreateDto>().ReverseMap();

            CreateMap<SalesOrderHeader, SalesOrderHeaderDto>().ReverseMap();
            CreateMap<SalesOrderHeader, ProductOnSaleDto>()
                .ForPath(p => p.salesOrderHeader.ShipMethod, pp => pp.MapFrom(p => p.ShipMethod))
                .ForPath(p => p.salesOrderHeader.Customer, pp => pp.MapFrom(p => p.Customer))
                .ForPath(p => p.salesOrderHeader.CreditCard, pp => pp.MapFrom(p => p.CreditCard))
                .ForPath(p => p.salesOrderHeader.SalesOrderDetails, pp => pp.MapFrom(p => p.SalesOrderDetails))
                .ReverseMap();
            CreateMap<SalesOrderHeader, SalesOrderHeaderForCreateDto>().ReverseMap();

            CreateMap<CreditCard, CreditCardDto>().ReverseMap();
            CreateMap<CreditCard, CreditCardForCreateDto>().ReverseMap();

            CreateMap<PersonCreditCard, PersonCreditCardDto>().ReverseMap();
            CreateMap<PersonCreditCard, PersonCreditCardForCreateDto>().ReverseMap();

            CreateMap<SpecialOffer, SpecialOfferDto>().ReverseMap();
            CreateMap<SpecialOffer, SpecialOfferForCreateDto>().ReverseMap();

            CreateMap<SpecialOfferProduct, SpecialOfferProductDto>().ReverseMap();
            CreateMap<SpecialOfferProduct, SpecialOfferProductForCreateDto>().ReverseMap();

            CreateMap<WorkOrderRouting, WorkOrderRoutingDto>().ReverseMap();
            CreateMap<WorkOrderRouting, WorkOrderRoutingForCreateDto>().ReverseMap();

            CreateMap<BillOfMaterial, BillOfMaterialDto>().ReverseMap();
            CreateMap<BillOfMaterial, BillOfMaterialForCreateDto>().ReverseMap();
            CreateMap<TransactionHistory, TransactionHistoryDto>().ReverseMap();
            CreateMap<TransactionHistory, TransactionHistoryForCreateDto>().ReverseMap();

            CreateMap<ProductCostHistory, ProductCostHistoryDto>().ReverseMap();
            CreateMap<ProductCostHistory, ProductCategoryForCreatDto>().ReverseMap();

            CreateMap<ProductReview, ProductReviewDto>().ReverseMap();
            CreateMap<ProductReview, ProductReviewForCreateDto>().ReverseMap();


            CreateMap<UnitMeasure, UnitMeasureDto>().ReverseMap();
            CreateMap<UnitMeasure, UnitMeasureForCreateDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductOnSaleDto>()
                .ForPath(p => p.product.ProductId, pp => pp.MapFrom(p => p.ProductId))
                .ForPath(p => p.product.Name, pp => pp.MapFrom(p => p.Name))
                .ForPath(p => p.product.ListPrice, pp => pp.MapFrom(p => p.ListPrice))
                .ForPath(p => p.product.SafetyStockLevel, pp => pp.MapFrom(p => p.SafetyStockLevel))
                .ForPath(p => p.product.ProductProductPhotos, pp => pp.MapFrom(p => p.ProductProductPhotos))
                .ReverseMap();
            CreateMap<Product, ProductForCreateDto>().ReverseMap();

            CreateMap<ProductInventory, ProductInventoryDto>().ReverseMap();
            CreateMap<ProductInventory, ProductInventoryDtoForCreate>().ReverseMap();

            CreateMap<ScrapReason, ScrapReasonDto>().ReverseMap();
            CreateMap<ScrapReason, ScrapReasonForCreateDto>().ReverseMap();

            CreateMap<WorkOrder, WorkOrderDto>().ReverseMap();
            CreateMap<WorkOrder, WorkOrderForCreateDto>().ReverseMap();
            
            CreateMap<UserRegistrationDto, User>()
           .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<UserLoginDto, User>().ReverseMap();

            CreateMap<ProductPhoto, ProductPhotoDto>().ReverseMap();
            CreateMap<ProductPhoto, ProductPhotoForCreateDto>().ReverseMap();
            CreateMap<ProductProductPhoto, ProductPhotoDto>().ReverseMap();
            CreateMap<ProductProductPhoto, ProductProductPhotoDto>().ReverseMap();
            CreateMap<ProductProductPhoto, ProductProductPhotoForCreateDto>().ReverseMap();

            CreateMap<ShipMethod, ShipMethodDto>().ReverseMap();
            CreateMap<ShipMethod, ShipMethodForCreateDto>().ReverseMap();

            CreateMap<Vendor, VendorDto>()
                .ForPath(p => p.ProductVendorDto, pp => pp.MapFrom(p => p.ProductVendors))
                .ReverseMap();
            CreateMap<Vendor, VendorForCreateDto>().ReverseMap();

            CreateMap<ProductVendor, ProductVendorDto>().ReverseMap();
            CreateMap<ProductVendor, ProductVendorForCreateDto>().ReverseMap();

            CreateMap<PurchaseOrderHeader, PurchaseOrderHeaderDto>()
                .ForPath(p=>p.GetPurchaseOD, pp=>pp.MapFrom(p=>p.PurchaseOrderDetails))
                .ReverseMap();
            CreateMap<PurchaseOrderHeader, PurchaseOrderHeaderForCreateDto>().ReverseMap();

            CreateMap<PurchaseOrderDetail, PurchaseOrderDetailsDto>().ReverseMap();
            CreateMap<PurchaseOrderDetail, PurchaseOrderDetailsForCreateDto>().ReverseMap();

            //CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryDto>()
                .ForPath(p => p.ProductSubCategoryDto, pp => pp.MapFrom(p => p.ProductSubcategories))
                .ReverseMap();
            CreateMap<ProductCategory, ProductCategoryForCreatDto>().ReverseMap();

            CreateMap<ProductSubcategory, ProductSubCategoryDto>().ReverseMap();
            CreateMap<ProductSubcategory, ProductSubCategoryForCreateDto>().ReverseMap();
            CreateMap<SalesPerson, SalesPersonDto>()
                .ForPath(p => p.StoreDtos, pp => pp.MapFrom(p => p.Stores))
                .ReverseMap();
            CreateMap<SalesPerson, SalesPersonForCreateDto>().ReverseMap();

            CreateMap<ShoppingCartItem, ShoppingCartItemDto>().ReverseMap();
            CreateMap<ShoppingCartItem, ProductOnSaleDto>()
                .ForPath(p => p.shoppingCartItem.ProductId, pp => pp.MapFrom(p => p.ProductId))
                .ForPath(p => p.shoppingCartItem.Quantity, pp => pp.MapFrom(p => p.Quantity))
                .ForPath(p => p.shoppingCartItem.Product, pp => pp.MapFrom(p => p.Product))
                .ForPath(p => p.shoppingCartItem.ShoppingCartId, pp => pp.MapFrom(p => p.ShoppingCartId))
                .ForPath(p => p.shoppingCartItem.ShoppingCartItemId, pp => pp.MapFrom(p => p.ShoppingCartItemId))
                .ForPath(p => p.shoppingCartItem.ModifiedDate, pp => pp.MapFrom(p => p.ModifiedDate))
                .ForPath(p => p.shoppingCartItem.DateCreated, pp => pp.MapFrom(p => p.DateCreated))
                .ReverseMap();
            CreateMap<ShoppingCartItem, ShoppingCartItemForCreateDto>().ReverseMap();

            CreateMap<Store, StoreDto>().ReverseMap();
            CreateMap<Store, StoreForCreateDto>().ReverseMap();

            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, CustomerForCreateDto>().ReverseMap();

            CreateMap<SalesOrderDetail, SalesOrderDetailDto>().ReverseMap();
            CreateMap<SalesOrderDetail, SalesOrderDetailForCreateDto>().ReverseMap();
        }
    }
}
