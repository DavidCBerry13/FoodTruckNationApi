using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Web;
using NLog.Extensions.Logging;
using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.AppServices;
using Framework;
using System.IO;
//using Microsoft.Extensions.PlatformAbstractions;  // For ApiControllerAttribute Compatibility Setting
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

namespace FoodTruckNationApi
{
    public class Startup
    {

        //private readonly IConfigurationRoot configuration;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ILoggerFactory loggerFactory;

        public Startup(IHostingEnvironment env, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            // Hold onto a reference to the hosting environment and the logger factory
            this.hostingEnvironment = env;
            this.loggerFactory = loggerFactory;

            this.Configuration = configuration;

            
            // For NLog            
            env.ConfigureNLog("nlog.config");
        }



        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new ValidationAttribute());
                options.Filters.Add(new ExceptionHandlerFilterAttribute(this.loggerFactory));
                
            })            
            // .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)  // Uncomment to use ApiController Attribute
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddCors();
            this.ConfigureServicesDI(services);
            services.AddAutoMapper();
            this.ConfigureServicesVersioning(services);
            this.ConfigureServicesSwagger(services);

            var memoryThreshhold = 800000000; // 800 MB
            var healthBuilder = new HealthBuilder()
                // Check that the current amount of physical memory in bytes is below a threshold
                .HealthChecks.AddProcessPhysicalMemoryCheck("Working Set", memoryThreshhold)
                // Check connectivity to google with a "ping", passes if the result is `IPStatus.Success`
                .HealthChecks.AddPingCheck("google ping", "google.com", TimeSpan.FromSeconds(10))
                // Check that our SQL Server is still up and running
                .HealthChecks.AddSqlCheck("Food Truck Database", 
                    this.Configuration.GetConnectionString("FoodTruckConnectionString"), TimeSpan.FromSeconds(10))
                .OutputHealth.AsJson()
                .BuildAndAddTo(services);
            services.AddHealthEndpoints();
        }





        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();

            // Shows UseCors with CorsPolicyBuilder.
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
            services.AddSingleton<IConfiguration>(this.Configuration);

            if (this.hostingEnvironment.EnvironmentName == "IntegrationTests")
            {
                var testDbName = this.Configuration["TestName"];
                services.ConfigureInMemoryDataAccess(testDbName);
            }
            else
            {
                var connectionString = this.Configuration.GetConnectionString("FoodTruckConnectionString");
                services.ConfigureSqlServerDataAccess(connectionString, loggerFactory);
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
            var pathToXmlComments = Configuration["Swagger:FileName"];
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
