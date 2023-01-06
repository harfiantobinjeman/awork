using AutoMapper;
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
    public class TestCustomerFaiz
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static ISalesRepositoryManager _repositoryManager;

        public TestCustomerFaiz()
        {
            BuildConfiguration();
            SetupOptions();
        }

        [Fact]
        public void TestGetCustomerService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager serviceManager = new SalesServiceManager(_repositoryManager, mapper);
                var custumer = serviceManager.CustomerService.GetAllCustomer(false);
                custumer.ShouldNotBeNull();
                custumer.Result.Count().ShouldBe(19820);
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
