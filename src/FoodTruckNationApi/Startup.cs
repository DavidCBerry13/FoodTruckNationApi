using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using NLog.Extensions.Logging;
using FoodTruckNation.Data.EF;
using FoodTruckNation.Core.DataInterfaces;
using FoodTruckNation.Data.EF.Repositories;
using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.AppServices;
using Framework;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Framework.ApiUtil.Filters;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace FoodTruckNationApi
{
    public class Startup
    {

        private IConfigurationRoot configuration;
        private IHostingEnvironment hostingEnvironment;
        private ILoggerFactory loggerFactory;

        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Hold onto a reference to the hosting environment and the logger factory
            this.hostingEnvironment = env;
            this.loggerFactory = loggerFactory;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            configuration = Configuration = builder.Build();
            
            // For NLog            
            env.ConfigureNLog("nlog.config");
        }



        public IConfigurationRoot Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new ValidationAttribute());
                options.Filters.Add(new ExceptionHandlerFilterAttribute(this.loggerFactory));
            });

            services.AddCors();
            this.ConfigureServicesDI(services);
            services.AddAutoMapper();
            this.ConfigureServicesVersioning(services);
            this.ConfigureServicesSwagger(services);
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

            app.UseSwagger(c =>
            {                
                c.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Value);
            });
            
            app.UseSwaggerUI(c =>
            { 
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Docs");
            });

        }



        #region Configure Services Helpers

        /// <summary>
        /// Helper method where all the dependency injection is configured
        /// </summary>
        /// <remarks>
        /// This code is split out into a helper method to keep the ConfigureServices method from getting too large.
        /// Having a separate helper method helps to keep each helper tightly focused on one job as well.
        /// </remarks>
        /// <param name="services"></param>
        private void ConfigureServicesDI(IServiceCollection services)
        {
            services.AddSingleton<IConfigurationRoot>(this.configuration);

            services.AddDbContext<FoodTruckContext>(options => options
                .UseSqlServer(this.configuration.GetConnectionString("FoodTruckConnectionString"))
                .UseLoggerFactory(this.loggerFactory)
                );

            services.AddScoped<IUnitOfWork, EfUnitOfWork<FoodTruckContext>>();

            services.AddScoped<IFoodTruckRepository, FoodTruckRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<ISocialMediaPlatformRepository, SocialMediaPlatformRepository>();

            services.AddScoped<IFoodTruckService, FoodTruckService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<ISocialMediaPlatformService, SocialMediaPlatformService>();


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
                options.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Food Truck API",
                        Description = "A demonstration api built around tracking food trucks and their schedules",
                        TermsOfService = "For demonstration only"
                    }
                );
                
                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, pathToXmlComments);
                options.IncludeXmlComments(filePath);
                options.DescribeAllEnumsAsStrings();


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
