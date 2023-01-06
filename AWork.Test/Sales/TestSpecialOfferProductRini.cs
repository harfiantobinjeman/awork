using AutoMapper;
using AWork.Contracts.Dto.Sales.SpecialOffer;
using AWork.Contracts.Dto.Sales.SpecialOfferProduct;
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
    public class TestSpecialOfferProductRini
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static ISalesRepositoryManager _repositoryManager;

        public TestSpecialOfferProductRini()
        {
            BuildConfiguration();
            SetupOptions();
        }

        [Fact]
        public void TestGetSpecialOfferProductService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager salesServiceManager = new SalesServiceManager
                    (_repositoryManager, mapper);
                var specialOfferProduct = salesServiceManager
                    .SpecialOfferProductService
                    .GetAllSpecialOfferProduct(false);
                specialOfferProduct.ShouldNotBeNull();
                specialOfferProduct.Result.Count().ShouldBe(546);
            }
        }

        [Fact]
        public async void TestCreateSpecialOfferAndProductService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager salesServiceManager = new SalesServiceManager
                    (_repositoryManager, mapper);
                var specialOfferForCreateDto = new SpecialOfferForCreateDto
                {

                    Description = "Discounts",
                    Category = "Reseller",
                    Type = "Discontinued Product",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(30),
                    MinQty = 2,
                    MaxQty = 7,
                    Rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now
                };

                var specialOfferDto = salesServiceManager.SpecialOfferService
                    .CreateSpecialOfferProduct(specialOfferForCreateDto);

                var specialOfferProductForCreateDto = new SpecialOfferProductForCreateDto
                {
                    SpecialOfferId = specialOfferDto.SpecialOfferId,
                    ProductId = 680
                };

                salesServiceManager.SpecialOfferProductService.Insert(specialOfferProductForCreateDto);

                var salesOfferDetail = await salesServiceManager.SpecialOfferProductService
                        .GetSpecialOfferProductById(specialOfferDto.SpecialOfferId, false);

                specialOfferDto.SpecialOfferId.ShouldBeEquivalentTo
                    (specialOfferDto.SpecialOfferId);
                salesOfferDetail.SpecialOfferId.ShouldBeEquivalentTo
                    ((int)specialOfferDto.SpecialOfferId);
            }
        }

        [Fact]
        public void TestEditSpecialOfferAndProductService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                //act
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager serviceManager = new SalesServiceManager
                    (_repositoryManager, mapper);

                var specialOfferProductDto = new SpecialOfferProductDto
                {
                    SpecialOfferId = 38,
                    ProductId = 680,
                    Rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now.AddDays(2)
                };
                serviceManager.SpecialOfferProductService.Edit(specialOfferProductDto);

                //assert
                var specialOfferProduct = serviceManager.SpecialOfferProductService
                    .GetAllSpecialOfferProduct(false);
                specialOfferProduct.ShouldNotBeNull();
                specialOfferProduct.Result.Count().ShouldBe(547);
            }
        }

        [Fact]
        public void TestDeleteSpecialOfferProductService()
        {
            using (var context = new AdventureWorks2019Context(optionBuilder.Options))
            {
                _repositoryManager = new SalesRepositoryManager(context);
                ISalesServiceManager serviceManager = new SalesServiceManager
                    (_repositoryManager, mapper);

                var specialOfferProductDto = new SpecialOfferProductDto
                {
                    SpecialOfferId = 42,
                    ProductId = 680
                };
                serviceManager.SpecialOfferProductService.Remove(specialOfferProductDto);

                //assert
                var specialOfferProduct = serviceManager.SpecialOfferProductService
                    .GetAllSpecialOfferProduct(false);
                specialOfferProduct.ShouldNotBeNull();
                specialOfferProduct.Result.Count().ShouldBe(546);
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
