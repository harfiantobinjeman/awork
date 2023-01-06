using AutoMapper;
using AWork.Contracts.Dto.HumanResources.Employee;
using AWork.Contracts.Dto.Purchasing;
using AWork.Contracts.Dto.Sales.SalesPerson;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Persistence;
using AWork.Persistence.Base;
using AWork.Services;
using AWork.Services.Abstraction;
using AWork.Test;
using AWork.Test.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Awork.Test.HumanResources
{
    public class AWorkIswaraEmployeeTest
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionsBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IHRRepositoryManager _repositoryManager;

        public AWorkIswaraEmployeeTest()
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

        /*
                [Fact]
                public void TestDeleteEmployeeService()
                {
                    using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
                    {
                        _repositoryManager = new HRRepositoryManager(context);
                        IHumanResourceServiceManager serviceManager = new HumanResourceServiceManager(_repositoryManager, mapper);

                        var employeeDto = new EmployeeDto
                        {
                            BusinessEntityId = 20778,
                            NationalIdnumber = "123123132"
                        };
                        serviceManager.EmployeeService.Remove(employeeDto);

                        //assert
                        var employeeModel = serviceManager.EmployeeService.GetAllEmployee(false);
                        employeeModel.ShouldNotBeNull();
                        employeeModel.Result.Count().ShouldBe(290);
                    }
                }
        */
/*
        [Fact]
        public void TestEditEmployeeService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                // act
                _repositoryManager = new HRRepositoryManager(context);
                IHumanResourceServiceManager serviceManager = new HumanResourceServiceManager(_repositoryManager, mapper);

                var employeeDto = new EmployeeDto
                {
                    BusinessEntityId = 20780,
                    LoginId = "Cuba test dulu",
                    JobTitle = "Ngetes Doang",
                    CurrentFlag = true,
                    OrganizationLevel = 9,
                    BirthDate = new DateTime(2002, 02, 02),
                    MaritalStatus = "S",
                    Gender = "M",
                    HireDate = DateTime.Now,
                    SalariedFlag = true,
                    VacationHours = 20,
                    SickLeaveHours = 20,
                    NationalIdnumber = "202020202",
                    Rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now
                };
                serviceManager.EmployeeService.Edit(employeeDto);

                //assert
                *//*var employee = serviceManager.EmployeeService.GetAllEmployee(false);
                employee.ShouldNotBeNull();
                employee.Result.Count().ShouldBe(291);*//*
            }
        }
*/
        /*
                [Fact]
                public void TestCreateEmployeeService()
                {
                    using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
                    {
                        _repositoryManager = new HRRepositoryManager(context);
                        IHumanResourceServiceManager serviceManager = new HumanResourceServiceManager(_repositoryManager, mapper);

                        var employeeModel = new EmployeeForCreateDto
                        {
                            BusinessEntityId = 20780,
                            NationalIdnumber = "543214321",
                            LoginId = "Buat Job Candidate",
                            OrganizationLevel = null,
                            JobTitle = "Testing Job Candidate",
                            BirthDate = new DateTime(2001, 11, 11),
                            MaritalStatus = "M",
                            Gender = "M",
                            HireDate = new DateTime(2012, 12, 12),
                            SalariedFlag = true,
                            VacationHours = 12,
                            SickLeaveHours = 12,
                            CurrentFlag = true,
                            Rowguid = Guid.NewGuid(),
                            ModifiedDate = DateTime.Now


                        };

                        serviceManager.EmployeeService.Insert(employeeModel);
                        var employee = serviceManager.EmployeeService.GetAllEmployee(false);
                        employee.ShouldNotBeNull();
                        employee.Result.Count().ShouldBe(290);
                    }
                }
        */
        /*[Fact]
        public void TestGetEmployeeService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _repositoryManager = new HRRepositoryManager(context);
                IHumanResourceServiceManager servicesManager = new HumanResourceServiceManager(_repositoryManager, mapper);

                var employee = servicesManager.EmployeeService.GetAllEmployee(false);

                //Assert
                employee.ShouldNotBeNull();
                employee.Result.Count().ShouldBe(290);
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
