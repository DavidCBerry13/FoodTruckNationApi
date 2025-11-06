using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using AutoMapper;
using DavidBerry.Framework.ApiUtil.Controllers;
using DavidBerry.Framework.ApiUtil.Models;
using DavidBerry.Framework.Functional;
using DavidBerry.Framework.TimeAndDate;
using FoodTruckApi.FoodTrucks.Schedules.Models;
using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;
using FoodTruckNation.Core.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FoodTruckNationApi.FoodTrucks.Schedules
{

    /// <summary>
    /// Controller containing actions related to the schedules (appointments) for a food truck
    /// </summary>
    [Produces("application/json")]
    [Route("api/FoodTrucks/{foodTruckId}/Schedules")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class FoodTruckSchedulesController : ApiControllerBase
    {
        /// <summary>
        /// Constructs a FoodTruckSchedulesController object
        /// </summary>
        /// <param name="logger">An ILogger to use by the controller</param>
        /// <param name="mapper">An IMapper used by this controller to map between model and entity objects</param>
        /// <param name="scheduleService">An iScheduleService object used to read/create/update food truck schedules</param>
        /// <param name="dateTimeProvider">An iDateTimeProver for handling date/time functions</param>
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
        public async Task<IActionResult> Get(int foodTruckId, [FromQuery]FoodTruckScheduleParameters parameters)
        {
            if (!parameters.StartDate.HasValue)
                parameters.StartDate = _dateTimeProvider.CurrentDateTime.Date;

            if (!parameters.EndDate.HasValue)
                parameters.EndDate = _dateTimeProvider.CurrentDateTime.AddDays(7).Date;

            var result = await _scheduleService.GetSchedulesForFoodTruckAsync(foodTruckId, parameters.StartDate.Value, parameters.EndDate.Value);
            return CreateResponse<IEnumerable<Schedule>, IEnumerable<FoodTruckScheduleModel>>(result);
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
        public async Task<IActionResult> Get(int foodTruckId, int scheduleId)
        {
            var result = await _scheduleService.GetScheduleAsync(foodTruckId, scheduleId);
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
        public async Task<IActionResult> Post(int foodTruckId, [FromBody]CreateFoodTruckScheduleModel createModel)
        {
            var createCommand = new CreateFoodTruckScheduleCommand() { FoodTruckId = foodTruckId };
            _mapper.Map<CreateFoodTruckScheduleModel, CreateFoodTruckScheduleCommand>(createModel, createCommand);

            var result = await _scheduleService.AddFoodTruckScheduleAsync(createCommand);

            return CreateResponse<Schedule, FoodTruckScheduleModel>(result,
                (schedule) =>
                {
                    var model = _mapper.Map<Schedule, FoodTruckScheduleModel>(schedule);
                    return CreatedAtRoute(GET_SINGLE_FOOD_TRUCK_SCHEDULE,
                        new { foodTruckId = model.FoodTruckId, scheduleId = model.ScheduleId }, model);
                });
        }

        /// <summary>
        /// Updates the schedule of a specific schedule with the provided details.
        /// </summary>
        /// <remarks>This method maps the provided <paramref name="updateModel"/> to a command object and
        /// invokes the schedule service to perform the update. If the update is successful, the method returns a
        /// response with the updated schedule and a location header pointing to the resource.</remarks>
        /// <param name="foodTruckId">The unique identifier of the food truck containing the schedule to be updated.</param>
        /// <param name="scheduleId">The unique identifier of the schedule to update.</param>
        /// <param name="updateModel">The model containing the updated schedule details.</param>
        /// <returns>An <see cref="ActionResult{T}"/> containing the updated <see cref="Schedule"/> object if the update is
        /// successful.</returns>
        [HttpPut("{scheduleId}")]
        public async Task<ActionResult<Schedule>> Put(int foodTruckId, int scheduleId, [FromBody]UpdateFoodTruckScheduleModel updateModel)
        {
            // Populate the command object from the incoming urls parametrs and model
            var updateCommand = new UpdateFoodTruckScheduleCommand()
            {
                FoodTruckId = foodTruckId,
                ScheduleId = scheduleId,
                StartTime = updateModel.StartTime,
                EndTime = updateModel.EndTime
            };

            // Call the service to update the schedule
            var result = await _scheduleService.UpdateFoodTruckScheduleAsync(updateCommand);
            return CreateResponse<Schedule, FoodTruckScheduleModel>(result,
                (schedule) =>
                {
                    var model = _mapper.Map<Schedule, FoodTruckScheduleModel>(schedule);
                    return CreatedAtRoute(GET_SINGLE_FOOD_TRUCK_SCHEDULE,
                    new { foodTruckId = model.FoodTruckId, scheduleId = model.ScheduleId }, model);
                });
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
        public async Task<IActionResult> Delete(int foodTruckId, int scheduleId)
        {
            var result = await _scheduleService.DeleteFoodTruckScheduleAsync(foodTruckId, scheduleId);

            return ( result.IsSuccess )
                ? Ok(new ApiMessageModel() { Message = $"Schedule {scheduleId} has been deleted for food truck {foodTruckId}" })
                : MapErrorResult(result);
        }

        /// <summary>
        /// Overrides the MapErrorResult method to handle SchedulingConflictError errors specifically
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        [NonAction]
        protected override ActionResult MapErrorResult<TEntity, TModel>(Result result)
        {
            switch (result.Error)
            {
                case SchedulingConflictError error:
                    return UnprocessableEntity(new ScheduleConflictMessageModel() {
                        Message = error.Message,
                        ConflictingSchedules = _mapper.Map<List<Schedule>, List<FoodTruckScheduleModel>>(error.ConflictingSchedules)
                    });
                default:
                    return base.MapErrorResult<TEntity, TModel>(result);
            }
        }

    }

}
