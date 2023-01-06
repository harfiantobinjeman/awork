using AutoMapper;
using AWork.Contracts.Dto.Purchasing;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AWork.Contracts.Dto.PersonModule;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Persistence;
using AWork.Persistence.Base;
using AWork.Persistence.Repositories.Purchasing;
using AWork.Services;
using AWork.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using Northwind.Contracts.Dto.Category;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AWork.Test.Mapping
{
    public class AWorkIntegrationTest
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionsBuilder;

        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IPersonRepositoryManager _personReporitoryManager;

        public AWorkIntegrationTest()
        {
            BuildConfiguration();
            setupOption();
        }


        [Fact]
        public void createStateProvince()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personReporitoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personReporitoryManager, mapper);

                var stateProvinveDto = new StateProvinceForCreateDto
                {
                    Name = "Jakarta",
                    TerritoryId = 8
                };
                serviceManager.StateProvinceServices.Insert(stateProvinveDto);
                var state = serviceManager.StateProvinceServices.GetAllStateprovince(false);
                state.ShouldNotBeNull();
                state.Result.Count().ShouldBe(182);
            }
        }


        [Fact]
        public void getCountryRegion()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personReporitoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personReporitoryManager, mapper);

                var countryRegion = serviceManager.CountryRegionServices.GetAllCountryRegion(false);
                countryRegion.ShouldNotBeNull();
                countryRegion.Result.Count().ShouldBe(238);
            }
        }
        /*[Fact]
        public void getBusinessEntity()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personReporitoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personReporitoryManager, mapper);

                var BusinessEntity = serviceManager.BusinessEntityServices.GetAllBusinessEntity(false);
                BusinessEntity.ShouldNotBeNull();
                BusinessEntity.Result.Count().ShouldBe(20777);
            }
        }*/

        [Fact]
        public void getallperson()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personReporitoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personReporitoryManager, mapper);

                var person = serviceManager.PersonServices.GetAllPerson(false);
                person.ShouldNotBeNull();
                person.Result.Count().ShouldBe(19972);
            }
        }

        /*[Fact]
        public void createPerson()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                _personReporitoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new PersonServiceManager(_personReporitoryManager, mapper);

                var personDto = new PersonForCreateDto
                {
                    PersonType = "SNTL",
                    FirstName = "irham",
                    LastName = "rafi"

                     ShipDate = DateTime.Today.AddDays(1),
                     PurchaseOrderId = 2,
                     OrderDate = DateTime.Now.AddDays(2),
                     Status = 3,
                     Freight = 7
                };

                serviceManager.PersonServices.Insert(personDto);
                var person = serviceManager.PersonServices.GetAllPerson(false);

                person.ShouldNotBeNull();
                person.Result.Count().ShouldBe(20778);
            }
        }
*/













 /*       [Fact]
        public void TestGetPasswordRepo()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                //act
                _personReporitoryManager = new PersonRepositoryManager(context);
                var password = _personReporitoryManager.PasswordRepository.GetAllPassword(false);


                //asset
                password.ShouldNotBeNull();
                password.Result.Count().ShouldBe(19973);
            }
        }

*//*        [Fact]
        public void TestGetPasswordRepoInsert()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                //act
                _personReporitoryManager = new PersonRepositoryManager(context);

                //define category
                var passwordModel = new Password
                {
                    PasswordHash = "pbFwXWE99vobT6g+vPWFy93NtUU/orrIWafF01hccfM=",
                    PasswordSalt = "bE3XiWw="
                };

                _personReporitoryManager.PasswordRepository.Insert(passwordModel);
                _personReporitoryManager.Save();

                //asset
                var password = _personReporitoryManager.PasswordRepository.GetAllPassword(false);
                password.ShouldNotBeNull();
                password.Result.Count().ShouldBe(16);

            }
        }*/

        /*        /// Testing Layer Repo Insert
                [Fact]
                public void TestGetPasswordRepoInsert()
                {
                    using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
                    {
                        //act
                        _personReporitoryManager = new PersonRepositoryManager(context);

                        //define category
                        var passwordModel = new Password
                        {
                            PasswordHash = "pbFwXWE99vobT6g+vPWFy93NtUU/orrIWafF01hccfM=",
                            PasswordSalt = "bE3XiWw="
                        };

                        _personReporitoryManager.PasswordRepository.Insert(passwordModel);
                        _personReporitoryManager.Save();

                        //asset
                        var category = _personReporitoryManager.PasswordRepository.GetAllPassword(false);
                        category.ShouldNotBeNull();
                        category.Result.Count().ShouldBe(16);

                    }
                }*/

        /*        /// Testing Layer Services Create
                [Fact]
                public void TestGetCategoryServicesInsert()
                {
                    using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
                    {
                        //act
                        _personReporitoryManager = new PersonRepositoryManager(context);
                        IPersonServiceManager personServiceManager = new PersonServiceManager(_personReporitoryManager, mapper);

                        //define person
                        var personDto = new PersonForCreateDto
                        {
                            PersonType = "Bleace",
                            Title = "History Movie"
                        };

                        personServiceManager.PersonServices.Insert(personDto);
                        _personReporitoryManager.Save();



                        //define person
                        var person = personServiceManager.PersonServices.GetAllPerson(false);

                        //asset
                        person.ShouldNotBeNull();
                        person.Result.Count().ShouldBe(13);

                    }
                }*//*

        /// Testing Layer Repo Get All

        *//*
        /// Testing Layer Repo Insert
        [Fact]
        public void TestGetCategoryRepoInsert()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                //act
                _personReporitoryManager = new PersonRepositoryManager(context);

                //define category
                var categoryModel = new Category
                {
                    CategoryName = "Jujutsu Kaizen",
                    Description = "Adventure Movie"
                };

                _personReporitoryManager.CategoryRepository.Insert(categoryModel);
                _personReporitoryManager.Save();

                //asset
                var category = _personReporitoryManager.CategoryRepository.GetAllCategory(false);
                category.ShouldNotBeNull();
                category.Result.Count().ShouldBe(16);

        [Fact]
        public void TestGetPurchaseOHService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                repositoryManager = new PurchasingRepositoryManager(context);
                IPurchasingServiceManager serviceManager = new PurchasingServiceManager(repositoryManager, mapper);
                var purchase = serviceManager.PurchaseOrderHeaderService.GetAllPurchaseOH(false);
                purchase.ShouldNotBeNull();
                purchase.Result.Count().ShouldBe(4012);
            }
        }


        /// Testing Layer Service Get All
        [Fact]
        public void TestGetCategoryService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                //act
                _personReporitoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager serviceManager = new ServiceManager(_personReporitoryManager, mapper);

                //define category
                var category = serviceManager.CategoryService.GetAllCategory(false);

                //asset
                category.ShouldNotBeNull();
                category.Result.Count().ShouldBe(13);

            }
        }
                /// Testing Layer Service Get All
        [Fact]
        public void TestGetPasswordService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                //act
                _personReporitoryManager = new PersonRepositoryManager(context);
                IPersonServiceManager personServiceManager = new PersonServiceManager(_personReporitoryManager, mapper);

                //define category
                var password = personServiceManager.PasswordServices.GetAllPassword(false);

                //asset
                password.ShouldNotBeNull();
                password.Result.Count().ShouldBe(13);

                var purchase = repositoryManager.PurchaseOrderDetailRepository.GetAllPurchaseOrderDetail(false);
                purchase.ShouldNotBeNull();
                purchase.Result.Count().ShouldBe(8846);
            }
            }
        }*//*
*/
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