using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Framework.ApiUtil;
using Microsoft.Extensions.Logging;
using AutoMapper;
using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.Domain;
using FoodTruckNation.Core.Commands;
using Framework.ApiUtil.Controllers;
using Framework.ApiUtil.Models;

namespace FoodTruckNationApi.Api.FoodTrucks.Reviews
{
    [Produces("application/json")]
    [Route("api/FoodTrucks/{foodTruckId}/Reviews")]
    [ApiVersion("1.0")]
    public class FoodTruckReviewsController : BaseController
    {

        public FoodTruckReviewsController(ILogger<FoodTruckReviewsController> logger, IMapper mapper, IFoodTruckService foodTruckService)
            : base(logger, mapper)
        {
            this.foodTruckService = foodTruckService;
        }


        private IFoodTruckService foodTruckService;


        /// <summary>
        /// Route name for the route that gets all reviews for a food truck
        /// </summary>
        public const String GET_ALL_FOOD_TRUCK_REVIEWS = "GetAllFoodTruckReviews";

        /// <summary>
        /// Route name for the route that gets a single review for a food truck
        /// </summary>
        public const String GET_SINGLE_FOOD_TRUCK_REVIEW = "GetFoodTruckReviewsById";


        /// <summary>
        /// Gets all of the reviews for the specified food truck
        /// </summary>
        /// <param name="foodTruckId">The int of the unique id number of the food truck</param>
        /// <returns>An IActionResult containing the list of reviews for the food truck</returns>
        /// <response code="200">Success.  A list of reviews for the food truck is being returned</response>
        /// <response code="404">Not Found.  No food truck was found with the given id</response>
        /// <response code="500">Internal Server Error.  An unexpected error internal to the application has occured.  The error has been logged automatically by the system.</response>
        [HttpGet(Name = GET_ALL_FOOD_TRUCK_REVIEWS)]
        [ProducesResponseType(typeof(FoodTruckModel), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public IActionResult Get(int foodTruckId)
        {
            var reviews = this.foodTruckService.GetFoodTruckReviews(foodTruckId);

            var models = this.mapper.Map<List<Review>, List<ReviewModel>>(reviews);
            return this.Ok(models);            
        }


        /// <summary>
        /// Gets an individual review for a food truck, as specified by the food truck id and the review id
        /// </summary>
        /// <param name="foodTruckId">The int of the unique id number of the food truck</param>
        /// <param name="reviewId">The int of the id number of the review</param>
        /// <returns></returns>
        /// <response code="200">Success.  The specified review is being returned</response>
        /// <response code="404">Not Found.  Either no food truck was found with the given id or no review was found for this truck with the specified id</response>
        /// <response code="500">Internal Server Error.  An unexpected error internal to the application has occured.  The error has been logged automatically by the system.</response>
        [HttpGet("{reviewId:int}", Name = GET_SINGLE_FOOD_TRUCK_REVIEW)]
        [ProducesResponseType(typeof(FoodTruckModel), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public IActionResult Get(int foodTruckId, int reviewId)
        {
            var review = this.foodTruckService.GetFoodTruckReview(foodTruckId, reviewId);

            if (review == null)
                return this.NotFound($"Review {reviewId} not found for food truck id {foodTruckId}");

            var model = this.mapper.Map<Review, ReviewModel>(review);
            return this.Ok(model);
        }

        /// <summary>
        /// Creates a new review for the specified food truck
        /// </summary>
        /// <param name="foodTruckId">The int of the unique id number of the food truck to create a review for</param>
        /// <param name="createModel"></param>
        /// <returns></returns>
        /// <response code="201">Created.  The new review was successfully created.  A ReviewModel object for the review is returned and a link to the endpoint is included in the Location header</response>
        /// <response code="400">Bad Request.  The request was invalid.  A list of errors will be returned</response>
        /// <response code="404">Not Found.  No food truck was found with the given id so no review could be added</response>
        /// <response code="500">Internal Server Error.  An unexpected error internal to the application has occured.  The error has been logged automatically by the system.</response>
        [HttpPost]
        [ProducesResponseType(typeof(FoodTruckModel), 201)]
        [ProducesResponseType(typeof(List<RequestErrorModel>), 400)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public IActionResult Post(int foodTruckId, [FromBody]CreateReviewModel createModel)
        {
            var createCommand = new CreateReviewCommand() { FoodTruckId = foodTruckId };
            this.mapper.Map<CreateReviewModel, CreateReviewCommand>(createModel, createCommand);

            Review foodTruck = this.foodTruckService.CreateFoodTruckReview(createCommand);

            var model = this.mapper.Map<Review, ReviewModel>(foodTruck);
            return this.CreatedAtRoute(GET_SINGLE_FOOD_TRUCK_REVIEW, 
                new { FoodTruckId = model.FoodTruckId, ReviewId = model.ReviewId }, model);
        }


        //// PUT: api/FoodTruckReviews/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
