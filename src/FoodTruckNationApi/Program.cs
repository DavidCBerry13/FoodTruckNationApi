#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace FoodTruckNationApi
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingEnv, configBuilder) =>
                    configBuilder.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{hostingEnv.HostingEnvironment.EnvironmentName}.json", optional: true)
                        .AddEnvironmentVariables().Build()            
                )
                //.UseSetting("detailedErrors", "true")
                .UseIISIntegration()
                .UseStartup<Startup>()
                //.CaptureStartupErrors(true)
                .Build();

            host.Run();
        }
    }
}

#pragma warning restore 1591