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
    public class AWorkIwanProductPhotoTest
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionsBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IProductionRepositoryManager _repositoryManager;


        public AWorkIwanProductPhotoTest()
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

        [Fact]
        public void TestGetProductPhotoService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _repositoryManager = new ProductionRepositoryManager(context);
                IProductionServiceManager servicesManager = new ProductionServiceManager(_repositoryManager, mapper);

                var productPhoto = servicesManager.ProductPhotoService.GetAllProductPhoto(false);

                //Assert
                productPhoto.ShouldNotBeNull();
                productPhoto.Result.Count().ShouldBe(110);

            }
        }
        /*[Fact]
        public void TestCreateProductPhotoService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {

                _repositoryManager = new ProductionRepositoryManager(context);
                IProductionServiceManager servicesManager = new ProductionServiceManager(_repositoryManager, mapper);
                var photoDto = new ProductPhotoDto
                {

                    ModifiedDate = DateTime.Now,
                    LargePhotoFileName = "Honda Vario"

                };
                servicesManager.ProductPhotoService.Insert(photoDto);

                //Assert
                var productPhoto = servicesManager.ProductPhotoService.GetAllProductPhoto(false);
                //assert
                productPhoto.ShouldNotBeNull();
                productPhoto.Result.Count().ShouldBe(104);

            }
        }*/

        [Fact]
        public void TestEditProductPhotoService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _repositoryManager = new ProductionRepositoryManager(context);
                IProductionServiceManager servicesManager = new ProductionServiceManager(_repositoryManager, mapper);
                var productPhotoDto = new ProductPhotoDto
                {
                    ModifiedDate = DateTime.Now,
                    ProductPhotoId = 1198,
                    ThumbnailPhotoFileName = "Mobil",
                    LargePhotoFileName = "Daihatsu Xenia",
                };
                servicesManager.ProductPhotoService.Edit(productPhotoDto);
                var productPhoto = servicesManager.ProductPhotoService.GetAllProductPhoto(false);
                productPhoto.ShouldNotBeNull();
                productPhoto.Result.Count().ShouldBe(121);
            }
        }

        [Fact]
        public void TestRemoveProductPhotoService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _repositoryManager = new ProductionRepositoryManager(context);
                IProductionServiceManager servicesManager = new ProductionServiceManager(_repositoryManager, mapper);

                var productPhotoDto = new ProductPhotoDto
                {
                    ProductPhotoId = 1205,
                    ThumbnailPhotoFileName = null,
                    LargePhotoFileName = null,
                };
                servicesManager.ProductPhotoService.Remove(productPhotoDto);
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