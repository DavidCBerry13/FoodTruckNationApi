using Framework;
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

        public BaseService(ILoggerFactory loggerFactory, IUnitOfWork uow)
        {
            Logger = loggerFactory.CreateLogger(GetType());
            UnitOfWork = uow;
        }


        protected ILogger Logger { get; private set; }
        protected IUnitOfWork UnitOfWork { get; private set; }


    }
}
