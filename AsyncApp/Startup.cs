using System;
using AsyncApp.Data;
using AsyncApp.Models;
using AsyncApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AsyncApp
{
    public class Startup
    {
        // 1. Propery to hold configuration
        public IConfiguration Configuration { get; }

        // 2. Constructor to receive configuration
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // 3. Register DbContext with the app
            services.AddDbContext<HotelDbContext>(options =>
            {
                // DATABASE_URL equivalent 
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Hotel Docs", Version = "v1" });
            });

            services.AddTransient<IHotelRepository, DatabaseHotelRepository>();

            services.AddTransient<IAmenityRepository, DatabaseAmenityRepository>();

            services.AddTransient<IHotelRoomRepository, DatabaseHotelRoomRepository>();

            services.AddTransient<IRoomRepository, DatabaseRoomRepository>();

            services
                .AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<HotelDbContext>();

            services.AddTransient<IUserService, IdentityUserService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(options =>
            {
                options.RouteTemplate = "/api/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("api/v1/swagger.json", "Hotel Docs");
                options.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });

                endpoints.MapGet("/boom", context =>
                {
                    throw new InvalidOperationException("boom");
                });

            });
        }
    }
}
