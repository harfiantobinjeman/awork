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
    public class PersonIrhamAddressTest
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionsBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IPersonRepositoryManager _personRepositoryManager;

        public PersonIrhamAddressTest()
        {
            BuildConfiguration();
            SetupOption();

        }

        //delete
        [Fact]
        public void DeleteAddress()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personRepositoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personRepositoryManager, mapper);

                var addressDto = new AddressDto()
                {
                    AddressId = 32524,
              

                };
                serviceManager.AddressServices.Remove(addressDto);
            
            }
        }

        //Edit Address
        [Fact]
        public void EditAddress()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personRepositoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personRepositoryManager, mapper);

                var address = new AddressDto()
                {
                    AddressId = 32524,
                    AddressLine1 = "Pasar Minggu",
                    AddressLine2 = "Lenteng Agung",
                    StateProvinceId = 203,
                    City = "jakarta",
                    PostalCode = "66666",
                    Rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now
                    
                };
                serviceManager.AddressServices.Edit(address);

            }
        }


        //createAddress
        [Fact]
        public void CreateAdress()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personRepositoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personRepositoryManager, mapper);

                var addressDto = new AddressForCreateDto
                {
                    AddressLine1 = "Pondok Indah",
                    AddressLine2 = "Blok m",
                    City = "Jakarta",
                    PostalCode = "12220",
                    StateProvinceId = 203,
                    Rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now

                };
                serviceManager.AddressServices.Insert(addressDto);
              
            }
        }
        ////Adress
        [Fact]
        public void GetAddress()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personRepositoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personRepositoryManager, mapper);

                var address = serviceManager.AddressServices.GetAllAddress(false);
                address.ShouldNotBeNull();
                address.Result.Count().ShouldBe(19617);
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
