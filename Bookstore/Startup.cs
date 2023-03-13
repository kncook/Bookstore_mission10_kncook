using Bookstore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        
        public IConfiguration Configuration { get; set; }

        public Startup (IConfiguration temp)
        {
            Configuration = temp;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //added this that tells ASP to use MVC
            services.AddControllersWithViews();

            //connection string to connect to database
            services.AddDbContext<BookstoreContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:BookDBConnection"]);
            });
            //each HTTP request gets its own object
            services.AddScoped<IBookstoreRepository, EFBookstoreRepository>();
            services.AddScoped<IShoppingCartRepository, EFShoppingCartRepository>();

            services.AddRazorPages();

            //allows you to run a session so that things remain in the cart until the end of the session
            services.AddDistributedMemoryCache();
            services.AddSession();

            //gets a new basket if there is not already one (sessionbasket.cs)
            services.AddScoped<Basket>(x => SessionBasket.GetBasket(x));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //uses the wwroot
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("typepage",
                   "{bookType} / Page{pageNum}",
                   new { Controller = "Home", action = "Index" }
                    );

                //endpoints are executed in ORDER -- if we get a page # w/o a type
                endpoints.MapControllerRoute(
                    name: "Paging",
                    pattern: "Page{pageNum}",
                    defaults: new { Controller = "Home", action = "Index", pageNum= 1
                    }); 

                //this is if we were just to get a category
                endpoints.MapControllerRoute("type",
                    "{bookType}",
                    new { Controller = "Home", action = "Index", pageNum = 1 });


                //added/changed this to go to defult controller app
                endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages();
            });

 
        }
    }
}
