using AutoMapper;
using AWork.Domain.Base;
using AWork.Persistence;
using AWork.Persistence.Base;
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
using AWork.Contracts.Dto.PersonModule;
using Shouldly;
using AWork.Test.Mapping;

namespace AWork.Test.PersonIrham
{
    public class PersonIrhamStateProvinceTest
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionsBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IPersonRepositoryManager _personRepositoryManager;

        public PersonIrhamStateProvinceTest()
        {
            BuildConfiguration();
            SetupOption();

        }

        //remove
        
        [Fact]
        public void RemoveStateProvince()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personRepositoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personRepositoryManager, mapper);


                var stateProvinceDto = new StateProvinceDto
                {
                    StateProvinceId = 202
                    

                };

                serviceManager.StateProvinceServices.Remove(stateProvinceDto);

            }
        }

        //edit
        [Fact]
        public void EditStateProvince()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personRepositoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personRepositoryManager, mapper);


                var stateProvinceDto = new StateProvinceDto
                {
                    StateProvinceId = 202,
                    CountryRegionCode = "CPD",
                    StateProvinceCode = "JKK",                 
                    Name = "Setu Babakan",
                    ModifiedDate = DateTime.Now,
                    IsOnlyStateProvinceFlag = true,
                    TerritoryId = 3,
                    Rowguid = Guid.NewGuid()

                };

                serviceManager.StateProvinceServices.Edit(stateProvinceDto);

            }
        }


        //create
        [Fact]
        public void CreateStateprovince()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personRepositoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personRepositoryManager, mapper);
                

                var stateProvinceDto = new StateProvinceForCreateDto
                {
                    StateProvinceCode = "JKT",
                    CountryRegionCode = "CPD",
                    Name = "Jagakarsa",
                    ModifiedDate = DateTime.Now,
                    IsOnlyStateProvinceFlag = true,
                    TerritoryId = 3,                    
                    Rowguid = Guid.NewGuid()
                   
                };

                serviceManager.StateProvinceServices.Insert(stateProvinceDto);
               
            }
        }

        //stateProvince
        [Fact]
        public void GetStateprovince()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personRepositoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personRepositoryManager, mapper);

                var stateProvince = serviceManager.StateProvinceServices.GetAllStateprovince(false);
                stateProvince.ShouldNotBeNull();
                stateProvince.Result.Count().ShouldBe(184);
            }
        }

        private void BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }

        private void SetupOption()
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
