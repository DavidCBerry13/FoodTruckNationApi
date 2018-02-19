using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodTruckNationApi.Schedules.Base.Get;
using Framework.ApiUtil.Controllers;
using Microsoft.Extensions.Logging;
using FoodTruckNation.Core.AppInterfaces;
using AutoMapper;
using FoodTruckNation.Core.Domain;

namespace FoodTruckNationApi.Schedules
{
    [Produces("application/json")]
    [Route("api/Schedules")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class SchedulesController : BaseController
    {

        public SchedulesController(ILogger<SchedulesController> logger, IMapper mapper, 
            IScheduleService scheduleService) : base(logger, mapper)
        {
            _scheduleService = scheduleService;
        }


        private IScheduleService _scheduleService;

        // GET: api/Schedules
        [HttpGet]
        public IEnumerable<ScheduleModel> Get([FromQuery]GetSchedulesParameters parameters)
        {
            var startDate = parameters.StartDate.HasValue ? parameters.StartDate.Value : DateTime.Today;
            var endDate = parameters.EndDate.HasValue ? parameters.StartDate.Value : DateTime.Today.AddDays(7);

            var schedules = this._scheduleService.GetSchedules(startDate, endDate);
            var scheduleModels = this.mapper.Map<List<Schedule>, List<ScheduleModel>>(schedules);
            return scheduleModels;
        }


        
        
    }
}
