using AutoMapper;
using AWork.Contracts.Dto.Sales.SpecialOffer;
using AWork.Domain.Base;
using AWork.Persistence;
using AWork.Persistence.Base;
using AWork.Services;
using AWork.Services.Abstraction;
using AWork.Test;
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

namespace AWork.Testing.Sales
{

    public class TestSpecialOfferRini
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static ISalesRepositoryManager _repositoryManager;

        public TestSpecialOfferRini()
        {
            BuildConfiguration();
            SetupOptions();
        }

        [Fact]
        public void TestGetSpecialOfferService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager salesServiceManager = new SalesServiceManager(_repositoryManager, mapper);
                var specialOffer = salesServiceManager.SpecialOfferService.GetAllSpecialOffer(false);
                specialOffer.ShouldNotBeNull();
                specialOffer.Result.Count().ShouldBe(34);
            }
        }

        [Fact]
        public void TestCreateSpecialOfferService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager salesServiceManager = new SalesServiceManager(_repositoryManager, mapper);
                var specialOfferDto = new SpecialOfferForCreateDto
                {
                    
                    Description = "Discounts",
                    Category = "Customer",
                    Type = "New Product",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(30),
                    MinQty = 2,
                    MaxQty = 4,
                    Rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now
                };
                salesServiceManager.SpecialOfferService.Insert(specialOfferDto);

                var specialOffer = salesServiceManager.SpecialOfferService.GetAllSpecialOffer(false);
                specialOffer.ShouldNotBeNull();
                specialOffer.Result.Count().ShouldBe(36);
            }
        }

        [Fact]
        public void TestEditSpecialOfferService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                // act
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager salesServiceManager = new SalesServiceManager(_repositoryManager, mapper);
                var specialOfferDto = new SpecialOfferDto
                {
                    SpecialOfferId = 40,
                    Description = "Jasmin Hijab sale 20%",
                    DiscountPct = 5,
                    Type = "New Product",
                    Category = "Reseller",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(30),
                    MinQty = 1,
                    MaxQty = 3,
                    Rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now
                };
                salesServiceManager.SpecialOfferService.Edit(specialOfferDto);

                //assert
                var specialOffer = salesServiceManager.SpecialOfferService.GetAllSpecialOffer(false);
                specialOffer.ShouldNotBeNull();
                specialOffer.Result.Count().ShouldBe(36);
            }
        }
        
        [Fact]
        public void TestDeleteSpecialOfferService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                // act
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager salesServiceManager = new SalesServiceManager(_repositoryManager, mapper);

                var specialOfferDto = new SpecialOfferDto
                {
                    SpecialOfferId = 41
                };
                salesServiceManager.SpecialOfferService.Remove(specialOfferDto);

                //assert
                var specialOffer = salesServiceManager.SpecialOfferService.GetAllSpecialOffer(false);
                specialOffer.ShouldNotBeNull();
                specialOffer.Result.Count().ShouldBe(35);
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

