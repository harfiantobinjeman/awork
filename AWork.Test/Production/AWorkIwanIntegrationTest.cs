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
    public class AWorkIwanIntegrationTest
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionsBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IProductionRepositoryManager _repositoryManager;

        public AWorkIwanIntegrationTest()
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
        public void TestGetProductProductPhotoService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _repositoryManager = new ProductionRepositoryManager(context);
                IProductionServiceManager servicesManager = new ProductionServiceManager(_repositoryManager, mapper);

                var productPhoto = servicesManager.ProductProductPhotoService.GetAllProductProductPhoto(false);

                //Assert
                productPhoto.ShouldNotBeNull();
                productPhoto.Result.Count().ShouldBe(509);

            }
        }

        [Fact]
        public void TestCreateProductProductPhotoService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {

                _repositoryManager = new ProductionRepositoryManager(context);
                IProductionServiceManager servicesManager = new ProductionServiceManager(_repositoryManager, mapper);


                /*var Product = servicesManager.ProductService.CreateProductId();
                var ProductPhotoId = servicesManager.ProductPhotoService.CreateProductPhotoId();*/
                var photoDto = new ProductProductPhotoForCreateDto
                {
                    ProductId = 1000,
                    ProductPhotoId = 1,
                    Primary = true
                    
                };
                servicesManager.ProductProductPhotoService.Insert(photoDto);

                //Assert
                /*var productPhoto = servicesManager.ProductProductPhotoService.GetAllProductProductPhoto(false);
                //assert
                productPhoto.ShouldNotBeNull();
                productPhoto.Result.Count().ShouldBe(508);
*/
            }
        }

        [Fact]
        public void TestEditProductProductPhotoService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _repositoryManager = new ProductionRepositoryManager(context);
                IProductionServiceManager servicesManager = new ProductionServiceManager(_repositoryManager, mapper);
                var productPhotoDto = new ProductProductPhotoDto
                {
                    ProductId = 1000,
                    ProductPhotoId = 80,
                    Primary = false,
                    ModifiedDate = DateTime.Now,
                };
                servicesManager.ProductProductPhotoService.Edit(productPhotoDto);
                var productPhoto = servicesManager.ProductProductPhotoService.GetAllProductProductPhoto(false);
                productPhoto.ShouldNotBeNull();
                productPhoto.Result.Count().ShouldBe(510);
            }
        }


        [Fact]
        public void TestRemoveProductProductPhotoService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _repositoryManager = new ProductionRepositoryManager(context);
                IProductionServiceManager servicesManager = new ProductionServiceManager(_repositoryManager, mapper);

                var productPhotoDto = new ProductProductPhotoDto
                {
                    ProductId = 1000,
                    ProductPhotoId = 1195,
                    Primary = true

                };
                servicesManager.ProductProductPhotoService.Remove(productPhotoDto);
            }
        }
        /*        [Fact]
                public void TestCreateProductProductPhotoService()
                {
                    using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
                    {

                        _repositoryManager = new ProductionRepositoryManager(context);
                        IProductionServiceManager servicesManager = new ProductionServiceManager(_repositoryManager, mapper);

                        var ProductPhoto = servicesManager.ProductPhotoService.CreateProductPhotoId();
                        var Product = servicesManager.ProductService.CreateProductId();
                        var productProductPhotoDto = new ProductProductPhotoForCreateDto
                        {
                            Primary = true



                        };
                        servicesManager.ProductProductPhotoService.Insert(productProductPhotoDto);

                        //Assert
                        var product2Photo = servicesManager.ProductProductPhotoService.GetAllProductProductPhoto(false);
                        //assert
                        product2Photo.ShouldNotBeNull();
                        product2Photo.Result.Count().ShouldBe(505);

                    }
                }*/

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