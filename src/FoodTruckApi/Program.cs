using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Asp.Versioning;
using DavidBerry.Framework.ApiUtil;
using DavidBerry.Framework.ApiUtil.Util;
using DavidBerry.Framework.TimeAndDate;
using FluentValidation;
using FoodTruckNation.Core;
using FoodTruckNation.Data.EF;
using FoodTruckNationApi.FoodTrucks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NLog;
using NLog.Web;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FoodTruckApi
{

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Program
    {

        public static void Main(string[] args)
        {
            // Early init of NLog to allow startup and exception logging, before host is built
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init main");

            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Services.AddControllers();
                builder.Services.AddProblemDetails();

                // For API Versioning
                builder.Services.AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                    options.ApiVersionReader = new HeaderApiVersionReader("api-version");
                })
                .AddMvc()
                .AddApiExplorer(
                    options =>
                    {
                        // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                        // note: the specified format code will format the version as "'v'major[.minor][-status]"
                        options.GroupNameFormat = "'v'VVV";
                    });


                builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
                builder.Services.AddSwaggerGen(
                    options =>
                    {
                        // add a custom operation filter which sets default values
                        options.OperationFilter<SwaggerDefaultValues>();

                        var fileName = typeof(Program).Assembly.GetName().Name + ".xml";
                        var filePath = Path.Combine(AppContext.BaseDirectory, fileName);

                        // integrate xml comments
                        options.IncludeXmlComments(filePath);

                        options.CustomSchemaIds(x => x.FullName.Replace("+", "."));
                    });

                // Add services to the container.

                builder.Services.AddValidatorsFromAssemblyContaining<CreateFoodTruckModelValidator>();  // Fluent Validation
                builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(UrlResolver)));  // AutoMapper
                builder.Services.AddCors();  // For CORS

                var connectionString = builder.Configuration.GetConnectionString("FoodTruckConnectionString");
                builder.Services.ConfigureSqlServerDataAccess(connectionString);
                builder.Services.ConfigureFoodTruckServices();
                builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                builder.Services.AddSingleton<IDateTimeProvider, StandardDateTimeProvider>();

                var app = builder.Build();


                app.UseSwagger();
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwaggerUI(
                       options =>
                       {
                           var descriptions = app.DescribeApiVersions();

                           // build a swagger endpoint for each discovered API version
                           foreach (var description in descriptions)
                           {
                               var url = $"/swagger/{description.GroupName}/swagger.json";
                               var name = description.GroupName.ToUpperInvariant();
                               options.SwaggerEndpoint(url, name);
                           }
                       });
                }

                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.MapControllers();

                app.Run();
            }
            catch (Exception exception)
            {
                // NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }


    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

}
