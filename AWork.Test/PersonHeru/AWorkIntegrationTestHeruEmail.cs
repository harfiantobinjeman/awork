using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AWork.Contracts.Dto.PersonModule;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Persistence;
using AWork.Persistence.Base;
using AWork.Services;
using AWork.Services.Abstraction;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using AWork.Contracts.Dto;

namespace AWork.Test.Mapping
{
    public class AWorkIntegrationTestHeruEmail
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionsBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IPersonRepositoryManager _personReporitoryManager;

        public AWorkIntegrationTestHeruEmail()
        {
            BuildConfiguration();
            setupOption();
        }
        [Fact]
        public void TestReadEmailService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                //act
                _personReporitoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager personServiceManager = new PersonServiceManager(_personReporitoryManager, mapper);

                //define emailaddress
                var emailAddresses = personServiceManager.EmailAddressServices.GetAllEmailAddress(false);

                //asset
                emailAddresses.ShouldNotBeNull();
                emailAddresses.Result.Count().ShouldBe(19973);

            }
        }

        [Fact]
        public void TestDeleteEmailService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                //Act
                _personReporitoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager personServiceManager = new PersonServiceManager(_personReporitoryManager, mapper);

                //define email address
                var emailAddressDto = new EmailAddressDto
                {
                    BusinessEntityId = 20778,
                    EmailAddressId = 19989,
                    EmailAddress1 = "khoerulmutaqin@Rocketmail.com"
                };
                personServiceManager.EmailAddressServices.Delete(emailAddressDto);
                _personReporitoryManager.Save();
                //asset
                var emailAddresss = _personReporitoryManager.EmailAddressRepository.GetAllEmailAddress(false);
                emailAddresss.ShouldNotBeNull();
                emailAddresss.Result.Count().ShouldBe(19974);
            }
        }
        [Fact]
        public void TestInsertEmailService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                //act
                _personReporitoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager personServiceManager = new PersonServiceManager(_personReporitoryManager, mapper);

                //define email address
                var emailAddressForCreateDto = new EmailAddressForCreateDto
                {
                    BusinessEntityId = 20778,
                    EmailAddress1 = "khoerulmutaqin@Rocketmail.com"
                };

                personServiceManager.EmailAddressServices.Insert(emailAddressForCreateDto);
                _personReporitoryManager.Save();

                //assert
                var emailAddresses = personServiceManager.EmailAddressServices.GetAllEmailAddress(false);
                emailAddresses.ShouldNotBeNull();
                emailAddresses.Result.Count().ShouldBe(19975);

            }
        }
        [Fact]
        public void TestEditemailService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                //act
                _personReporitoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager personServiceManager = new PersonServiceManager(_personReporitoryManager, mapper);

                //define email address
                var emailAddressDto = new EmailAddressDto
                {

                    EmailAddressId = 0,
                    EmailAddress1 = "khoerulmutaqin529@yahoo.com",
                };

                personServiceManager.EmailAddressServices.Edit(emailAddressDto);
                _personReporitoryManager.Save();

                //assert
                var emailAddresses = personServiceManager.EmailAddressServices.GetAllEmailAddress(false);
                emailAddresses.ShouldNotBeNull();
                emailAddresses.Result.Count().ShouldBe(19973);

            }
        }


        private void BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }

        private void setupOption()
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
