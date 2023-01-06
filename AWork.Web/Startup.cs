using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Persistence;
using AWork.Persistence.Base;
using AWork.Services;
using AWork.Services.Abstraction;
using AWork.Services.Abstraction.Production;
using AWork.Services.Production;
using AWork.Web.Extensions;
using AWork.Web.Factory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace AWork.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserClaimsPrincipalFactory<User>, CustomClaimsFactory>();

            services.AddControllersWithViews();

            services.AddScoped<IPersonRepositoryManager, PersonRepositoryManager>();
            services.AddScoped<IPersonServiceManager, PersonServiceManager>();

            services.AddScoped<IPurchasingServiceManager, PurchasingServiceManager>();
            services.AddScoped<IPurchasingRepositoryManager, PurchasingRepositoryManager>();
            services.AddScoped<IProductionRepositoryManager, ProductionRepositoryManager>();
            services.AddScoped<IProductionServiceManager, ProductionServiceManager>();
            services.AddScoped<ISalesRepositoryManager, SalesRepositoryManager>();
            services.AddScoped<ISalesServiceManager, SalesServiceManager>();

            services.AddScoped<IHRRepositoryManager, HRRepositoryManager>();
            services.AddScoped<IHumanResourceServiceManager, HumanResourceServiceManager>();
            services.AddScoped<IUtilityService, UtilityService>();



            //add ConfigureIdentity here from Service Extensions
            services.ConfigureIdentity();
            //services.ConfigureApplicationCookie(o => o.LoginPath = "/Authentication/Login");




            services.AddAutoMapper(typeof(Startup));

            // conection to databases
            services.AddDbContext<AdventureWorks2019Context>(opts =>
            {
                opts.UseSqlServer(Configuration["ConnectionStrings:AdventureWorks2019Db"]);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseRouting();
            //call useAuthencation
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //call useAuthentication
            app.UseAuthentication();
            app.UseAuthorization();


        }
    }
}
                                                