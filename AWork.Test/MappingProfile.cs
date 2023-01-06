using AutoMapper;
using AWork.Contracts.Dto.Purchasing;
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
using AWork.Contracts.Dto.HumanResources.Employee;
using AWork.Contracts.Dto.Sales.Store;
using AWork.Contracts.Dto.Sales.SalesCustomer;
using AWork.Contracts.Dto.Sales.Currency;
using AWork.Contracts.Dto.Sales.SpecialOfferProduct;
using AWork.Contracts.Dto;

namespace AWork.Test
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmailAddress, EmailAddressDto>().ReverseMap();
            CreateMap<EmailAddress, EmailAddressForCreateDto>().ReverseMap();
            CreateMap<BusinessEntity, BusinessEntityDto>().ReverseMap();
            CreateMap<BusinessEntity, BusinessEntityForCreateDto>().ReverseMap();

/*            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Address, AddressForCreateDto>().ReverseMap();

            CreateMap<ContactType, ContactTypeDto>().ReverseMap();
            CreateMap<ContactType, ContactTypeForCreateDto>().ReverseMap();

            CreateMap<CountryRegion, CountryRegionDto>().ReverseMap();
            CreateMap<CountryRegion, CountryRegionForCreateDto>().ReverseMap();*/

            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Person, PersonForCreateDto>().ReverseMap();

/*            CreateMap<StateProvince, StateProvinceDto>().ReverseMap();
            CreateMap<StateProvince, StateProvinceForCreateDto>().ReverseMap();*/

            /*CreateMap<ProductPhoto, ProductPhotoDto>().ReverseMap();
            CreateMap<ProductPhoto, ProductPhotoForCreateDto>().ReverseMap();
            */


            CreateMap<ShipMethod, ShipMethodDto>().ReverseMap();
            CreateMap<ShipMethod, ShipMethodForCreateDto>().ReverseMap();

            /*CreateMap<Vendor, VendorDto>().ReverseMap();
            CreateMap<Vendor, VendorForCreateDto>().ReverseMap();
            CreateMap<ProductVendor, ProductVendorDto>().ReverseMap();
            CreateMap<ProductVendor, ProductVendorForCreateDto>().ReverseMap();
            CreateMap<PurchaseOrderHeader, PurchaseOrderHeaderDto>().ReverseMap();
            CreateMap<PurchaseOrderHeader, PurchaseOrderHeaderForCreateDto>().ReverseMap();
            CreateMap<PurchaseOrderDetail, PurchaseOrderDetailsDto>().ReverseMap();
            CreateMap<PurchaseOrderDetail, PurchaseOrderDetailsForCreateDto>().ReverseMap();


            CreateMap<ProductCategory,ProductCategoryDto>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryForCreatDto>().ReverseMap();
            CreateMap<ProductSubcategory,ProductSubCategoryDto>().ReverseMap();
            CreateMap<ProductSubcategory, ProductSubCategoryForCreateDto>().ReverseMap();

            /*

            CreateMap<UnitMeasure, UnitMeasureDto>().ReverseMap();
            CreateMap<UnitMeasure, UnitMeasureForCreateDto>().ReverseMap();*/

            //CreateMap<Employee, EmployeeDto>().ReverseMap();


     /*       CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<Location, LocationForCreateDto>().ReverseMap();

            CreateMap<SalesTerritory, SalesTerritoryDto>().ReverseMap();
            CreateMap<SalesTerritory, SalesTerritoryForCreateDto>().ReverseMap();*/

           /* CreateMap<SalesPerson, SalesPersonDto>().ReverseMap();
            CreateMap<SalesPerson, SalesPersonForCreateDto>().ReverseMap();

            CreateMap<SalesOrderHeader, SalesOrderHeaderDto>().ReverseMap();
            CreateMap<SalesOrderHeader, SalesOrderHeaderForCreateDto>().ReverseMap();

            CreateMap<CreditCard, CreditCardDto>().ReverseMap();
            CreateMap<CreditCard, CreditCardForCreateDto>().ReverseMap();*/

       /*     CreateMap<PersonCreditCard, PersonCreditCardDto>().ReverseMap();
            CreateMap<PersonCreditCard, PersonCreditCardForCreateDto>().ReverseMap();*/

      /*      CreateMap<SpecialOffer, SpecialOfferDto>().ReverseMap();
            CreateMap<SpecialOffer, SpecialOfferForCreateDto>().ReverseMap();

            CreateMap<SpecialOfferProduct, SpecialOfferProductDto>().ReverseMap();
            CreateMap<SpecialOfferProduct, SpecialOfferProductForCreateDto>().ReverseMap();

            CreateMap<Store, StoreDto>().ReverseMap();
            CreateMap<Store, StoreForCreateDto>().ReverseMap();*/

  /*          CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, CustomerForCreateDto>().ReverseMap();*/

           /* CreateMap<Currency, CurrencyDto>().ReverseMap();
            CreateMap<Currency, CurrencyForCreateDto>().ReverseMap();

            CreateMap<WorkOrderRouting, WorkOrderRoutingDto>().ReverseMap();
            CreateMap<WorkOrderRouting, WorkOrderRoutingForCreateDto>().ReverseMap();

            CreateMap<BillOfMaterial, BillOfMaterialDto>().ReverseMap();
            CreateMap<BillOfMaterial, BillOfMaterialForCreateDto>().ReverseMap();
            CreateMap<TransactionHistory, TransactionHistoryDto>().ReverseMap();
            CreateMap<TransactionHistory, TransactionHistoryForCreateDto>().ReverseMap();

            CreateMap<ProductCostHistory, ProductCostHistoryDto>().ReverseMap();
            CreateMap<ProductCostHistory, ProductCategoryForCreatDto>().ReverseMap();

            CreateMap<ProductReview, ProductReviewDto>().ReverseMap();
            CreateMap<ProductReview, ProductReviewForCreateDto>().ReverseMap();*/


          /*  CreateMap<UnitMeasure, UnitMeasureDto>().ReverseMap();
            CreateMap<UnitMeasure, UnitMeasureForCreateDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductForCreateDto>().ReverseMap();

            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<Location, LocationForCreateDto>().ReverseMap();

            CreateMap<ProductInventory, ProductInventoryDto>().ReverseMap();
            CreateMap<ProductInventory, ProductInventoryDtoForCreate>().ReverseMap();

            CreateMap<ScrapReason, ScrapReasonDto>().ReverseMap();
            CreateMap<ScrapReason, ScrapReasonForCreateDto>().ReverseMap();

            CreateMap<WorkOrder, WorkOrderDto>().ReverseMap();
            CreateMap<WorkOrder, WorkOrderForCreateDto>().ReverseMap();*/

        }
    }
}
