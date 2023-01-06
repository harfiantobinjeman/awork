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
using AWork.Test.Mapping;

namespace AWork.Test.PersonIrham
{
    public class PersonIrhamBusinessEntityTest
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionsBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IPersonRepositoryManager _personRepositoryManager;

        public PersonIrhamBusinessEntityTest()
        {
            BuildConfiguration();
            SetupOption();

        }



        //createBusinessEntity
        [Fact]
        public void CreateBusinessEntity()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personRepositoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personRepositoryManager, mapper);

                var businessEntityDto = new BusinessEntityForCreateDto
                {
                    ModifiedDate = DateTime.Now,
                    Rowguid = Guid.Empty

                };
                // serviceManager.BusinessEntityServices.Insert(businessEntityDto);
                var businessEntity = serviceManager.BusinessEntityServices.CreateBusinessEntity();
                businessEntity.ShouldNotBeNull();
                businessEntity.BusinessEntityId.ShouldBe(20809);

            }
        }
        //Get
        [Fact]
        public void GetBusinessEntity()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personRepositoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personRepositoryManager, mapper);

                var businessEntity = serviceManager.BusinessEntityServices.GetAllBusinessEntity(false);
                businessEntity.ShouldNotBeNull();
                businessEntity.Result.Count().ShouldBe(20809);
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
