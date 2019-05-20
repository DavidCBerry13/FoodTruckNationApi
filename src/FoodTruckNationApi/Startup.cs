#pragma warning disable 1591

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.AppServices;
using Framework;
using System.IO;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Framework.ApiUtil.Filters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Framework.ApiUtil.Controllers;
using System.Linq;
using FluentValidation.AspNetCore;
using System.Reflection;
using App.Metrics.Health;
using App.Metrics.Health.Builder;
using System;
using App.Metrics.Health.Checks.Sql;
using FoodTruckNation.Data.EF;
using FoodTruckNation.Core;
using Microsoft.AspNetCore.Mvc;

namespace FoodTruckNationApi
{
    public class Startup
    {



        public Startup(IHostingEnvironment env, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            // Hold onto a reference to the hosting environment and the logger factory
            _hostingEnvironment = env;
            _loggerFactory = loggerFactory;
            _configuration = configuration;            
        }


        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILoggerFactory _loggerFactory;


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                //options.Filters.Add(new ValidationAttribute());
                options.Filters.Add(new ExceptionHandlerFilterAttribute(_loggerFactory));                
            })
             .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)  // Uncomment to use ApiController Attribute
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddCors();
            ConfigureServicesDI(services);
            services.AddAutoMapper();
            ConfigureServicesVersioning(services);
            ConfigureServicesSwagger(services);

            var memoryThreshhold = 800000000; // 800 MB
            var healthBuilder = new HealthBuilder()
                // Check that the current amount of physical memory in bytes is below a threshold
                .HealthChecks.AddProcessPhysicalMemoryCheck("Working Set", memoryThreshhold)
                // Check connectivity to google with a "ping", passes if the result is `IPStatus.Success`
                .HealthChecks.AddPingCheck("google ping", "google.com", TimeSpan.FromSeconds(10))
                // Check that our SQL Server is still up and running
                .HealthChecks.AddSqlCheck("Food Truck Database", 
                    _configuration.GetConnectionString("FoodTruckConnectionString"), TimeSpan.FromSeconds(10))
                .OutputHealth.AsJson()
                .BuildAndAddTo(services);
            services.AddHealthEndpoints();
        }





        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Configure Cors
            app.UseCors(builder =>
               builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod()            
            );

            app.UseMvc();

            // For AppMetrics health Endpoints
            app.UseHealthEndpoint();
            app.UsePingEndpoint();

            app.UseSwagger(c =>
            {                
                c.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Value);

            });
            
            app.UseSwaggerUI(c =>
            { 
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Food Truck API v1.0");
                c.SwaggerEndpoint("/swagger/v1.1/swagger.json", "Food Truck API v1.1");                
            });

        }



        #region Configure Services Helpers

        /// <summary>
        /// Helper method where all the dependency injection is configured
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureServicesDI(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(_configuration);

            if (_hostingEnvironment.EnvironmentName == "IntegrationTests")
            {
                var testDbName = _configuration["TestName"];
                services.ConfigureInMemoryDataAccess(testDbName);
            }
            else
            {
                var connectionString = _configuration.GetConnectionString("FoodTruckConnectionString");
                services.ConfigureSqlServerDataAccess(connectionString, _loggerFactory);
            }

            services.ConfigureFoodTruckServices();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IDateTimeProvider, StandardDateTimeProvider>();
        }


        /// <summary>
        /// Helper method to setup Swagger
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureServicesSwagger(IServiceCollection services)
        {
            var pathToXmlComments = _configuration["Swagger:FileName"];
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1.1", new Info { Title = "Food Truck API v1.1", Version = "v1.1" });
                options.SwaggerDoc("v1.0", new Info { Title = "Food Truck API v1.0", Version = "v1.0" });
                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var actionApiVersionModel = apiDesc.ActionDescriptor?.GetApiVersion();
                    if (actionApiVersionModel == null)
                    {
                        return true;
                    }
                    if (actionApiVersionModel.DeclaredApiVersions.Any())
                    {
                        return actionApiVersionModel.DeclaredApiVersions.Any(v => $"v{v.ToString()}" == docName);
                    }
                    return actionApiVersionModel.ImplementedApiVersions.Any(v => $"v{v.ToString()}" == docName);
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.DescribeAllEnumsAsStrings();
                options.CustomSchemaIds(x => x.FullName);
            });
        }


        private void ConfigureServicesVersioning(IServiceCollection services)
        {
            services.AddApiVersioning(cfg =>
            {
                cfg.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                cfg.AssumeDefaultVersionWhenUnspecified = true;
                cfg.ReportApiVersions = true;
                cfg.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
        }

        #endregion


    }
}

#pragma warning restore 1591