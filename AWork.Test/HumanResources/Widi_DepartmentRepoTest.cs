using AutoMapper;
using AWork.Contracts.Dto.HumanResources;
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
    public class Widi_DepartmentRepoTest
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionsBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IHRRepositoryManager _hRRepositoryManager;

        public Widi_DepartmentRepoTest()
        {
            BuildConfiguration();
            SetupOptions();
        }

        [Fact]
        public void TestGetDepartmentRepo()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                //act
                _hRRepositoryManager = new HRRepositoryManager(context);
                var department = _hRRepositoryManager.DepartmentRepository.GetAllDepartment(false);

                //assert
                department.ShouldNotBeNull();
                department.Result.Count().ShouldBe(16);
            }
        }

        [Fact]
        public void TestCreateDepartmentRepo()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _hRRepositoryManager = new HRRepositoryManager(context);

                var departmentModel = new Department
                {
                    Name = "Data Scientist",
                    GroupName = "Data Engineering"
                };

                _hRRepositoryManager.DepartmentRepository.Insert(departmentModel);
                _hRRepositoryManager.Save();


                //assert
                var department = _hRRepositoryManager.DepartmentRepository.GetAllDepartment(false);
                department.ShouldNotBeNull();
                department.Result.Count().ShouldBe(17);

            }
        }

        [Fact]
        public void TestRemoveDepartmentRepo()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _hRRepositoryManager = new HRRepositoryManager(context);

                var departmentModels = new Department
                {
                    Name = "Network Administrator",
                    GroupName = "IT Department",
                    DepartmentId = 22
                };

                _hRRepositoryManager.DepartmentRepository.Remove(departmentModels);
                _hRRepositoryManager.Save();


                //assert
                var department = _hRRepositoryManager.DepartmentRepository.GetAllDepartment(true);
                department.ShouldNotBeNull();
                department.Result.Count().ShouldBe(16);

            }
        }

        [Fact]
        public void TestEditDepartmentRepo()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _hRRepositoryManager = new HRRepositoryManager(context);

                var departmentModel = new Department
                {
                    Name = "Network Administrator",
                    GroupName = "IT Department",

                };

                _hRRepositoryManager.DepartmentRepository.Edit(departmentModel);
                _hRRepositoryManager.Save();

                //assert
                var department = _hRRepositoryManager.DepartmentRepository.GetAllDepartment(false);
                department.ShouldNotBeNull();
                department.Result.Count().ShouldBe(17);
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
