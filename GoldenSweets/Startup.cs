using AutoMapper;
using GoldenSweets.Core;
using GoldenSweets.Core.Models;
using GoldenSweets.Persistence;
using GoldenSweets.Core;
using GoldenSweets.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GoldenSweets
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment en)
        {
            //Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(en.ContentRootPath)
                //.AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Production.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<ICakeRepository, CakeRepository>();
            services.AddScoped<IGalleryRepository, GalleryRepository>(); 
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRatingRepository, RatingRepository>();

            services.AddScoped<IShoppingCartService>(sp => ShoppingCartService.GetCart(sp));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<GoldenSweetsDbContext>(ctx =>
            {
                ctx.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddAutoMapper();

            services.AddMemoryCache();

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<GoldenSweetsDbContext>();

            services.AddSession();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/UnAuthorized";
            });

        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/AppException");
            }

            app.UseStatusCodePages();
            app.UseSession();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {

                //routes.MapRoute(
                //    name: "categoryFilter",
                //    template: "Cakes/{action}/{category?}",
                //    defaults: new { Controller = "Cake", action = "List" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
