using AutoMapper;
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
namespace AWork.Test.Farid.Test
{
    public class FaridIntegrationTest
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionsBuilder;

        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IPurchasingRepositoryManager repositoryManager;
        public FaridIntegrationTest()
        {
            BuilderConfiguration();
            SetupOptions();
        }


        /*[Fact]
        public void TestGetVendorService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                repositoryManager = new PurchasingRepositoryManager(context);
                IPurchasingServiceManager serviceManager = new PurchasingServiceManager(repositoryManager, mapper);
                var Vendor = serviceManager.VendorService.GetAllVendor(false);

                //assert
                Vendor.ShouldNotBeNull();
                Vendor.Result.Count().ShouldBe(108);
            }
        }*/

        /*[Fact]
        public void TestCreateVendorService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                repositoryManager = new PurchasingRepositoryManager(context);
                IPurchasingServiceManager serviceManager = new PurchasingServiceManager(repositoryManager, mapper);
                var VendorDto = new VendorForCreateDto
                {
                    BusinessEntityId = 4,
                    AccountNumber = "Lazada01",
                    Name = "Lazada",
                    CreditRating = 5,
                    PreferredVendorStatus = true,
                    ActiveFlag = true,
                    PurchasingWebServiceUrl = "www.lazada.com"
                };
                serviceManager.VendorService.Insert(VendorDto);

                //assert
                var Vendor = serviceManager.VendorService.GetAllVendor(false);
                Vendor.ShouldNotBeNull();
                Vendor.Result.Count().ShouldBe(108);

            }
        }*/

        /*[Fact]
        public void TestEditVendorService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                repositoryManager = new PurchasingRepositoryManager(context);
                IPurchasingServiceManager serviceManager = new PurchasingServiceManager(repositoryManager, mapper);
                var VendorDto = new VendorDto
                {
                    BusinessEntityId = 4,
                    AccountNumber = "Lazada01",
                    Name = "Lazada Selalu",
                    CreditRating = 5,
                    PreferredVendorStatus = true,
                    ActiveFlag = true,
                    PurchasingWebServiceUrl = "www.lazada.com",
                    ModifiedDate = DateTime.Now
                };
                serviceManager.VendorService.Edit(VendorDto);
            }
        }*/

        /*[Fact]
        public void TestGetProductVendorService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                repositoryManager = new PurchasingRepositoryManager(context);
                IPurchasingServiceManager serviceManager = new PurchasingServiceManager(repositoryManager, mapper);
                var productVendor = serviceManager.ProductVendorService.GetAllProductVendor(false);
                productVendor.ShouldNotBeNull();
                productVendor.Result.Count().ShouldBe(460);
            }
        }*/

        /*[Fact]
        public void TestCreateProductVendorService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                repositoryManager = new PurchasingRepositoryManager(context);
                IPurchasingServiceManager serviceManager = new PurchasingServiceManager(repositoryManager, mapper);
                var productVendorDto = new ProductVendorForCreateDto
                {
                    ProductId = 316,
                    BusinessEntityId = 3,
                    AverageLeadTime = 18,
                    StandardPrice = 60,
                    LastReceiptCost = 65,
                    LastReceiptDate = new DateTime(2022, 4, 18),
                    MinOrderQty = 2,
                    MaxOrderQty = 8,
                    OnOrderQty = 5,
                    UnitMeasureCode = "CTN"

                };
                serviceManager.ProductVendorService.Insert(productVendorDto);
                // assert 
                var vendor = serviceManager.ProductVendorService.GetAllProductVendor(false);
                vendor.ShouldNotBeNull();
                vendor.Result.Count().ShouldBe(461);
            }
        }*/

        /*[Fact]
        public void TestEditProductVendorService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                repositoryManager = new PurchasingRepositoryManager(context);
                IPurchasingServiceManager serviceManager = new PurchasingServiceManager(repositoryManager, mapper);
                var productVendorDto = new ProductVendorDto
                {
                    ProductId = 316,
                    BusinessEntityId = 3,
                    AverageLeadTime = 18,
                    StandardPrice = 61,
                    LastReceiptCost = 71,
                    LastReceiptDate = new DateTime(2022, 4, 18),
                    MinOrderQty = 2,
                    MaxOrderQty = 8,
                    OnOrderQty = 5,
                    UnitMeasureCode = "CTN",
                    ModifiedDate = DateTime.Now

                };
                serviceManager.ProductVendorService.Edit(productVendorDto);
            }
        }*/

        /*[Fact]
        public void TestRemoveProductVendorService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                repositoryManager = new PurchasingRepositoryManager(context);
                IPurchasingServiceManager serviceManager = new PurchasingServiceManager(repositoryManager, mapper);
                var productVendorDto = new ProductVendorDto
                {
                    ProductId = 316,
                    BusinessEntityId = 3

                };
                serviceManager.ProductVendorService.Remove(productVendorDto);
                //assert
                var vendor = serviceManager.ProductVendorService.GetAllProductVendor(false);
                vendor.ShouldNotBeNull();
                vendor.Result.Count().ShouldBe(460);
            }
        }*/

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
