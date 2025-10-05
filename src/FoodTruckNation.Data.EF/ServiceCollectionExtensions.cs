using FoodTruckNation.Core.DataInterfaces;
using FoodTruckNation.Data.EF.Repositories;
using DavidBerry.Framework.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Data.EF
{
    public static class ServiceCollectionExtensions
    {


        public static void ConfigureSqlServerDataAccess(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<FoodTruckContext>(options => options
                .UseSqlServer(connectionString)
            );

            services.AddScoped<IFoodTruckDatabase, FoodTruckDatabase>();
        }



        public static void ConfigureSqlLiteDatabase(this IServiceCollection services, string connectionString)
        {
            // Configures the contest
            var connection = new SqliteConnection(connectionString);
            connection.Open();

            services.AddDbContext<FoodTruckContext>(options =>
                options.UseSqlite(connection)
            );

            // Make sure the database exists
            var builder = new DbContextOptionsBuilder<FoodTruckContext>();
            builder.UseSqlite(connection);

            using (var context = new FoodTruckContext(builder.Options))
            {
                context.Database.EnsureCreated();
            }

            services.AddScoped<IFoodTruckDatabase, FoodTruckDatabase>();
        }




        //private static void ConfigureRepositories(IServiceCollection services)
        //{
        //    services.AddScoped<IUnitOfWork, BaseEfDatabase<FoodTruckContext>>();

        //    services.AddScoped<IFoodTruckRepository, FoodTruckRepository>();
        //    services.AddScoped<ITagRepository, TagRepository>();
        //    services.AddScoped<ILocalityRepository, LocalityRepository>();
        //    services.AddScoped<ILocationRepository, LocationRepository>();
        //    services.AddScoped<IScheduleRepository, ScheduleRepository>();
        //    services.AddScoped<ISocialMediaPlatformRepository, SocialMediaPlatformRepository>();
        //}


    }
}
