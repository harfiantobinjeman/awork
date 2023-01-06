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
    public class PersonIrhamCountryRegionTest
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionsBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IPersonRepositoryManager _personRepositoryManager;

        public PersonIrhamCountryRegionTest()
        {
            BuildConfiguration();
            SetupOption();

        }


         //remove
        [Fact]
        public void RemoveCountryRegion()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personRepositoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personRepositoryManager, mapper);

                var countryRegionDto = new CountryRegionDto
                {
                    CountryRegionCode = "CPD"
                };
                serviceManager.CountryRegionServices.Remove(countryRegionDto);
            }
        }

        //edit
        [Fact]
        public void EditCountryRegion()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personRepositoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personRepositoryManager, mapper);

                var countryRegionDto = new CountryRegionDto
                {
                    CountryRegionCode = "CPD",
                    Name = "CIPEDAK V",
                    ModifiedDate = DateTime.Now
                    
                    
                    
                };
                serviceManager.CountryRegionServices.Edit(countryRegionDto);
            }
        }

        [Fact]
        public void CreateCountryRegion()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personRepositoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personRepositoryManager, mapper);

                var countryRegionDto = new CountryRegionForCreateDto
                {
                    CountryRegionCode = "CPD",
                    Name = "Cipedak",
                    ModifiedDate = DateTime.Now
                    
                };
                serviceManager.CountryRegionServices.Insert(countryRegionDto);
               

            }
        }

        //countryRegion
        [Fact]
        public void GetCountryRegion()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personRepositoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personRepositoryManager, mapper);

                var countryRegion = serviceManager.CountryRegionServices.GetAllCountryRegion(false);
                countryRegion.ShouldNotBeNull();
                countryRegion.Result.Count().ShouldBe(241);
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
