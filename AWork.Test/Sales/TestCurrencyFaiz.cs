using AutoMapper;
using AWork.Contracts.Dto.Sales.Currency;
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
    public class TestCurrencyFaiz
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static ISalesRepositoryManager _repositoryManager;

        public TestCurrencyFaiz()
        {
            BuildConfiguration();
            SetupOptions();
        }

        [Fact]
        public void TestGetCurrencyService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager salesServiceManager = new SalesServiceManager(_repositoryManager, mapper);
                var currency = salesServiceManager.CurrencyService.GetAllCurrency(false);
                currency.ShouldNotBeNull();
                currency.Result.Count().ShouldBe(105);
            }
        }

        [Fact]
        public void TestCreateCurrencyService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager salesServiceManager = new SalesServiceManager(_repositoryManager, mapper);
                var currencyDto = new CurrencyForCreateDto
                {
                    CurrencyCode ="OPB",
                    Name = "Berry"
                };
                salesServiceManager.CurrencyService.Insert(currencyDto);

                var currency = salesServiceManager.CurrencyService.GetAllCurrency(false);
                currency.ShouldNotBeNull();
                currency.Result.Count().ShouldBe(106);
            }
        }

        public void TestEditCurrencyService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                // act
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager salesServiceManager = new SalesServiceManager(_repositoryManager, mapper);
                var currencyDto = new CurrencyDto
                {
                    CurrencyCode = "OPB",
                    Name= "Mata Uang One Piece"
                };
                salesServiceManager.CurrencyService.Edit(currencyDto);

                //assert
                var currency = salesServiceManager.CurrencyService.GetAllCurrency(false);
                currency.ShouldNotBeNull();
                currency.Result.Count().ShouldBe(106);
            }
        }

        [Fact]
        public void TestDeleteCurrencyService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                // act
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager serviceManager = new SalesServiceManager(_repositoryManager, mapper);

                var currencyDto = new CurrencyDto
                {
                    CurrencyCode = "OPB"
                };
                serviceManager.CurrencyService.Remove(currencyDto);

                //assert
                var currency = serviceManager.CurrencyService.GetAllCurrency(false);
                currency.ShouldNotBeNull();
                currency.Result.Count().ShouldBe(105);
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
