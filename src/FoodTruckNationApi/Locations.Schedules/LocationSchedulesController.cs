using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Framework.ApiUtil.Controllers;
using FoodTruckNation.Core.AppInterfaces;
using Framework;
using Microsoft.Extensions.Logging;
using AutoMapper;
using FoodTruckNation.Core.Domain;

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
        /// <summary>
        /// Creates a LocationSchedulesController, the controller responsible for returning schedules
        /// (appointments) for a specific location
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="scheduleService"></param>
        /// <param name="dateTimeProvider"></param>
        public LocationSchedulesController(ILogger<LocationSchedulesController> logger, IMapper mapper,
            IScheduleService scheduleService, IDateTimeProvider dateTimeProvider)
            : base(logger, mapper)
        {
            _dateTimeProvider = dateTimeProvider;
            _scheduleService = scheduleService;
        }

        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IScheduleService _scheduleService;

        #region Route Name Constants

        internal const string GET_ALL_SCHEDULES_FOR_LOCATION = "GetAllSchedulesForLocation";

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
                parameters.StartDate = _dateTimeProvider.CurrentDateTime.Date;

            if (!parameters.EndDate.HasValue)
                parameters.EndDate = parameters.StartDate.Value.AddDays(7).Date;

            List<Schedule> schedules = _scheduleService.GetSchedulesForLocation(locationId,
                parameters.StartDate.Value, parameters.EndDate.Value);

            List<LocationScheduleModel> models = _mapper.Map<List<Schedule>, List<LocationScheduleModel>>(schedules);

            return Ok(models);
        }
        
    }
}
