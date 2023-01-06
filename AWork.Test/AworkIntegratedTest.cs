using AutoMapper;
using AWork.Contracts.Dto.PersonModule;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Persistence;
using AWork.Persistence.Base;
using AWork.Persistence.Repositories.PersonModul;
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



namespace AWork.Test
{
    public class AworkIntegratedTest
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionsBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IPersonRepositoryManager _personRepositoryManager;

        public AworkIntegratedTest()
        {
            BuildConfiguration();
            SetupOption();

        }
      /*  [Fact]
        public void CreateCountryRegion()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personRepositoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personRepositoryManager, mapper);

                var countryRegionDto = new CountryRegionForCreateDto
                {
                    CountryRegionCode = "IN",
                    Name = "Indonesia ",
                    ModifiedDate = DateTime.Now
                };
                serviceManager.CountryRegionServices.Insert(countryRegionDto);
                var countryRegion = serviceManager.CountryRegionServices.GetAllCountryRegion(false);
                countryRegion.ShouldNotBeNull();
                countryRegion.Result.Count().ShouldBe(239);

        }
        }

        /*[Fact]
        public void GetPerson()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personRepositoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personRepositoryManager, mapper);

                var personDto = new PersonForCreateDto
                {

                    PersonType = "IN",
                    FirstName = "irham",
                    MiddleName = "R",
                    LastName = "Rafi",
                    EmailPromotion = 4
                };

                serviceManager.PersonServices.Insert(personDto);
                var person = serviceManager.PersonServices.GetAllPerson(false);
                person.ShouldNotBeNull();
                person.Result.Count().ShouldBe(19973);

            }
        }

                person.ShouldNotBeNull();
                person.Result.Count().ShouldBe(19972);
            }
        }*/

        ////personService
        [Fact]
        public void GetPersonRepo()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personRepositoryManager = new PersonRepositoryManager(context);
                var person = _personRepositoryManager.PersonRepository.GetAllPerson(false);

                person.ShouldNotBeNull();
                person.Result.Count().ShouldBe(19972);
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
