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

namespace FoodTruckNationApi.Api.Locations.Schedules
{
    [Produces("application/json")]
    [Route("api/Locations/{locationId}/Schedules")]
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
        /// <param name="dateRange">An optional date range to get the scheduled food trucks for</param>
        /// <returns></returns>
        [HttpGet(Name=GET_ALL_SCHEDULES_FOR_LOCATION)]
        public IActionResult Get(int locationId, DateRangeModel dateRange)
        {
            if (!dateRange.StartDate.HasValue)
                dateRange.StartDate = this.dateTimeProvider.CurrentDateTime.Date;

            if (!dateRange.EndDate.HasValue)
                dateRange.EndDate = dateRange.StartDate.Value.AddDays(7).Date;

            List<Schedule> schedules = scheduleService.GetSchedulesForLocation(locationId,
                dateRange.StartDate.Value, dateRange.EndDate.Value);

            List<LocationScheduleModel> models = this.mapper.Map<List<Schedule>, List<LocationScheduleModel>>(schedules);

            return Ok(models);
        }
        
    }
}
