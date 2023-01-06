using AutoMapper;
using AWork.Contracts.Dto.PersonModule;
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
using AWork.Test.Mapping;

namespace AWork.Test.PersonIrham
{
    public class PersonIrhamContactTypeTest
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionsBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IPersonRepositoryManager _personRepositoryManager;

        public PersonIrhamContactTypeTest()
        {
            BuildConfiguration();
            SetupOption();

        }

        //Remove
        [Fact]
        public void RemoveContactType()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personRepositoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personRepositoryManager, mapper);

                var contactTypeDto = new ContactTypeDto
                {
                    ContactTypeId = 28
                };

                serviceManager.ContactTypeServices.Remove(contactTypeDto);

            }
        }

        //EditContactType
        [Fact]
        public void EditContactType()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personRepositoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personRepositoryManager, mapper);

                var contactTypeDto = new ContactTypeDto
                {
                    Name = "Owner Rini Marlina ",
                    ModifiedDate = DateTime.Now
                };

                serviceManager.ContactTypeServices.Edit(contactTypeDto);

            }
        }

        //CreateContactType
        [Fact]
        public void CreateContactType()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personRepositoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personRepositoryManager, mapper);

                var contactTypeDto = new ContactTypeForCreateDto
                {
                    Name = "Owner Marlina ",
                    ModifiedDate = DateTime.Now
                };

                serviceManager.ContactTypeServices.Insert(contactTypeDto);
               
            }
        }

        [Fact]
        //contactType
        public void GetContactType()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personRepositoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personRepositoryManager, mapper);

                var contact = serviceManager.ContactTypeServices.GetAllContactType(false);
                contact.ShouldNotBeNull();
                contact.Result.Count().ShouldBe(25);
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
