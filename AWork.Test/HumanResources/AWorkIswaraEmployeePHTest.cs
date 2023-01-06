using AutoMapper;
using AWork.Domain.Base;
using AWork.Persistence.Base;
using AWork.Persistence;
using AWork.Services.Abstraction;
using AWork.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using AWork.Contracts.Dto.HumanResources.Employee;
using AWork.Contracts.Dto.HumanResources.EmployeePayHistory;
using AWork.Domain.Models;
using AWork.Test;
using AWork.Test.Mapping;

namespace Awork.Test.HumanResources
{
    public class AWorkIswaraEmployeePHTest
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionsBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IHRRepositoryManager _repositoryManager;

        public AWorkIswaraEmployeePHTest()
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
        public void TestDeleteEmployeePHService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _repositoryManager = new HRRepositoryManager(context);
                IHumanResourceServiceManager serviceManager = new HumanResourceServiceManager(_repositoryManager, mapper);

                var employeePHDto = new EmployeePayHistoryDto
                {
                    BusinessEntityId = 290,
                    RateChangeDate = new DateTime(2012, 05, 30)
                };
                serviceManager.EmployeePayHistoryService.Remove(employeePHDto);

                //assert
                /*var employeeModel = serviceManager.EmployeePayHistoryService.GetAllEmployeePayHistory(false);
                employeeModel.ShouldNotBeNull();
                employeeModel.Result.Count().ShouldBe(290);*/
            }
        }

        /*
                [Fact]
                public void TestEditEmployeePHService()
                {
                    using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
                    {
                        // act
                        _repositoryManager = new HRRepositoryManager(context);
                        IHumanResourceServiceManager serviceManager = new HumanResourceServiceManager(_repositoryManager, mapper);

                        var employeePHDto = new EmployeePayHistoryDto
                        {
                            BusinessEntityId = 290,
                            RateChangeDate = new DateTime(2012,05,30),
                            Rate = 25,
                            PayFrequency = 2,
                            ModifiedDate = DateTime.Now
                        };
                        serviceManager.EmployeePayHistoryService.Edit(employeePHDto);

                        //assert
                        var employee = serviceManager.EmployeeService.GetAllEmployee(false);
                        employee.ShouldNotBeNull();
                        employee.Result.Count().ShouldBe(291);
                    }
                }
        */
        /*
                [Fact]
                public void TestCreateEmployeePHService()
                {
                    using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
                    {
                        _repositoryManager = new HRRepositoryManager(context);
                        IHumanResourceServiceManager serviceManager = new HumanResourceServiceManager(_repositoryManager, mapper);

                        var employeePHModel = new EmployeePHForCreateDto
                        {
                            BusinessEntityId = 20779,
                            RateChangeDate = DateTime.Now,
                            Rate = 12 ,
                            PayFrequency = 1,
                            ModifiedDate = DateTime.Now
                        };

                        serviceManager.EmployeePayHistoryService.Insert(employeePHModel);
                        *//*var employee = serviceManager.EmployeeService.GetAllEmployee(false);
                        employee.ShouldNotBeNull();
                        employee.Result.Count().ShouldBe(290);*//*
                    }
                }
        */


        [Fact]
        public void TestGetEmployeePHService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _repositoryManager = new HRRepositoryManager(context);
                IHumanResourceServiceManager servicesManager = new HumanResourceServiceManager(_repositoryManager, mapper);

                var employee = servicesManager.EmployeePayHistoryService.GetAllEmployeePayHistory(false);

                //Assert
                employee.ShouldNotBeNull();
                employee.Result.Count().ShouldBe(316);
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
