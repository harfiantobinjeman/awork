using AutoMapper;
using AWork.Contracts.Dto.Production;
using AWork.Domain.Base;
using AWork.Persistence;
using AWork.Persistence.Base;
using AWork.Services;
using AWork.Services.Abstraction;
using AWork.Test.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AWork.Test.Production
{
    public class AWorkOktarianIntegration
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionsBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IProductionRepositoryManager _repositoryManager;

        public AWorkOktarianIntegration()
        {
            BuildConfiguration();
            SetupOptions();
        }
        private void BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();

        }
        /*
                [Fact]
                public void TestGetProductService()
                {
                    using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
                    {
                        _repositoryManager = new ProductionRepositoryManager(context);
                        IProductionServiceManager servicesManager = new ProductionServiceManager(_repositoryManager, mapper);

                        var product = servicesManager.ProductService.GetAllProduct(false);

                        //Assert
                        product.ShouldNotBeNull();
                        product.Result.Count().ShouldBe(507);

                    }
                }*/

        /* [Fact]
         public void TestGetCreateProductService()
         {
             using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
             {
                 _repositoryManager = new ProductionRepositoryManager(context);
                 IProductionServiceManager serviceManager = new ProductionServiceManager(_repositoryManager, mapper);

                 var productDto = new ProductForCreateDto
                 {

                     Name = "Toyono",
                     ProductNumber = "AR - 4327",
                     MakeFlag = true,
                     FinishedGoodsFlag = true,
                     SafetyStockLevel = 69,
                     ReorderPoint = 69,
                     StandardCost = 66_666,
                     ListPrice = 66_666,
                     DaysToManufacture = 1,
                     SellStartDate = DateTime.Now,
                     ModifiedDate = DateTime.Now


                 };

                 serviceManager.ProductService.Insert(productDto);


                 *//*var product = serviceManager.ProductService.GetAllProduct(false);
                 //assert
                 product.ShouldNotBeNull();
                 product.Result.Count().ShouldBe(507);*//*
             }
         }
 */
        /* [Fact]
         public void TestEditProductService()
         {
             using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
             {
                 _repositoryManager = new ProductionRepositoryManager(context);
                 IProductionServiceManager serviceManager = new ProductionServiceManager(_repositoryManager, mapper);

                 var productDto = new ProductDto
                 {
                     ProductId = 1015,
                     Name = "Id 1015 sudah diedit", 
                     ProductNumber = "666",
                     MakeFlag = true,
                     FinishedGoodsFlag = true,
                     SafetyStockLevel = 43,
                     ReorderPoint = 43,
                     StandardCost = 88_776,
                     ListPrice = 99_432,
                     DaysToManufacture = 1,
                     SellStartDate = DateTime.Now,
                     Rowguid = Guid.NewGuid(),
                     ModifiedDate = DateTime.Now

                 };
                 serviceManager.ProductService.Edit(productDto);
                *//* var product = serviceManager.ProductService.GetAllProduct(false);
                 product.ShouldNotBeNull();
                 product.Result.Count().ShouldBe(506);*//*
             }
         }*/

        [Fact]
        public void TestGetDeleteProductService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _repositoryManager = new ProductionRepositoryManager(context);
                IProductionServiceManager serviceManager = new ProductionServiceManager(_repositoryManager, mapper);

                var productDto = new ProductDto
                {
                    ProductId = 1015
                };

                serviceManager.ProductService.Remove(productDto);


               /* var product = serviceManager.ProductService.GetAllProduct(false);
                //assert
                product.ShouldNotBeNull();
                product.Result.Count().ShouldBe(507);*/
            }
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
