using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.Domain;
using FoodTruckNation.Core.Commands;
using Framework.ApiUtil.Controllers;
using Framework.ApiUtil.Models;

namespace FoodTruckNationApi.FoodTrucks.Reviews
{
    [Produces("application/json")]
    [Route("api/FoodTrucks/{foodTruckId}/Reviews")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class FoodTruckReviewsController : ApiControllerBase
    {

        public FoodTruckReviewsController(ILogger<FoodTruckReviewsController> logger, IMapper mapper, IFoodTruckService foodTruckService)
            : base(logger, mapper)
        {
            _foodTruckService = foodTruckService;
        }


        private readonly IFoodTruckService _foodTruckService;


        /// <summary>
        /// Route name for the route that gets all reviews for a food truck
        /// </summary>
        internal const string GET_ALL_FOOD_TRUCK_REVIEWS = "GetAllFoodTruckReviews";

        /// <summary>
        /// Route name for the route that gets a single review for a food truck
        /// </summary>
        internal const string GET_SINGLE_FOOD_TRUCK_REVIEW = "GetFoodTruckReviewsById";


        /// <summary>
        /// Gets all of the reviews for the specified food truck
        /// </summary>
        /// <param name="foodTruckId">The int of the unique id number of the food truck</param>
        /// <returns>An IActionResult containing the list of reviews for the food truck</returns>
        /// <response code="200">Success.  A list of reviews for the food truck is being returned</response>
        /// <response code="404">Not Found.  No food truck was found with the given id</response>
        /// <response code="500">Internal Server Error.  An unexpected error internal to the application has occured.  The error has been logged automatically by the system.</response>
        [HttpGet(Name = GET_ALL_FOOD_TRUCK_REVIEWS)]
        [ProducesResponseType(typeof(List<ReviewModel>), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public ActionResult<List<ReviewModel>> Get(int foodTruckId)
        {
            var result = _foodTruckService.GetFoodTruckReviews(foodTruckId);
            return CreateResponse<List<Review>, List<ReviewModel>>(result);          
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
        [ProducesResponseType(typeof(ReviewModel), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public ActionResult<Review> Get(int foodTruckId, int reviewId)
        {
            var result = _foodTruckService.GetFoodTruckReview(foodTruckId, reviewId);
            return CreateResponse<Review, ReviewModel>(result);
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
        [ProducesResponseType(typeof(ReviewModel), 201)]
        [ProducesResponseType(typeof(List<RequestErrorModel>), 400)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public IActionResult Post(int foodTruckId, [FromBody]CreateReviewModel createModel)
        {
            var createCommand = new CreateReviewCommand() { FoodTruckId = foodTruckId };
            _mapper.Map<CreateReviewModel, CreateReviewCommand>(createModel, createCommand);

            var result = _foodTruckService.CreateFoodTruckReview(createCommand);

            return CreateResponse<Review, ReviewModel>(result, (entity) => {
                var model = _mapper.Map<Review, ReviewModel>(entity);
                return CreatedAtRoute(GET_SINGLE_FOOD_TRUCK_REVIEW, new { foodTruckId = entity.FoodTruckId, reviewId = entity.ReviewId }, model);
            });
        }

    }
}
