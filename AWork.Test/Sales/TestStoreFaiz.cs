using AutoMapper;
using AWork.Contracts.Dto.Sales.Store;
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

namespace AWork.Test.Sales
{
    public class TestStoreFaiz
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static ISalesRepositoryManager _repositoryManager;

        public TestStoreFaiz()
        {
            BuildConfiguration();
            SetupOptions();
        }
        [Fact]
        public void TestGetStoreService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager serviceManager = new SalesServiceManager(_repositoryManager, mapper);
                var store = serviceManager.StoreService.GetAllAsync(false);
                store.ShouldNotBeNull();
                store.Result.Count().ShouldBe(701);
            }
        }
        
        /*public void TestCreateStoreService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                // act
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager serviceManager = new SalesServiceManager(_repositoryManager, mapper);

                var storeDto = new StoreForCreateDto
                {
                    Name = "Toko Visual Studio",
                    SalesPersonId = 999,
                    Demographics = "6 derajat lintang utara"
                };
                serviceManager.StoreService.Insert(storeDto);

                //assert
                var store = serviceManager.StoreService.GetAllAsync(false);
                store.ShouldNotBeNull();
                store.Result.Count().ShouldBe(702);
            }
        }*/

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
