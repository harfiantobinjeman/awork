using AutoMapper;
using AWork.Contracts.Dto.Sales.SalesPerson;
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
using AWork.Test.Mapping;

namespace AWork.Test.Sales
{
    public class TestSalesPersonDaniel
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static ISalesRepositoryManager _repositoryManager;

        public TestSalesPersonDaniel()
        {
            BuildConfiguration();
            SetupOptions();
        }

        [Fact]
        public void TestCreateSalesPersonService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                // act
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager serviceManager = new SalesServiceManager(_repositoryManager, mapper);

                var salesPersonDto = new SalesPersonForCreateDto
                { 
                    TerritoryId = 1,
                    SalesQuota = 1,
                    SalesYtd = 1,
                    SalesLastYear = 1,
                    Bonus = 1,
                    CommissionPct = 1
                };
                serviceManager.SalesPersonService.Insert(salesPersonDto);

                //assert
                var salesPerson = serviceManager.SalesPersonService.GetAllSalesPerson(false);
                salesPerson.ShouldNotBeNull();
                salesPerson.Result.Count().ShouldBe(18);
            }
        }
        [Fact]
        public void TestDeleteSalesPersonService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                // act
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager serviceManager = new SalesServiceManager(_repositoryManager, mapper);

                var salesPersonDto = new SalesPersonDto
                {
                    BusinessEntityId = 20778
                };
                serviceManager.SalesPersonService.Remove(salesPersonDto);

                //assert
                var salesPerson = serviceManager.SalesPersonService.GetAllSalesPerson(false);
                salesPerson.ShouldNotBeNull();
                salesPerson.Result.Count().ShouldBe(17);
            }
        }

        [Fact]
        public void TestGetSalesPersonService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager serviceManager = new SalesServiceManager(_repositoryManager, mapper);
                var salesPerson = serviceManager.SalesPersonService.GetAllSalesPerson(false);
                salesPerson.ShouldNotBeNull();
                salesPerson.Result.Count().ShouldBe(17);
            }
        }


        [Fact]
        public void TestEditSalesPersonService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                // act
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager serviceManager = new SalesServiceManager(_repositoryManager, mapper);

                var salesPersonDto = new SalesPersonDto
                {
                    BusinessEntityId = 20778,
                    TerritoryId = 3,
                    Rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now
                };
                serviceManager.SalesPersonService.Edit(salesPersonDto);

                //assert
                var salesPerson = serviceManager.SalesPersonService.GetAllSalesPerson(false);
                salesPerson.ShouldNotBeNull();
                salesPerson.Result.Count().ShouldBe(18);
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
