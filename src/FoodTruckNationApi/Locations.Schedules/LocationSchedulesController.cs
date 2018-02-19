using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Framework.ApiUtil.Controllers;
using FoodTruckNation.Core.AppInterfaces;
using Framework;
using Microsoft.Extensions.Logging;
using AutoMapper;
using FoodTruckNation.Core.Domain;
using FoodTruckNationApi.Locations.Schedules.Get;

namespace FoodTruckNationApi.Locations.Schedules
{

    /// <summary>
    /// API Endpoints related to the schedules of food trucks at a given location
    /// </summary>
    [Produces("application/json")]
    [Route("api/Locations/{locationId}/Schedules")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class LocationSchedulesController : BaseController
    {

        public LocationSchedulesController(ILogger<LocationSchedulesController> logger, IMapper mapper,
            IScheduleService scheduleService, IDateTimeProvider dateTimeProvider)
            : base(logger, mapper)
        {
            this.dateTimeProvider = dateTimeProvider;
            this.scheduleService = scheduleService;
        }

        private IDateTimeProvider dateTimeProvider;
        private IScheduleService scheduleService;

        #region Route Name Constants

        public const String GET_ALL_SCHEDULES_FOR_LOCATION = "GetAllSchedulesForLocation";

        #endregion

        /// <summary>
        /// Gets all of the food trucks scheduled at a location for a given date range
        /// </summary>
        /// <remarks>
        /// If no date range data is provided, then this enpoint will use a date range of the next seven days
        /// </remarks>
        /// <param name="locationId">The id number of the location</param>
        /// <param name="parameters">An optional date range to get the scheduled food trucks for</param>
        /// <returns></returns>
        [HttpGet(Name=GET_ALL_SCHEDULES_FOR_LOCATION)]
        public IActionResult Get(int locationId, GetLocationSchedulesParameters parameters)
        {
            if (!parameters.StartDate.HasValue)
                parameters.StartDate = this.dateTimeProvider.CurrentDateTime.Date;

            if (!parameters.EndDate.HasValue)
                parameters.EndDate = parameters.StartDate.Value.AddDays(7).Date;

            List<Schedule> schedules = scheduleService.GetSchedulesForLocation(locationId,
                parameters.StartDate.Value, parameters.EndDate.Value);

            List<LocationScheduleModel> models = this.mapper.Map<List<Schedule>, List<LocationScheduleModel>>(schedules);

            return Ok(models);
        }
        
    }
}
