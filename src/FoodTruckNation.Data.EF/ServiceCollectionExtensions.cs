using FoodTruckNation.Core.DataInterfaces;
using FoodTruckNation.Data.EF.Repositories;
using Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Data.EF
{
    public static class ServiceCollectionExtensions
    {


        public static void ConfigureSqlServerDataAccess(this IServiceCollection services, String connectionString, 
            ILoggerFactory loggerFactory)
        {
            services.AddDbContext<FoodTruckContext>(options => options
                .UseSqlServer(connectionString)
                .UseLoggerFactory(loggerFactory)
            );


            ConfigureRepositories(services);
        }


        public static void ConfigureInMemoryDataAccess(this IServiceCollection services, String testDbName)
        {
            services.AddDbContext<FoodTruckContext>(options =>
                options.UseInMemoryDatabase(databaseName: testDbName)
            );

            ConfigureRepositories(services);
        }



        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EfUnitOfWork<FoodTruckContext>>();

            services.AddScoped<IFoodTruckRepository, FoodTruckRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<ISocialMediaPlatformRepository, SocialMediaPlatformRepository>();
        }


    }
}
