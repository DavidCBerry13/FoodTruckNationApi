using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.AppServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Core
{
    public static class ServiceCollectionExtensions
    {


        public static void ConfigureFoodTruckServices(this IServiceCollection services)
        {
            services.AddScoped<IFoodTruckService, FoodTruckService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<ISocialMediaPlatformService, SocialMediaPlatformService>();
        }

    }
}
