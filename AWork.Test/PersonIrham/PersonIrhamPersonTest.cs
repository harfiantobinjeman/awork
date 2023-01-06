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
using Shouldly;
using AWork.Contracts.Dto.PersonModule;
using AWork.Domain.Models;
using AWork.Test.Mapping;

namespace AWork.Test.PersonIrham
{
    public class PersonIrhamPersonTest
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionsBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IPersonRepositoryManager _personRepositoryManager;

        public PersonIrhamPersonTest()
        {
            BuildConfiguration();
            SetupOption();

        }

        //edit
        [Fact]
        public async Task EditPersonAsync()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personRepositoryManager = new PersonRepositoryManager(context);
                var serviceManager = new PersonServiceManager(_personRepositoryManager, mapper);
                var businessEntity = serviceManager.BusinessEntityServices.CreateBusinessEntity();

                var personForCreateDto = new PersonForCreateDto
                {
                    BusinessEntityId = businessEntity.BusinessEntityId,
                    PersonType = "IN",
                    NameStyle = true,
                    FirstName = "Khoerul",
                    MiddleName = "Mutaqin",
                    LastName = "KM",
                    Suffix = null,
                    EmailPromotion = 2,
                    AdditionalContactInfo = null,
                    Demographics = null,
                    Rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now
                };
                var personDto = await serviceManager.PersonServices.CreatePerson(personForCreateDto);
                personDto.FirstName.ShouldBeEquivalentTo("Khoerul");


            }
        }
        //edit
        [Fact]
        public async void CreatePerson()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personRepositoryManager = new PersonRepositoryManager(context);
                var serviceManager = new PersonServiceManager(_personRepositoryManager, mapper);
                var businessEntity = serviceManager.BusinessEntityServices.CreateBusinessEntity();

                var personCreateDto = new PersonForCreateDto
                {
                    BusinessEntityId= businessEntity.BusinessEntityId,
                    PersonType = "SC",                   
                    FirstName = "IAMM",
                    MiddleName = "R",
                    LastName = "AMI",
                    EmailPromotion = 0,
                   
                    
                };

               var personDto =  await serviceManager.PersonServices.CreatePerson(personCreateDto);

                personDto.FirstName.ShouldBeEquivalentTo("IAMM");



            }
        }

        //get
        [Fact]
        public void GetPerson()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personRepositoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personRepositoryManager, mapper);

                var person = serviceManager.PersonServices.GetAllPerson(false);

                person.ShouldNotBeNull();
                person.Result.Count().ShouldBe(19979);
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
