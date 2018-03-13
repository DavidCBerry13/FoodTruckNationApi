using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FoodTruckNationApi.Tests.Integration
{
    public static class TestUtil
    {

        public static TestServer CreateTestServer(String testName)
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("IntegrationTests")
                .ConfigureAppConfiguration((hostingEnv, configBuilder) =>
                    configBuilder.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.IntegrationTests.json", optional: false, reloadOnChange: false)
                    .AddInMemoryCollection(new KeyValuePair<String, String>[] { new KeyValuePair<string, string>("TestName", testName) })
                .Build()
            )
            .UseStartup<Startup>());

            return server;
        }

        public static T GetDbContext<T>(this TestServer testServer) where T : DbContext
        {
            var context = testServer.Host.Services.GetService(typeof(T)) as T;
            return context;

        }

    }
}
