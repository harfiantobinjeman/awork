/*using AutoMapper;
using AWork.Contracts.Dto.Purchasing;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Persistence;
using AWork.Persistence.Base;
using AWork.Persistence.Repositories.Purchasing;
using AWork.Services;
using AWork.Services.Abstraction;
using AWork.Test.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using Northwind.Contracts.Dto.Category;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AWork.Test.Fajar.Test
{
    public class FajarTest
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionsBuilder;

        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IPurchasingRepositoryManager repositoryManager;

        public FajarTest()
        {
            BuilderConfiguration();
            SetupOptions();
        }
        *//*[Fact]
        public void TestGetPurchaseOHService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                repositoryManager = new PurchasingRepositoryManager(context);
                IPurchasingServiceManager serviceManager = new PurchasingServiceManager(repositoryManager, mapper);
                var purchase = serviceManager.PurchaseOrderHeaderService.GetAllPurchaseOH(false);
                purchase.ShouldNotBeNull();
                purchase.Result.Count().ShouldBe(4023);
            }
        }*/
        /*[Fact]
        public void TestGetPurchaseODService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                repositoryManager = new PurchasingRepositoryManager(context);
                IPurchasingServiceManager serviceManager = new PurchasingServiceManager(repositoryManager, mapper);
                var purchase = serviceManager.PurchaseOrderDetailService.GetAllPurchaseOD(false);
                purchase.ShouldNotBeNull();
                purchase.Result.Count().ShouldBe(8845);
            }
        }*/
        /* [Fact]
         public void TestCreatePurchaseOHService()
         {
             using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
             {
                 repositoryManager = new PurchasingRepositoryManager(context);
                 IPurchasingServiceManager serviceManager = new PurchasingServiceManager(repositoryManager, mapper);
                 var purchaseDto = new PurchaseOrderHeaderForCreateDto
                 {
                     VendorId = 1650,
                     EmployeeId = 250,
                     RevisionNumber = 3,
                     ShipMethodId = 3,
                     ShipDate = DateTime.Now.AddDays(5)
                 };
                 serviceManager.PurchaseOrderHeaderService.Insert(purchaseDto);
                 var purchase = serviceManager.PurchaseOrderHeaderService.GetAllPurchaseOH(false);
                 purchase.ShouldNotBeNull();
                 purchase.Result.Count().ShouldBe(4028);
                 purchaseDto.PurchaseOrderId.ShouldSatisfyAllConditions();
             }
         }*/
        /*    [Fact]
            public void TestCreatePurchaseODService()
            {
                using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
                {
                    repositoryManager = new PurchasingRepositoryManager(context);
                    IPurchasingServiceManager serviceManager = new PurchasingServiceManager(repositoryManager, mapper);
                    var purchaseDto = new PurchaseOrderDetailsForCreateDto
                    {
                        OrderQty = 7999,
                        PurchaseOrderId = 4010,
                        ProductId =874,
                        UnitPrice = 3.1M,
                        LineTotal = 18880,
                        ReceivedQty = 7998,
                        RejectedQty = 0,
                        StockedQty = 8000,
                        DueDate = DateTime.Now
                    };
                    serviceManager.PurchaseOrderDetailService.Insert(purchaseDto);
                    var purchase = serviceManager.PurchaseOrderDetailService.GetAllPurchaseOD(false);
                    purchase.ShouldNotBeNull();
                    purchase.Result.Count().ShouldBe(8847);
                    purchaseDto.PurchaseOrderDetailId.ShouldSatisfyAllConditions();
                }
            }*/

        /*[Fact]
        public void TestEditPurchaseOHService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                repositoryManager = new PurchasingRepositoryManager(context);
                IPurchasingServiceManager serviceManager = new PurchasingServiceManager(repositoryManager, mapper);
                var purchaseDto = new PurchaseOrderHeaderDto
                {
                    VendorId = 1650,
                    EmployeeId = 258,
                    RevisionNumber = 3,
                    ShipMethodId = 5,
                };
                serviceManager.PurchaseOrderHeaderService.Edit(purchaseDto);
                var purchase = serviceManager.PurchaseOrderHeaderService.GetAllPurchaseOH(false);
                purchase.ShouldNotBeNull();
                purchase.Result.Count().ShouldBe(4033);
                purchaseDto.ShouldSatisfyAllConditions();
            }
        }*/
        /*[Fact]
        public void TestEditPurchaseODService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                repositoryManager = new PurchasingRepositoryManager(context);
                IPurchasingServiceManager serviceManager = new PurchasingServiceManager(repositoryManager, mapper);
                var purchaseDto = new PurchaseOrderDetailsDto
                {
                    OrderQty = 6999,
                    PurchaseOrderId = 4010,
                    ProductId = 874,
                    UnitPrice = 2.7M,
                    LineTotal = 10800,
                    ReceivedQty = 7800,
                    RejectedQty = 0,
                    StockedQty = 8000,
                    DueDate = DateTime.Now
                };
                serviceManager.PurchaseOrderDetailService.Edit(purchaseDto);
                var purchase = serviceManager.PurchaseOrderDetailService.GetAllPurchaseOD(false);
                purchase.ShouldNotBeNull();
                purchase.Result.Count().ShouldBe(8850);
                purchaseDto.ShouldSatisfyAllConditions();
            }
        }*/
        /* [Fact]
         public void TestRemovePurchaseOHService()
         {
             using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
             {
                 repositoryManager = new PurchasingRepositoryManager(context);
                 IPurchasingServiceManager serviceManager = new PurchasingServiceManager(repositoryManager, mapper);
                 var purchaseDto = new PurchaseOrderHeaderDto
                 {
                     PurchaseOrderId = 4056,
                     VendorId = 1650,
                     EmployeeId = 258,
                     RevisionNumber = 3,
                     ShipMethodId = 5,
                 };
                 serviceManager.PurchaseOrderHeaderService.Remove(purchaseDto);
                 var purchase = serviceManager.PurchaseOrderHeaderService.GetAllPurchaseOH(false);
                 purchase.ShouldNotBeNull();
                 purchase.Result.Count().ShouldBe(4029);
                 purchaseDto.ShouldSatisfyAllConditions();
             }
         }*//*
        [Fact]
        public void TestRemovePurchaseODService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                repositoryManager = new PurchasingRepositoryManager(context);
                IPurchasingServiceManager serviceManager = new PurchasingServiceManager(repositoryManager, mapper);
                var purchaseDto = new PurchaseOrderDetailsDto
                {
                    PurchaseOrderDetailId = 8846,
                    PurchaseOrderId = 4010,
                    ProductId = 874,
                    UnitPrice = 2.36M,
                    LineTotal = 18877.64M,
                    ReceivedQty = 7999,
                    RejectedQty = 0,
                    StockedQty = 7999,
                };
                serviceManager.PurchaseOrderDetailService.Remove(purchaseDto);
                var purchase = serviceManager.PurchaseOrderDetailService.GetAllPurchaseOD(false);
                purchase.ShouldNotBeNull();
                purchase.Result.Count().ShouldBe(8850);
            }
        }


        public void BuilderConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }
        private void SetupOptions()
        {
            optionsBuilder = new DbContextOptionsBuilder<AdventureWorks2019Context>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("AdventureWorks2019Db"));

            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(MappingProfile));
            serviceProvider = services.BuildServiceProvider();

            mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            mapperConfig.AssertConfigurationIsValid();
            mapper = mapperConfig.CreateMapper();

        }
    }
}
*/
