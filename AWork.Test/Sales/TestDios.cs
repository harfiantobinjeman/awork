using AutoMapper;
using AWork.Contracts.Dto.Sales.CreditCard;
using AWork.Contracts.Dto.Sales.PersonCreditCard;
using AWork.Contracts.Dto.Sales.SalesPerson;
using AWork.Contracts.Dto.Sales.SalesTerritory;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Persistence;
using AWork.Persistence.Base;
using AWork.Services;
using AWork.Services.Abstraction;
using AWork.Test.Mapping;
using EmptyFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Northwind.Contracts.Dto.Category;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace AWork.Test.Sales
{
    public class TestDios
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static ISalesRepositoryManager _repositoryManager;

        public TestDios()
        {
            BuildConfiguration();
            SetupOptions();
        }
        [Fact]
        public void TestGetPersonCreditCardService()
        {

            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager serviceManager = new SalesServiceManager(_repositoryManager, mapper);
                var personCreditCard = serviceManager.PersonCreditCardService.GetAllPersonCreditCards(false);
                personCreditCard.ShouldNotBeNull();
                personCreditCard.Result.Count().ShouldBe(19118);
            }
        }

        [Fact]
        public void TestCreatePersonCreditCardService()

        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                // act
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager serviceManager = new SalesServiceManager(_repositoryManager, mapper);

                var personCreditCardDto = new PersonCreditCardForCreateDto
                {

                    BusinessEntityId = 20779,
                    CreditCardId = 16367,
                    ModifiedDate = DateTime.Now
                };
                serviceManager.PersonCreditCardService.Insert(personCreditCardDto);

                //assert
                /*var personCreditCardDtos = serviceManager.PersonCreditCardService.GetAllPersonCreditCards(false);
                personCreditCardDtos.ShouldNotBeNull();
                personCreditCardDtos.Result.Count().ShouldBe(19119);*/
            }
        }

        [Fact]
        public void TestDeletePersonCreditCardService()

        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                // act
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager serviceManager = new SalesServiceManager(_repositoryManager, mapper);

                var personCreditCardDto = new PersonCreditCardDto
                {

                    BusinessEntityId = 20779,
                    CreditCardId = 16367
                };
                serviceManager.PersonCreditCardService.Remove(personCreditCardDto);

                //assert
                /*var personCreditCardDtos = serviceManager.PersonCreditCardService.GetAllPersonCreditCards(false);
                personCreditCardDtos.ShouldNotBeNull();
                personCreditCardDtos.Result.Count().ShouldBe(19118);*/
            }
        }
        [Fact]

        public void TestSalesOrderHeaderService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager serviceManager = new SalesServiceManager(_repositoryManager, mapper);
                var salesOrderHeader = serviceManager.SalesOrderHeaderService.GetAllSalesOrderHeader(false);
                salesOrderHeader.ShouldNotBeNull();
                salesOrderHeader.Result.Count().ShouldBe(31465);
            }
        }


        //CreditCard
        [Fact]

        public void TestGetCreditCardService()

        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager serviceManager = new SalesServiceManager(_repositoryManager, mapper);
                var creditCard = serviceManager.CreditCardService.GetAllCreditCard(false);
                creditCard.ShouldNotBeNull();
                creditCard.Result.Count().ShouldBe(19118);
            }
        }
        [Fact]
        public void TestCreateCreditCardService()

        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                // act
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager serviceManager = new SalesServiceManager(_repositoryManager, mapper);

                var creditCardDto = new CreditCardForCreateDto
                {

                    CardType = "DiosTesting",
                    CardNumber = "1999"
                };
                serviceManager.CreditCardService.Insert(creditCardDto);

                //assert
                var creditCardDtos = serviceManager.CreditCardService.GetAllCreditCard(false);
                creditCardDtos.ShouldNotBeNull();
                creditCardDtos.Result.Count().ShouldBe(19119);
            }
        }

        [Fact]
        public void TestDeleteCreditCardService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                // act
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager serviceManager = new SalesServiceManager(_repositoryManager, mapper);

                var creditCardDto = new CreditCardDto
                {
                    CreditCardId = 19248

                };
                serviceManager.CreditCardService.Remove(creditCardDto);

                //assert
                var creditCard = serviceManager.CreditCardService.GetAllCreditCard(false);
                creditCard.ShouldNotBeNull();
                creditCard.Result.Count().ShouldBe(19118);
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

