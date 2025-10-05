using FoodTruckNation.Core.DataInterfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Core.AppServices
{

    /// <summary>
    /// Serves as the base service class for all services
    /// </summary>
    public abstract class BaseService
    {

        public BaseService(ILoggerFactory loggerFactory, IFoodTruckDatabase foodTruckDatabase)
        {
            Logger = loggerFactory.CreateLogger(GetType());
            FoodTruckDatabase = foodTruckDatabase;
        }


        protected ILogger Logger { get; private set; }
        protected IFoodTruckDatabase FoodTruckDatabase { get; init; }


    }
}
