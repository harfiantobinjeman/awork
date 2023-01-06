using AutoMapper;
using AWork.Domain.Base;
using AWork.Persistence.Base;
using AWork.Persistence;
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
using AWork.Test;
using AWork.Test.Mapping;

namespace Awork.Test.HumanResources
{
    public class AWorkIswaraJobCandidateTest
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionsBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IHRRepositoryManager _repositoryManager;

        public AWorkIswaraJobCandidateTest()
        {
            BuildConfiguration();
            SetupOptions();
        }
        private void BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();

        }



        [Fact]
        public void TestGetJobCandidateService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _repositoryManager = new HRRepositoryManager(context);
                IHumanResourceServiceManager servicesManager = new HumanResourceServiceManager(_repositoryManager, mapper);

                var employee = servicesManager.JobCandidateService.GetAllJobCandidate(false);

                //Assert
                employee.ShouldNotBeNull();
                employee.Result.Count().ShouldBe(13);
            }
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
