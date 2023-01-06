using AutoMapper;
using AWork.Contracts.Dto.Production;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Persistence;
using AWork.Persistence.Base;
using AWork.Persistence.Repositories.Production;
using AWork.Services;
using AWork.Services.Abstraction;
using AWork.Services.Production;
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
    public class AWorkHarfiantoIntegrationTest
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionsBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IProductionRepositoryManager _repositoryManager;

        public AWorkHarfiantoIntegrationTest()
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
        public void TestGetWorkOrderService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _repositoryManager = new ProductionRepositoryManager(context);
                IProductionServiceManager servicesManager = new ProductionServiceManager(_repositoryManager, mapper);

                var workOrder = servicesManager.WorkOrderService.GetAllWorkOrder(false);

                //Assert
                workOrder.ShouldNotBeNull();
                workOrder.Result.Count().ShouldBe(72591);

            }
        }

        [Fact]
        public void TestCreateLocation()
         {
             using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
             {
                //IProductionServiceManager productionServiceManager = new ProductionServiceManager(_repositoryManager, mapper);
                _repositoryManager = new ProductionRepositoryManager(context);
                 IProductionServiceManager productionServiceManager = new ProductionServiceManager(_repositoryManager, mapper);
                //var location = productionServiceManager.LocationService.CreateLocation();

                 var locationForCreateDto = new LocationForCreateDto
                 {
                     Name = "akuntant",
                     CostRate = 2,
                     Availability = 98,
                     ModifiedDate = DateTime.Now
                 };
                 productionServiceManager.LocationService.Insert(locationForCreateDto);
                /*var workOrder = productionServiceManager.LocationService.GetAllLocation(false);

                //assert
                workOrder.ShouldNotBeNull();
                workOrder.Result.Count().ShouldBe(16);*/
             }
         }

        [Fact]
        public void TestCreateOrderWork()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                //IProductionServiceManager productionServiceManager = new ProductionServiceManager(_repositoryManager, mapper);
                _repositoryManager = new ProductionRepositoryManager(context);
                IProductionServiceManager productionServiceManager = new ProductionServiceManager(_repositoryManager, mapper);

                var workOrderForCreateDto = new WorkOrderForCreateDto
                {
                    ProductId = 722,
                    OrderQty =73,
                    StockedQty=73,
                    ScrappedQty =73,
                    StartDate =DateTime.Now,
                    DueDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                productionServiceManager.WorkOrderService.Insert(workOrderForCreateDto);
                /*var workOrder = productionServiceManager.LocationService.GetAllLocation(false);

                //assert
                workOrder.ShouldNotBeNull();
                workOrder.Result.Count().ShouldBe(17);*/
            }
        }

        [Fact]
        public void TesCreateWorkOrderRouting()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                //IProductionServiceManager productionServiceManager = new ProductionServiceManager(_repositoryManager, mapper);
                _repositoryManager = new ProductionRepositoryManager(context);
                IProductionServiceManager productionServiceManager = new ProductionServiceManager(_repositoryManager, mapper);
                var location = productionServiceManager.LocationService.CreateLocation();
                var workOrder = productionServiceManager.WorkOrderService.CreateWorkOrder();

                var workOrderRoutingDto = new WorkOrderRoutingForCreateDto
                {
                    LocationId = location.LocationId,
                    WorkOrderId = workOrder.WorkOrderId,
                    ProductId = 747,
                    OperationSequence = 6,
                    ScheduledEndDate =DateTime.Now,
                    ScheduledStartDate = DateTime.Now,
                    PlannedCost = 99,
                    ModifiedDate= DateTime.Now
                };
                productionServiceManager.WorkOrderRoutingService.Insert(workOrderRoutingDto);
                var workOrderRouting = productionServiceManager.WorkOrderService.CreateWorkOrder();
                workOrderRouting.ShouldNotBeNull();
                workOrderRouting.WorkOrderId.ShouldBe(67132);
                /*
                //productionServiceManager.LocationService.Insert(location);
                var locationOrder = productionServiceManager.LocationService.CreateLocation();
                locationOrder.ShouldBeNull();
                locationOrder.LocationId.ShouldBe((short)20);

                //assert
                locationOrder.ShouldNotBeNull();
                locationOrder.Result.Count().ShouldBe(19);*/
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

