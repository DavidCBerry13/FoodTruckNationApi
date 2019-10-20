using FoodTruckNation.Data.EF;
using Framework.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace FoodTruckNationApi.Tests.Integration
{
    public static class TestUtil
    {

        public static TestServer CreateTestServer()
        {
            // Solves problem of Automapper getting re-initialized for each test run (which throws exception)
            // This is only for use in test scenarios per AutoMapper doc
            AutoMapper.Mapper.Reset();

            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("IntegrationTests")
                .ConfigureAppConfiguration((hostingEnv, configBuilder) =>
                    configBuilder.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.IntegrationTests.json", optional: false, reloadOnChange: false)                    
                    .Build()
            )
            .UseStartup<Startup>());

            // Seed the database
            // This gets the SQL of the initial database state
            var initialDataSql = Assembly.GetExecutingAssembly().ReadEmbeddedResourceTextFile("DatabaseData.sql");
            var dbContext = GetDbContext<FoodTruckContext>(server);
            dbContext.Database.ExecuteSqlCommand(initialDataSql);

            return server;
        }

        public static T GetDbContext<T>(this TestServer testServer) where T : DbContext
        {
            var context = testServer.Host.Services.GetService(typeof(T)) as T;
            return context;
        }





    }
}
