using AutoMapper;
using AWork.Domain.Base;
using AWork.Persistence;
using AWork.Persistence.Base;
using AWork.Services.Abstraction;
using AWork.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using AWork.Contracts.Dto.Sales.SalesTerritory;
using AWork.Test.Mapping;

namespace AWork.Test.Sales
{
    public class TestSalesTerritoryDaniel
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static ISalesRepositoryManager _repositoryManager;

        public TestSalesTerritoryDaniel()
        {
            BuildConfiguration();
            SetupOptions();
        }

        [Fact]
        public void TestGetSalesTerritoryService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager serviceManager = new SalesServiceManager(_repositoryManager, mapper);
                var salesTerritory = serviceManager.SalesTerritoryService.GetAllSalesTerritory(false);
                salesTerritory.ShouldNotBeNull();
                salesTerritory.Result.Count().ShouldBe(12);
            }
        }

        [Fact]
        public void TestCreateSalesTerritoryService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                // act
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager serviceManager = new SalesServiceManager(_repositoryManager, mapper);

                var salesTerritoryDto = new SalesTerritoryForCreateDto
                {
                    Name = "Testing Create",
                    CountryRegionCode = "ID",
                    Group = "Asia"
                };
                serviceManager.SalesTerritoryService.Insert(salesTerritoryDto);

                //assert
                var salesTerritory = serviceManager.SalesTerritoryService.GetAllSalesTerritory(false);
                salesTerritory.ShouldNotBeNull();
                salesTerritory.Result.Count().ShouldBe(13);
            }
        }

        [Fact]
        public void TestEditSalesTerritoryService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                // act
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager serviceManager = new SalesServiceManager(_repositoryManager, mapper);
                var salesTerritoryDto = new SalesTerritoryDto
                {
                    TerritoryId = 26,
                    Name = "Malaysia",
                    CountryRegionCode = "MY",
                    Group = "South-East Asia",
                    SalesYtd = 1,
                    SalesLastYear = 1,
                    CostYtd = 1,
                    CostLastYear = 1,
                    Rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now,

                };
                serviceManager.SalesTerritoryService.Edit(salesTerritoryDto);

                //assert
                var salesTerritory = serviceManager.SalesTerritoryService.GetAllSalesTerritory(false);
                salesTerritory.ShouldNotBeNull();
                salesTerritory.Result.Count().ShouldBe(13);
            }
        }

        [Fact]
        public void TestDeleteSalesTerritoryService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                // act
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager serviceManager = new SalesServiceManager(_repositoryManager, mapper);

                var salesTerritoryDto = new SalesTerritoryDto
                {
                    TerritoryId = 26
                };
                serviceManager.SalesTerritoryService.Remove(salesTerritoryDto);

                //assert
                var salesTerritory = serviceManager.SalesTerritoryService.GetAllSalesTerritory(false);
                salesTerritory.ShouldNotBeNull();
                salesTerritory.Result.Count().ShouldBe(12);
            }
        }

        private void BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }

        private void SetupOptions()
        {
            optionBuilder = new DbContextOptionsBuilder<AdventureWorks2019Context>();
            optionBuilder.UseSqlServer(Configuration.GetConnectionString("AdventureWorks2019Db"));

            var service = new ServiceCollection();
            service.AddAutoMapper(typeof(MappingProfile));
            serviceProvider = service.BuildServiceProvider();

            mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            mapperConfig.AssertConfigurationIsValid();
            mapper = mapperConfig.CreateMapper();
        }
    }
}
