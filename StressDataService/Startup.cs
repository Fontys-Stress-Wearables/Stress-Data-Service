using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using StressDataService.Database;
using StressDataService.Interfaces;
using StressDataService.Nats;
using StressDataService.Repositories;
using StressDataService.Services;

namespace StressDataService
{
    public class Startup
    {
        private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<HrvMeasurementService>();
            services.AddSingleton<HrvMeasurementRepository>();

            services.AddSingleton<INatsService, NatsService>();
            services.AddSingleton<InfluxDbService>();
            services.AddSingleton<InfluxDbSeeder>();

            services.AddSingleton<ProcessedDataService>();
            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "StressDataService API",
                    Description = "An ASP.NET Core Web API for managing Stress Data measurements",
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });
            }); 
            
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins, 
                    builder => {
                                    builder.AllowAnyOrigin();
                    });
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, InfluxDbSeeder dbSeeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
                dbSeeder.Seed();
            }
            
            app.UseHttpsRedirection();
            
            app.UseStaticFiles();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.ApplicationServices.GetService<INatsService>();
            app.ApplicationServices.GetService<ProcessedDataService>();
        }
    }
}
