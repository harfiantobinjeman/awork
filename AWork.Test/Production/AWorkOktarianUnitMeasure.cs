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
    public class AWorkOktarianUnitMeasure
    {

        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionsBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IProductionRepositoryManager _repositoryManager;

        public AWorkOktarianUnitMeasure()
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

        /*[Fact]
        public void TestGetUnitMeasureService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _repositoryManager = new ProductionRepositoryManager(context);
                IProductionServiceManager servicesManager = new ProductionServiceManager(_repositoryManager, mapper);

                var unitMeasure = servicesManager.UnitMeasureservice.GetAllUnitMeasure(false);

                //Assert
                unitMeasure.ShouldNotBeNull();
                unitMeasure.Result.Count().ShouldBe(40);
            }
        }*/

        /*  [Fact]
          public void TestGetCreateUnitMeasureService()
          {
              using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
              {
                  _repositoryManager = new ProductionRepositoryManager(context);
                  IProductionServiceManager serviceManager = new ProductionServiceManager(_repositoryManager, mapper);

                  var unitMeasureDto = new UnitMeasureForCreateDto
                  {
                      UnitMeasureCode = "PLG",
                      Name = "Palembang",
                      ModifiedDate = DateTime.Now


                  };

                  serviceManager.UnitMeasureservice.Insert(unitMeasureDto);

                 *//* var unitMeasure = serviceManager.UnitMeasureservice.GetAllUnitMeasure(false);
                  //assert
                  unitMeasure.ShouldNotBeNull();
                  unitMeasure.Result.Count().ShouldBe(39);*//*
              }
          }*/

        [Fact]
        public void TestEditProductService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _repositoryManager = new ProductionRepositoryManager(context);
                IProductionServiceManager serviceManager = new ProductionServiceManager(_repositoryManager, mapper);

                var unitMeasureDto = new UnitMeasureDto
                {
                    UnitMeasureCode = "PLG",
                    Name = "Palembang Bari",
                    ModifiedDate = DateTime.Now


                };

                serviceManager.UnitMeasureservice.Edit(unitMeasureDto);
                var unitMeasure = serviceManager.UnitMeasureservice.GetAllUnitMeasure(false);
                unitMeasure.ShouldNotBeNull();
                unitMeasure.Result.Count().ShouldBe(40);
            }
        }

       /* [Fact]
        public void TestGetDeleteUnitMeasureService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _repositoryManager = new ProductionRepositoryManager(context);
                IProductionServiceManager serviceManager = new ProductionServiceManager(_repositoryManager, mapper);

                var unitMeasure = new UnitMeasureDto
                {
                    UnitMeasureCode = "PLG",
                    Name = "Palembang"

                };

                serviceManager.UnitMeasureservice.Remove(unitMeasure);


               *//* var product = serviceManager.ProductService.GetAllProduct(false);
                //assert
                product.ShouldNotBeNull();
                product.Result.Count().ShouldBe(40);*//*
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
