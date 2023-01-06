using AutoMapper;
using AWork.Contracts.Dto.Production;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Persistence;
using AWork.Persistence.Base;
using AWork.Services;
using AWork.Services.Abstraction;
using AWork.Test;
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
    public class AWorkIntegrationYuliTest
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<AdventureWorks2019Context> optionsBuilder;

        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IProductionRepositoryManager productionRepositoryManager;

        public AWorkIntegrationYuliTest()
        {
            BuilderConfiguration();
            SetupOptions();
        }
        /*[Fact]
        public void TestGetProdCateService()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                productionRepositoryManager = new ProductionRepositoryManager(context);
                IProductionServiceManager productionServiceManager = new ProductionServiceManager(productionRepositoryManager, mapper);

                var prodCate = productionServiceManager.ProductCategoryService.GetAllProdcCategory(true);

                //assert
                prodCate.ShouldNotBeNull();
                prodCate.Result.Count().ShouldBe(4);
            }

        }*/
        /* [Fact]
         public void TestGetProdSubCateService()
         {
             using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
             {
                 productionRepositoryManager = new ProductionRepositoryManager(context);
                 IProductionServiceManager productionServiceManager = new ProductionServiceManager(productionRepositoryManager, mapper);

                 var prodCate = productionServiceManager.ProductSubCategoryService.GetAllProdcSubCategory(false);

                 //assert
                 prodCate.ShouldNotBeNull();
                 prodCate.Result.Count().ShouldBe(38);
             }

         }*/

        /* [Fact]
         public void TestCreateCategoryService()
         {
             using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
             {
                 productionRepositoryManager = new ProductionRepositoryManager(context);
                 IProductionServiceManager productionServiceManager = new ProductionServiceManager(productionRepositoryManager, mapper);

                 var categoryDto = new ProductCategoryForCreatDto
                 {
                     Name = "Entah"
                 };
                 productionServiceManager.ProductCategoryService.Insert(categoryDto);

                 //assert
                 categoryDto.Name.ShouldSatisfyAllConditions();
             }
         }*/

        /* [Fact]
         public void TestRemoveCategoryService()
         {
             using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
             {
                 productionRepositoryManager = new ProductionRepositoryManager(context);
                 IProductionServiceManager productionServiceManager = new ProductionServiceManager(productionRepositoryManager, mapper);

                 var categoryDto = new ProductCategoryDto
                 {
                     Name = "Entah",
                     ProductCategoryId = 8

                 };
                 productionServiceManager.ProductCategoryService.Remove(categoryDto);

                 //assert
                 categoryDto.Name.ShouldSatisfyAllConditions();
             }
         }*/
        /* [Fact]
         public void TestGetSubCategoryRepo()
         {
             using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
             {
                 //act
                 productionRepositoryManager = new ProductionRepositoryManager(context);
                 var category = productionRepositoryManager.ProductSubCategoryRepository.GetAllProdcSubCategory(false);

                 //assert
                 category.ShouldNotBeNull();
                 category.Result.Count().ShouldBe(37);
             }
         }*/
        /*  [Fact]
          public void TestRemoveProductSubCategoryRepo()
          {
              using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
              {
                  //act
                  productionRepositoryManager = new ProductionRepositoryManager(context);

                  //define model category
                  var productSubCategor = new ProductSubcategory
                  {
                      Name = "Cinta",
                      ProductCategoryId = 1,
                      ProductSubcategoryId =39

                  };

                  productionRepositoryManager.ProductSubCategoryRepository.remove(productSubCategor);
                  productionRepositoryManager.Save();

                  //productCategory.ProductCategoryId.ShouldBeEquivalentTo(1);

                  //assert
                  var proCate = productionRepositoryManager.ProductSubCategoryRepository.GetAllProdcSubCategory(false);

                  proCate.ShouldNotBeNull();
                  proCate.Result.Count().ShouldNotBe(39);

              }

          }
  */
        /*[Fact]
        public void TestCreateProductSubCategoryRepo()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                //act
                productionRepositoryManager = new ProductionRepositoryManager(context);

                //define model category
                var productSubCategor = new ProductSubcategory
                {
                    Name = "Asma",
                    ProductCategoryId = 4,
                    
                };

                productionRepositoryManager.ProductSubCategoryRepository.insert(productSubCategor);
                productionRepositoryManager.Save();

                //productCategory.ProductCategoryId.ShouldBeEquivalentTo(1);

                //assert
                var proCate = productionRepositoryManager.ProductSubCategoryRepository.GetAllProdcSubCategory(false);

                proCate.ShouldNotBeNull();
                proCate.Result.Count().ShouldNotBe(38);

            }

        }*/
        /* [Fact]
         public void TestEditProductSubCategoryRepo()
         {
             using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
             {
                 //act
                 productionRepositoryManager = new ProductionRepositoryManager(context);

                 //define model category
                 var productSubCategor = new ProductSubcategory
                 {
                     Name = "Kemana",
                     ProductCategoryId = 4,
                     ProductSubcategoryId = 41

                 };

                 productionRepositoryManager.ProductSubCategoryRepository.edit(productSubCategor);
                 productionRepositoryManager.Save();

                 //productCategory.ProductCategoryId.ShouldBeEquivalentTo(1);

                 //assert
                 var proCate = productionRepositoryManager.ProductSubCategoryRepository.GetAllProdcSubCategory(false);

                 proCate.ShouldNotBeNull();
                 proCate.Result.Count().ShouldNotBe(38);

             }

         }*/



        [Fact]
        public void TestGetCategoryRepo()
        {
            using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
            {
                //act
                productionRepositoryManager = new ProductionRepositoryManager(context);
                var category = productionRepositoryManager.ProductCategoryRepository.GetAllProdcCategory(false);

                //assert
                category.ShouldNotBeNull();
                category.Result.Count().ShouldBe(4);
            }
        }

        /* [Fact]
         public void TestRemoveProductCategoryRepo()
         {
             using (var context = new AdventureWorks2019Context(optionsBuilder.Options))
             {
                 //act
                 productionRepositoryManager = new ProductionRepositoryManager(context);

                 //define model category
                 var productCategory = new ProductCategory
                 {
                     Name = "Satin",
                     ProductCategoryId = 6


                 };

                 productionRepositoryManager.ProductCategoryRepository.remove(productCategory);
                 productionRepositoryManager.Save();

                 //productCategory.ProductCategoryId.ShouldBeEquivalentTo(1);

                 //assert
                *//* var proCate = productionRepositoryManager.ProductCategoryRepository.GetAllProdcCategory(false);

                 proCate.ShouldNotBeNull();
                 proCate.Result.Count().ShouldNotBe(6);*//*

             }
         }*/


        public void BuilderConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration =builder.Build();
        }

        private void SetupOptions()
        {
            optionsBuilder=new DbContextOptionsBuilder<AdventureWorks2019Context>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("AdventureWorks2019Db"));

            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(MappingProfile));
            serviceProvider=services.BuildServiceProvider();

            mapperConfig = new MapperConfiguration(cfg =>
              {
                  cfg.AddProfile<MappingProfile>();
                  //cfg.ValidateInlineMaps = false;
              });

            mapperConfig.AssertConfigurationIsValid();
            mapper = mapperConfig.CreateMapper();
        }
    }
}
