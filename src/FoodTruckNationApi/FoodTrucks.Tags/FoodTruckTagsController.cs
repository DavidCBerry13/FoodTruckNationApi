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
using Framework.ApiUtil.Models;
using FoodTruckNation.Core.Domain;
using FoodTruckNationApi.FoodTrucks.Get;

namespace FoodTruckNationApi.FoodTrucks.Tags
{
    [Produces("application/json")]
    [Route("api/FoodTrucks/{foodTruckId}/Tags")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class FoodTruckTagsController : BaseController
    {

        /// <summary>
        /// Creates a new FoodTruckTagsController
        /// </summary>
        /// <param name="logger">An ILogger object used to log messages from this controller</param>
        /// <param name="mapper">An Automapper IMapper object used for object mapping within this controller</param>
        /// <param name="foodTruckService">An IFoodTruckService object that contains the business logic for food truck functions</param>
        public FoodTruckTagsController(ILogger<FoodTruckTagsController> logger, IMapper mapper, IFoodTruckService foodTruckService)
            : base(logger, mapper)
        {
            this.foodTruckService = foodTruckService;
        }


        private IFoodTruckService foodTruckService;


        /// <summary>
        /// Gets the list of tags on the specified food truck
        /// </summary>
        /// <param name="foodTruckId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<String>), 200)]
        public IActionResult Get(int foodTruckId)
        {
            FoodTruck foodTruck = this.foodTruckService.GetFoodTruck(foodTruckId);

            if (foodTruck == null)
            {
                return this.NotFound(new ApiMessageModel() { Message = $"No food truck found with id {foodTruckId}" });
            }
            else
            {
                var model = this.mapper.Map<List<FoodTruckTag>, List<String>>(foodTruck.Tags);
                return this.Ok(model);
            }
        }

        /// <summary>
        /// Adds the given tags to the food truck
        /// </summary>
        /// <remarks>
        /// This endpoint allows you to add one or more tags to an existing food truck.  The tags
        /// may already exist or they don't have to.  New tags will automatically be created and 
        /// added to the food truck
        /// <para>
        /// This response marks a departure from REST conventions in that I am returning the entire
        /// FoodTruckModel object rather than a list of the updated tags.  This is a decision point
        /// and for this example, I am making the decision the client would most likely want an updated
        /// representation of the entire food truck, not just the list of tags
        /// </para>
        /// </remarks>
        /// <param name="foodTruckId"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(FoodTruckModel), 200)]
        public IActionResult Post(int foodTruckId, [FromBody]List<string> tags)
        {
            FoodTruck foodTruck = this.foodTruckService.AddFoodTruckTags(foodTruckId, tags);

            var model = this.mapper.Map<FoodTruck, FoodTruckModel>(foodTruck);
            return this.Ok(model);
        }

        /// <summary>
        /// Updates the list of tags to the food truck to the specified list
        /// </summary>
        /// <remarks>
        /// This method effectively replaces the existing list of tags on the food truck with
        /// the list specified.  Any new tags in the specified list are added to the food truck, 
        /// where as any existing tags not included in the list will be removed from the food truck.
        /// <para>
        /// This response marks a departure from REST conventions in that I am returning the entire
        /// FoodTruckModel object rather than a list of the updated tags.  This is a decision point
        /// and for this example, I am making the decision the client would most likely want an updated
        /// representation of the entire food truck, not just the list of tags
        /// </para>
        /// </remarks>
        /// <param name="foodTruckId"></param>
        /// <param name="tags"></param>
        /// <response code="200">Success.  A FoodTruckModel that represents the current state of the food truck is returned</response>
        [HttpPut()]
        [ProducesResponseType(typeof(FoodTruckModel), 200)]
        public IActionResult Put(int foodTruckId, [FromBody]List<string> tags)
        {
            FoodTruck foodTruck = this.foodTruckService.UpdateFoodTruckTags(foodTruckId, tags);

            var model = this.mapper.Map<FoodTruck, FoodTruckModel>(foodTruck);
            return this.Ok(model);
        }


        /// <summary>
        /// Deletes the specified tag for the given food truck
        /// </summary>
        /// <param name="foodTruckId"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        /// <response code="200">Success.  A message indicating the tag has been removed is returned</response>
        [HttpDelete("{tag}")]
        [ProducesResponseType(typeof(ApiMessageModel), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public IActionResult Delete(int foodTruckId, string tag)
        {
            this.foodTruckService.DeleteFoodTruckTag(foodTruckId, tag);

            return this.Ok(new ApiMessageModel() { Message = $"Tag {tag} has been deleted on food truck {foodTruckId}" });
        }
    }
}
