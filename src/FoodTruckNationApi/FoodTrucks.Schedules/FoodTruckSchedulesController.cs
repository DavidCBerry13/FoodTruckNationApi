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
using Framework.Utility;
using FoodTruckNation.Core.Domain;
using FoodTruckNation.Core.Commands;
using Framework.ApiUtil.Models;

namespace FoodTruckNationApi.FoodTrucks.Schedules
{
    [Produces("application/json")]
    [Route("api/FoodTrucks/{foodTruckId}/Schedules")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class FoodTruckSchedulesController : ApiControllerBase
    {

        public FoodTruckSchedulesController(ILogger<FoodTruckSchedulesController> logger, IMapper mapper,
            IScheduleService scheduleService, IDateTimeProvider dateTimeProvider)
            : base(logger, mapper)
        {
            _dateTimeProvider = dateTimeProvider;
            _scheduleService = scheduleService;
        }

        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IScheduleService _scheduleService;


        #region Route Name Constants

        internal const string GET_FOOD_TRUCK_SCHEDULE = "GetFoodTruckSchedule";


        internal const string GET_SINGLE_FOOD_TRUCK_SCHEDULE = "GetFoodTruckScheduleById";


        #endregion


        /// <summary>
        /// Gets all the Schedules (appointments) for a Food Truck in the given date range
        /// </summary>
        /// <param name="foodTruckId">An int of the food truck id</param>
        /// <param name="parameters">A FoodTruckScheduleParameters object that encapsulates the optional parameters that can be passed to this action (example the start and end date)</param>
        /// <returns></returns>
        /// <response code="200">Success.  A list of location schedule objects of what food trucks are scheduled at this location are returned</response>
        /// <response code="404">Not Found.  The requested food truck id could not be found</response>
        /// <response code="500">Internal Server Error.  An unexpected error occured.  This error has been logged so support personel can troubleshoot the problem</response>
        [ProducesResponseType(typeof(List<FoodTruckScheduleModel>), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        [HttpGet(Name = GET_FOOD_TRUCK_SCHEDULE)]
        public IActionResult Get(int foodTruckId, [FromQuery]FoodTruckScheduleParameters parameters)
        {
            if (!parameters.StartDate.HasValue)
                parameters.StartDate = _dateTimeProvider.CurrentDateTime.Date;

            if (!parameters.EndDate.HasValue)
                parameters.EndDate = _dateTimeProvider.CurrentDateTime.AddDays(7).Date;


            var result = _scheduleService.GetSchedulesForFoodTruck(foodTruckId, parameters.StartDate.Value, parameters.EndDate.Value);
            return CreateResponse<List<Schedule>, List<FoodTruckScheduleModel>>(result);
        }


        /// <summary>
        /// Gets an individual schedule (appointment) for a food truck
        /// </summary>
        /// <param name="foodTruckId">An int of the food truck id the schedule is for</param>
        /// <param name="scheduleId">An int of the unique id of the schedule</param>
        /// <returns></returns>
        /// <response code="200">Success.  A location schedule object of of an individual appointment for this food truck</response>
        /// <response code="404">Not Found.  The requested food truck id could not be found or the individual schedule could not be found</response>
        /// <response code="500">Internal Server Error.  An unexpected error occured.  This error has been logged so support personel can troubleshoot the problem</response>
        [ProducesResponseType(typeof(List<FoodTruckScheduleModel>), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        [HttpGet("{scheduleId}", Name = GET_SINGLE_FOOD_TRUCK_SCHEDULE)]
        public IActionResult Get(int foodTruckId, int scheduleId)
        {
            var result = _scheduleService.GetSchedule(foodTruckId, scheduleId);
            return CreateResponse<Schedule, FoodTruckScheduleModel>(result);
        }

        /// <summary>
        /// Creates a new schedule (appointment) for a food truck
        /// </summary>
        /// <param name="foodTruckId">An int of the id of the food truck to create the schedule for</param>
        /// <param name="createModel">The data required to create the new schedule for the food truck</param>
        /// <returns></returns>
        /// <response code="201">Success.  A new schedule was created and has been returned</response>
        /// <response code="404">Not Found.  The requested food truck id could not be found</response>
        /// <response code="500">Internal Server Error.  An unexpected error occured.  This error has been logged so support personel can troubleshoot the problem</response>
        [ProducesResponseType(typeof(List<FoodTruckScheduleModel>), 201)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        [HttpPost]
        public IActionResult Post(int foodTruckId, [FromBody]CreateFoodTruckScheduleModel createModel)
        {
            var createCommand = new CreateFoodTruckScheduleCommand() { FoodTruckId = foodTruckId };
            _mapper.Map<CreateFoodTruckScheduleModel, CreateFoodTruckScheduleCommand>(createModel, createCommand);

            var result = _scheduleService.AddFoodTruckSchedule(createCommand);

            return CreateResponse<Schedule, FoodTruckScheduleModel>(result,
                (schedule) =>
                {
                    var model = _mapper.Map<Schedule, FoodTruckScheduleModel>(schedule);
                    return CreatedAtRoute(GET_SINGLE_FOOD_TRUCK_SCHEDULE,
                        new { foodTruckId = model.FoodTruckId, scheduleId = model.ScheduleId }, model);
                });        
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
            var result = _scheduleService.DeleteFoodTruckSchedule(foodTruckId, scheduleId);

            return ( result.IsSuccess )
                ? Ok(new ApiMessageModel() { Message = $"Schedule {scheduleId} has been deleted for food truck {foodTruckId}" })
                : MapErrorResult(result);
        }
    }
}
