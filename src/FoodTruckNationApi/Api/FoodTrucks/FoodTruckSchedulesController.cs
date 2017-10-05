using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Framework.ApiUtil.Controllers;
using Microsoft.Extensions.Logging;
using AutoMapper;
using FoodTruckNation.Core.AppInterfaces;
using Framework;
using FoodTruckNation.Core.AppServices;
using FoodTruckNation.Core.Domain;
using FoodTruckNationApi.ApiModels;
using FoodTruckNation.Core.Commands;
using Framework.ApiUtil.Models;
using FoodTruckNationApi.Api.FoodTrucks.Models;
using FoodTruckNationApi.Api.Locations.Models;

namespace FoodTruckNationApi.Controllers
{
    [Produces("application/json")]
    [Route("api/FoodTrucks/{foodTruckId}/Schedules")]
    [ApiVersion("1.0")]
    public class FoodTruckSchedulesController : BaseController
    {

        public FoodTruckSchedulesController(ILogger<FoodTruckSchedulesController> logger, IMapper mapper,
            IScheduleService scheduleService, IDateTimeProvider dateTimeProvider)
            : base(logger, mapper)
        {
            this.dateTimeProvider = dateTimeProvider;
            this.scheduleService = scheduleService;
        }

        private IDateTimeProvider dateTimeProvider;
        private IScheduleService scheduleService;


        #region Route Name Constants

        public const String GET_FOOD_TRUCK_SCHEDULE = "GetFoodTruckSchedule";


        public const String GET_SINGLE_FOOD_TRUCK_SCHEDULE = "GetFoodTruckScheduleById";


        #endregion


        /// <summary>
        /// Gets all the Schedules (appointments) for a Food Truck in the given date range
        /// </summary>
        /// <param name="foodTruckId">An int of the food truck id</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        /// <response code="200">Success.  A list of location schedule objects of what food trucks are scheduled at this location are returned</response>
        /// <response code="404">Not Found.  The requested location id could not be found</response>
        /// <response code="500">Internal Server Error.  An unexpected error occured.  This error has been logged so support personel can troubleshoot the problem</response>
        [ProducesResponseType(typeof(List<FoodTruckScheduleModel>), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        [HttpGet(Name = GET_FOOD_TRUCK_SCHEDULE)]
        public IActionResult Get(int foodTruckId, [FromQuery]DateRangeModel dateRange)
        {
            if (!dateRange.StartDate.HasValue)
                dateRange.StartDate = this.dateTimeProvider.CurrentDateTime.Date;

            if (!dateRange.EndDate.HasValue)
                dateRange.EndDate = this.dateTimeProvider.CurrentDateTime.AddDays(7).Date;


            var schedules = this.scheduleService.GetSchedulesForFoodTruck(foodTruckId, dateRange.StartDate.Value, dateRange.EndDate.Value);
            var models = mapper.Map<List<Schedule>, List<FoodTruckScheduleModel>>(schedules);

            return Ok(models);
        }


        // GET: api/FoodTruckSchedules/5
        [HttpGet("{scheduleId}", Name = GET_SINGLE_FOOD_TRUCK_SCHEDULE)]
        public IActionResult Get(int foodTruckId, int scheduleId)
        {
            var schedule = this.scheduleService.GetSchedule(foodTruckId, scheduleId);
            var model = mapper.Map<Schedule, FoodTruckScheduleModel>(schedule);

            return Ok(model);
        }
        
        // POST: api/FoodTruckSchedules
        [HttpPost]
        public IActionResult Post(int foodTruckId, [FromBody]CreateFoodTruckScheduleModel createModel)
        {
            var createCommand = new CreateFoodTruckScheduleCommand() { FoodTruckId = foodTruckId };
            this.mapper.Map<CreateFoodTruckScheduleModel, CreateFoodTruckScheduleCommand>(createModel, createCommand);

            Schedule schedule = this.scheduleService.AddFoodTruckSchedule(createCommand);

            var model = this.mapper.Map<Schedule, FoodTruckScheduleModel>(schedule);
            return this.CreatedAtRoute(GET_SINGLE_FOOD_TRUCK_SCHEDULE,
                new { foodTruckId = model.FoodTruckId, scheduleId = model.ScheduleId }, model);           
        }

        // PUT: api/FoodTruckSchedules/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// Deleted the given schedule for the food truck
        /// </summary>
        /// <param name="foodTruckId">The id of the food truck the schule is for</param>
        /// <param name="scheduleId">The id of the schedule</param>
        /// <returns></returns>
        /// <response code="200">Success.  A message is returned to confirm the deletion</response>
        /// <response code="404">Not Found.  Either the food truck of the schedule could not be found (the message will indicate which one)</response>
        /// <response code="500">Internal Server Error.  An unexpected error occured.  This error has been logged so support personel can troubleshoot the problem</response>
        [HttpDelete("{scheduleId}")]
        [ProducesResponseType(typeof(ApiMessageModel), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public IActionResult Delete(int foodTruckId, int scheduleId)
        {
            this.scheduleService.DeleteFoodTruckSchedule(foodTruckId, scheduleId);

            return Ok(new ApiMessageModel() { Message = "Schedule was deleted" } );
        }
    }
}
