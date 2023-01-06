using AutoMapper;
using AWork.Contracts.Dto.HumanResources;
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

namespace Awork.Test.Widi
{
    public class Widi_EmployeeDepartmentHistoryServiceTest
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionsBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IHRRepositoryManager _repositoryManager;

        public Widi_EmployeeDepartmentHistoryServiceTest()
        {
            BuildConfiguration();
            SetupOptions();
        }


        [Fact]
        public void TestGetEmployeeDepartmentHistoryService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _repositoryManager = new HRRepositoryManager(context);
                IHumanResourceServiceManager servicesManager = new HumanResourceServiceManager(_repositoryManager, mapper);
                var employeeDepartmentHistory = servicesManager.EmployeeDepartmentHistoryService.GetAllEmployeeDepartmentHistory(false);

                //Assert
                employeeDepartmentHistory.ShouldNotBeNull();
                employeeDepartmentHistory.Result.Count().ShouldBe(298);

            }
        }


        [Fact]
        public void TestCreateEmployeeDepartmentHistoryService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _repositoryManager = new HRRepositoryManager(context);
                IHumanResourceServiceManager servicesManager = new HumanResourceServiceManager(_repositoryManager, mapper);

                var employeeHistory = new EmployeeDepartmentHistoryForCreateDto
                {

                    BusinessEntityId = 20782,
                    DepartmentId = 5,
                    ShiftId = 3,
                    StartDate = new DateTime(2022, 09, 23),
                    EndDate = DateTime.Now,
                    ModifiedDate = DateTime.Now

                };

                servicesManager.EmployeeDepartmentHistoryService.Insert(employeeHistory);
                _repositoryManager.Save();

            }
        }

        [Fact]
        public async void TestEditEmployeeDepartmentHistoryService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _repositoryManager = new HRRepositoryManager(context);
                IHumanResourceServiceManager servicesManager = new HumanResourceServiceManager(_repositoryManager, mapper);

                var employeeEdit = await servicesManager
                    .EmployeeDepartmentHistoryService
                    .GetEmployeeDepartmentHistoryById(20782,false);

                employeeEdit.EndDate = DateTime.Now.AddDays(1);

                servicesManager.EmployeeDepartmentHistoryService.Edit(employeeEdit);

                employeeEdit.EndDate.ShouldBeEquivalentTo(DateTime.Now.AddDays(10));

                /*   var employeeDepartmentHistoryDto = new EmployeeDepartmentHistoryDto
                   {
                       BusinessEntityId = 20782,
                       DepartmentId = 5,
                       ShiftId = 3,
                       StartDate = new DateTime(2021, 10, 27),
                       EndDate = DateTime.Now.AddDays(15),
                       ModifiedDate = DateTime.Now

                   };*/

                //servicesManager.EmployeeDepartmentHistoryService.Edit(employeeDepartmentHistoryDto);

                //find businessentytyId=1700 from demployedpartmetn
                /*var employeeHistory = await servicesManager
                    .EmployeeDepartmentHistoryService.GetEmployeeDepartmentHistoryById(1700,false);*/

                //assert
                //employeeHistory.DepartmentId.ShouldBeEquivalentTo(7);

            }
        }

          [Fact]
/*          public void TestDeleteEmployeeDepartmentHistoryService()
          {
              using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
              {
                  _repositoryManager = new HRRepositoryManager(context);
                  IHumanResourceServiceManager humanResourceServiceManager = new HumanResourceServiceManager(_repositoryManager, mapper);

                  var employeeDepartmentHistoryDto = new EmployeeDepartmentHistoryDto
                  {
                      BusinessEntityId = 1700,
                      DepartmentId = 7,
                      ShiftId = 3
                  };

                  humanResourceServiceManager.EmployeeDepartmentHistoryService.Delete(employeeDepartmentHistoryDto);

                //assert
                  var employeeDepartmentHistory = humanResourceServiceManager.EmployeeDepartmentHistoryService.GetEmployeeDepartmentHistoryById(1700,false);
                employeeDepartmentHistory.ShouldNotBeNull();
                employeeDepartmentHistory.Result.Count().ShouldBe(296);



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
