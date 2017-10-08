using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FoodTruckNation.Core.Domain;
using Microsoft.Extensions.Logging;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.AppInterfaces;
using AutoMapper;
using Framework.ApiUtil.Models;
using Framework.ApiUtil.Controllers;

namespace FoodTruckNationApi.Api.FoodTrucks
{

    /// <summary>
    /// Controller for the API endpoints related to food trucks.  Only endpoints on the core food truck object are contained
    /// in this controller.  Child objects have their own controllers that are of the form FoodTruck{Child Object}Contoller,
    /// for example, FoodTruckReviewsController
    /// </summary>
    [Produces("application/json")]
    [Route("api/FoodTrucks")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class FoodTrucksController : BaseController
    {

        /// <summary>
        /// Creates a new FoodTruckController
        /// </summary>
        /// <param name="logger">An ILogger object used to for any logging inside of this controller</param>
        /// <param name="mapper">An Automapper IMapper object used for object mapping within this controller</param>
        /// <param name="foodTruckService">An IFoodTruckService object that contains the business logic for food truck functions</param>
        public FoodTrucksController(ILogger<FoodTrucksController> logger, IMapper mapper, IFoodTruckService foodTruckService)
            : base(logger, mapper)
        {
            this.foodTruckService = foodTruckService;            
        }


        private IFoodTruckService foodTruckService;


        #region Route Constants

        /// <summary>
        /// Route Name Constant for route that will get all food trucks
        /// </summary>
        public const String GET_ALL_FOOD_TRUCKS = "GetFoodTrucks";

        /// <summary>
        /// Route name constant for route that gets an individual food truck
        /// </summary>
        public const String GET_FOOD_TRUCK_BY_ID = "GetFoodTruckById";

        /// <summary>
        /// Route name constant for route that creates a new Food Truck
        /// </summary>
        public const String CREATE_FOOD_TRUCK = "CreatFoodTruck";


        #endregion




        /// <summary>
        /// Gets a list of all food trucks in the system.  Optionally, a tag value can be provided that will return only food trucks tagged with the given tag
        /// </summary>
        /// <param name="tag">An optional tag to filter the food trucks by</param>
        /// <returns></returns>
        /// <response code="200">Success.  A list of food trucks will be returned</response>
        [HttpGet(Name = GET_ALL_FOOD_TRUCKS)]
        [ProducesResponseType(typeof(List<FoodTruckModel>), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        [MapToApiVersion("1.0")]
        public IActionResult Get([FromQuery]String tag = null)
        {
            // Since we have just one filter possibility, we'll leave this as a simple if statement
            // If we had more/more complex filter criteria, then splitting the logic into multiple methods would be in order
            List<FoodTruck> foodTrucks = null;
            if (tag == null)
            {
                foodTrucks = this.foodTruckService.GetAllFoodTrucks();
            }
            else
            {
                foodTrucks = this.foodTruckService.GetFoodTrucksByTag(tag);
            }
            var models = this.mapper.Map<List<FoodTruck>, List<FoodTruckModel>>(foodTrucks);
            
            return Ok(models);
        }



        ///// <summary>
        ///// Gets a list of all food trucks in the system.  Optionally, a tag value can be provided that will return only food trucks tagged with the given tag
        ///// </summary>
        ///// <param name="tag">An optional tag to filter the food trucks by</param>
        ///// <returns></returns>
        ///// <response code="200">Success.  A list of food trucks will be returned</response>
        //[HttpGet(Name = GET_ALL_FOOD_TRUCKS)]
        //[ProducesResponseType(typeof(List<FoodTruckModel>), 200)]
        //[ProducesResponseType(typeof(ApiMessageModel), 500)]
        //[MapToApiVersion("1.1")]
        //public IActionResult GetV11([FromQuery]String tag = null)
        //{
        //    // Since we have just one filter possibility, we'll leave this as a simple if statement
        //    // If we had more/more complex filter criteria, then splitting the logic into multiple methods would be in order
        //    List<FoodTruck> foodTrucks = null;
        //    if (tag == null)
        //    {
        //        foodTrucks = this.foodTruckService.GetAllFoodTrucks();
        //    }
        //    else
        //    {
        //        foodTrucks = this.foodTruckService.GetFoodTrucksByTag(tag);
        //    }
        //    var models = this.mapper.Map<List<FoodTruck>, List<FoodTruckModelV11>>(foodTrucks);

        //    return Ok(models);
        //}


        /// <summary>
        /// Gets the food truck with the given id
        /// </summary>
        /// <param name="id">An into of the unique id of the food truck</param>
        /// <returns></returns>
        /// <response code="200">Success.  A food truck with the given id was found and is being returned</response>
        /// <response code="404">Not Found.  No food truck was found with the supplied id</response>
        /// <response code="500">Internal Server Error.  An unexpected error internal to the application has occured.  The error has been logged automatically by the system.</response>
        [HttpGet("{id:int}", Name = GET_FOOD_TRUCK_BY_ID)]
        [ProducesResponseType(typeof(FoodTruckModel), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        [MapToApiVersion("1.0")]
        public IActionResult Get(int id)
        {
            FoodTruck foodTruck = this.foodTruckService.GetFoodTruck(id);
                       
            if ( foodTruck == null)
            {
                return this.NotFound(new ApiMessageModel() { Message = $"No food truck found with id {id}"} );
            }
            else
            {
                var model = this.mapper.Map<FoodTruck, FoodTruckModel>(foodTruck);
                return this.Ok(model);
            }
        }



        ///// <summary>
        ///// Gets the food truck with the given id
        ///// </summary>
        ///// <param name="id">An into of the unique id of the food truck</param>
        ///// <returns></returns>
        ///// <response code="200">Success.  A food truck with the given id was found and is being returned</response>
        ///// <response code="404">Not Found.  No food truck was found with the supplied id</response>
        ///// <response code="500">Internal Server Error.  An unexpected error internal to the application has occured.  The error has been logged automatically by the system.</response>
        //[HttpGet("{id:int}", Name = GET_FOOD_TRUCK_BY_ID)]
        //[ProducesResponseType(typeof(FoodTruckModel), 200)]
        //[ProducesResponseType(typeof(ApiMessageModel), 404)]
        //[ProducesResponseType(typeof(ApiMessageModel), 500)]
        //[MapToApiVersion("1.1")]
        //public IActionResult GetV11(int id)
        //{
        //    FoodTruck foodTruck = this.foodTruckService.GetFoodTruck(id);

        //    if (foodTruck == null)
        //    {
        //        return this.NotFound(new ApiMessageModel() { Message = $"No food truck found with id {id}" });
        //    }
        //    else
        //    {
        //        var model = this.mapper.Map<FoodTruck, FoodTruckModelV11>(foodTruck);
        //        return this.Ok(model);
        //    }
        //}



        /// <summary>
        /// Creates a new food truck with the supplied information
        /// </summary>
        /// <param name="createModel">A CreateFoodTruckModel object with the information needed to create the food truck</param>
        /// <returns></returns>
        /// <response code="200">Success.  The new food truck has been created</response>
        /// <response code="409">Conflict.  A food truck with the same name found so this food truck could not be created</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost(Name = CREATE_FOOD_TRUCK)]
        [ProducesResponseType(typeof(FoodTruckModel), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 409)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public IActionResult Post([FromBody]CreateFoodTruckModel createModel)
        {
            var createCommand = this.mapper.Map<CreateFoodTruckModel, CreateFoodTruckCommand>(createModel);

            FoodTruck foodTruck = this.foodTruckService.CreateFoodTruck(createCommand);

            var model = this.mapper.Map<FoodTruck, FoodTruckModel>(foodTruck);
            return this.CreatedAtRoute(GET_FOOD_TRUCK_BY_ID, new { id = model.FoodTruckId }, model);
        }


        /// <summary>
        /// Updates the core data elements of a food truck.
        /// </summary>
        /// <remarks>
        /// This endpoint only updates the properties directly on the food truck.  To
        /// change elements on 'child' objects of the food truck (tags, social media accounts,
        /// reviews, schedules), you need to look in the appropriate association controller, 
        /// for example FoodTruckTagsController.
        /// <para>
        /// This decision was made because when editing a food truck, I think it is more likely
        /// someone will just want to edit elements like the name or description.  It seemed 
        /// unnatural to make them also include the tags or social media accounts as part of
        /// the same update operation.  
        /// </para>
        /// </remarks>
        /// <param name="id">An int of the id of the food truck to update</param>
        /// <param name="updateModel">An UpdateFoodTruckModel of the </param>
        /// <returns></returns>
        /// <response code="200">Success.  The food truck has been updated</response>
        /// <response code="404">No food truck with the given id could be found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut("{id}", Name ="UpdateFoodTruck")]
        [ProducesResponseType(typeof(FoodTruckModel), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ApiMessageModel), 409)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public IActionResult Put(int id, [FromBody]UpdateFoodTruckModel updateModel)
        {
            var updateCommand = new UpdateFoodTruckCommand() { FoodTruckId = id };
            this.mapper.Map<UpdateFoodTruckModel, UpdateFoodTruckCommand>(updateModel, updateCommand);

            FoodTruck foodTruck = this.foodTruckService.UpdateFoodTruck(updateCommand);

            var model = this.mapper.Map<FoodTruck, FoodTruckModel>(foodTruck);
            return this.Ok(model);
        }


        /// <summary>
        /// Deletes the food truck with the given id
        /// </summary>
        /// <param name="id">An int of the food truck to be deleted</param>
        /// <returns></returns>
        /// <response code="200">Success.  The food truck has been deleted</response>
        /// <response code="404">No food truck with the given id could be found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{id}", Name ="DeleteFoodTruck")]
        [ProducesResponseType(typeof(ApiMessageModel), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public IActionResult Delete(int id)
        {
            this.foodTruckService.DeleteFoodTruck(id);

            return this.Ok(new ApiMessageModel() { Message = $"Food truck {id} has been deleted" });
        }



    }
}
