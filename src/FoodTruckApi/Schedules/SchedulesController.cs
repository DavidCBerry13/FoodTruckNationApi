using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DavidBerry.Framework.ApiUtil.Controllers;
using Microsoft.Extensions.Logging;
using FoodTruckNation.Core.AppInterfaces;
using AutoMapper;
using FoodTruckNation.Core.Domain;
using DavidBerry.Framework.TimeAndDate;
using Asp.Versioning;

namespace FoodTruckNationApi.Schedules
{

    /// <summary>
    /// API Controller allowing you to get the schedules for all food trucks in a given date range
    /// </summary>
    [Produces("application/json")]
    [Route("api/Schedules")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class SchedulesController : ApiControllerBase
    {

        public SchedulesController(ILogger<SchedulesController> logger, IMapper mapper,
            IScheduleService scheduleService, IDateTimeProvider dateTimeProvider) : base(logger, mapper)
        {
            _scheduleService = scheduleService;
            _dateTimeProvider = dateTimeProvider;
        }


        private readonly IScheduleService _scheduleService;
        private readonly IDateTimeProvider _dateTimeProvider;

        /// <summary>
        /// Gets a list of food truck schedules (appointments) for all food trucks for
        /// the provided date range.  If no date range is provided, then the next seven
        /// days is the default date range used
        /// </summary>
        /// <param name="parameters">A GetSchedulesParameters object that encapsulates the query string arguments to this method, mainly, the start and end dates if any were included</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ScheduleModel> Get([FromQuery]GetSchedulesParameters parameters)
        {
            var startDate = parameters.StartDate ?? _dateTimeProvider.CurrentDateTime.Date; ;
            var endDate = parameters.EndDate ?? _dateTimeProvider.CurrentDateTime.Date.AddDays(7);

            var result = _scheduleService.GetSchedules(startDate, endDate);
            return CreateResponse<List<Schedule>, List<ScheduleModel>>(result);

        }




    }
}
