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
    public class Widi_DepartmentServiceTest
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionsBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IHRRepositoryManager _repositoryManager;

        public Widi_DepartmentServiceTest()
        {
            BuildConfiguration();
            SetupOptions();
        }

        [Fact]
        public void TestGetDepartmenService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _repositoryManager = new HRRepositoryManager(context);
                IHumanResourceServiceManager servicesManager = new HumanResourceServiceManager(_repositoryManager, mapper);
                var department = servicesManager.DepartmentService.GetAllDepartment(false);

                //Assert
                department.ShouldNotBeNull();
                department.Result.Count().ShouldBe(16);

            }
        }

        [Fact]
        public void TestCreateDepartmentService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _repositoryManager = new HRRepositoryManager(context);
                IHumanResourceServiceManager humanResourceServiceManager = new HumanResourceServiceManager(_repositoryManager, mapper);

                var departmentDto = new DepartmentForCreateDto
                {
                    Name = "Data Scientist",
                    GroupName = "Data Engineering",
                    ModifiedDate = DateTime.Now
                };
                humanResourceServiceManager.DepartmentService.insert(departmentDto);

            }
        }

        [Fact]
        public void TestEditDepartmentService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _repositoryManager = new HRRepositoryManager(context);
                IHumanResourceServiceManager humanResourceServiceManager = new HumanResourceServiceManager(_repositoryManager, mapper);

                var departmentForCreateDto = new DepartmentDto
                {
                    DepartmentId = 23,
                    Name = "Network Administrator",
                    GroupName = "IT Department",
                    ModifiedDate = DateTime.Now
                };
                humanResourceServiceManager.DepartmentService.edit(departmentForCreateDto);

            }
        }

        [Fact]
        public void TestDeleteDepartmentService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _repositoryManager = new HRRepositoryManager(context);
                IHumanResourceServiceManager humanResourceServiceManager = new HumanResourceServiceManager(_repositoryManager, mapper);

                var departmentForCreateDto = new DepartmentDto
                {
                    DepartmentId = 23,
                    Name = "Network Administrator",
                    GroupName = "IT Department",
                    ModifiedDate = DateTime.Now
                };
                humanResourceServiceManager.DepartmentService.delete(departmentForCreateDto);

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
