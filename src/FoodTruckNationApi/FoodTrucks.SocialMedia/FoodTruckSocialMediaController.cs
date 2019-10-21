using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using FoodTruckNation.Core.AppInterfaces;
using Framework.ApiUtil.Controllers;
using FoodTruckNation.Core.Domain;
using Framework.ApiUtil.Models;

namespace FoodTruckNationApi.Api.FoodTrucks.SocialMedia
{
    /// <summary>
    /// API Controller to get/add/update social media accounts for a food truck
    /// </summary>
    [Produces("application/json")]
    [Route("api/FoodTrucks/{foodTruckId}/SocialMediaAccounts")]
    [ApiVersion("1.1")]
    public class FoodTruckSocialMediaController : ApiControllerBase
    {

        public FoodTruckSocialMediaController(ILogger<FoodTruckSocialMediaController> logger, IMapper mapper, IFoodTruckService foodTruckService)
            : base(logger, mapper)
        {
            _foodTruckService = foodTruckService;
        }


        private readonly IFoodTruckService _foodTruckService;



        internal const string GET_FOOD_TRUCK_SOCIAL_ACCOUNTS = "GetFoodTruckSocialMediaAccounts";

        internal const string GET_FOOD_TRUCK_SOCIAL_ACCOUNT_BY_ID = "GetFoodTruckSocialMediaAccountById";


        /// <summary>
        /// Gets the list of social media accounts for this food truck
        /// </summary>
        /// <param name="foodTruckId">The id number of this food truck</param>
        /// <returns></returns>
        /// <response code="200">Success.  A list of the current social media accounts for the food truck is returned</response>
        /// <response code="404">Not Found.  No food truck could be found for this id</response>
        /// <response code="500">Internal Server Error.  An unexpected problem occured on the server occured.  The error has been logged by the server</response>
        [HttpGet(Name = "GetFoodTruckSocialMediaAccounts")]
        [ProducesResponseType(typeof(List<SocialMediaAccount>), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public IActionResult Get(int foodTruckId)
        {
            var result = _foodTruckService.GetFoodTruck(foodTruckId);

            if ( result.IsSuccess )
            {
                var models = _mapper.Map<List<SocialMediaAccount>, List<SocialMediaAccountModelV11>>(result.Value.SocialMediaAccounts);
                return Ok(models);
            }
            else
            {
                return MapErrorResult<List<SocialMediaAccount>, List<SocialMediaAccountModelV11>>(result);
            }
        }


        /// <summary>
        /// Gets a specific social media account for a food truck
        /// </summary>
        /// <param name="foodTruckId">The id of the food truck</param>
        /// <param name="socialAccountId">The id of the social media account to fetch</param>
        /// <returns></returns>
        /// <response code="200">Success.  A corresponding social media account will be returned</response>
        /// <response code="404">Not Found.  Either the food truck or the specified social media account could not be found</response>
        /// <response code="500">Internal Server Error.  An unexpected problem occured on the server occured.  The error has been logged by the server</response>
        [HttpGet("{socialAccountId}", Name = GET_FOOD_TRUCK_SOCIAL_ACCOUNT_BY_ID)]
        [ProducesResponseType(typeof(SocialMediaAccount), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public IActionResult Get(int foodTruckId, int socialAccountId)
        {
            var result = _foodTruckService.GetFoodTruck(foodTruckId);
            
            if (result.IsSuccess)
            {
                var model = _mapper.Map<SocialMediaAccount, SocialMediaAccountModelV11>(result.Value.SocialMediaAccounts.FirstOrDefault(x => x.SocialMediaAccountId == socialAccountId));
                return Ok(model);
            }
            else
            {
                return MapErrorResult<List<SocialMediaAccount>, List<SocialMediaAccountModelV11>>(result);
            }
        }

        /// <summary>
        /// Creates a new Social Media Account for the specified food truck
        /// </summary>
        /// <param name="foodTruckId">The id of the food truck to create the social media account for</param>
        /// <param name="socialMediaAccountModel">A model object containing the social media account information</param>
        /// <returns></returns>
        /// <response code="200">Success.  The social media account was created</response>
        /// <response code="400">Request error.  The request contained invalid data, like an account name that does not correspond to the rules of this social media platform</response>
        /// <response code="404">Not Found.  No food truck with the specified id could be found to create a social media account for</response>
        /// <response code="409">Conflict.  This food truck already contains a social media account for the specified platform</response>
        /// <response code="500">Internal Server Error.  An unexpected problem occured on the server occured.  The error has been logged by the server</response>
        [HttpPost]
        [ProducesResponseType(typeof(SocialMediaAccount), 201)]
        [ProducesResponseType(typeof(RequestErrorModel), 400)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ApiMessageModel), 409)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public IActionResult Post(int foodTruckId, [FromBody]CreateSocialMediaAccountModel socialMediaAccountModel)
        {
            var result = _foodTruckService.AddSocialMediaAccount(foodTruckId, 
                socialMediaAccountModel.SocialMediaPlatformId,
                socialMediaAccountModel.AccountName);

            return CreateResponse<SocialMediaAccount, SocialMediaAccountModelV11>(result, (account) => {
               var model = _mapper.Map<SocialMediaAccount, SocialMediaAccountModelV11>(account);
               return CreatedAtRoute(GET_FOOD_TRUCK_SOCIAL_ACCOUNT_BY_ID,
                   new { foodTruckId, socialAccountId = account.SocialMediaAccountId },
                   model);
            });
        }
        
        // PUT: api/FoodTruckSocialMedia/5
        [HttpPut("{socialAccountId}")]
        public IActionResult Put(int foodTruckId, int socialMediaAccountId, [FromBody]UpdateSocialMediaAccount updateModel)
        {
            var result = _foodTruckService.UpdateSocialMediaAccount(foodTruckId, socialMediaAccountId, updateModel.AccountName);
            return CreateResponse<SocialMediaAccount, SocialMediaAccountModelV11>(result);
        }


        /// <summary>
        /// Deletes a social media account for a food truck
        /// </summary>
        /// <param name="foodTruckId">The id of the food truck</param>
        /// <param name="socialMediaAccountId">The id of the social media account to delete</param>
        /// <returns></returns>
        /// <response code="200">Success.  The social media account has been deleted</response>
        /// <response code="404">Not Found.  Either the food truck or the specified social media account could not be found</response>
        /// <response code="500">Internal Server Error.  An unexpected problem occured on the server occured.  The error has been logged by the server</response>
        [HttpDelete("{socialMediaAccountId}")]
        [ProducesResponseType(typeof(SocialMediaAccount), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public IActionResult Delete(int foodTruckId, int socialMediaAccountId)
        {
            var result = _foodTruckService.DeleteSocialMediaAccount(foodTruckId, socialMediaAccountId);

            return ( result.IsSuccess )
                ? Ok(new ApiMessageModel() { Message = $"Social Media Account deleted" })
                : MapErrorResult(result);
        }
    }
}
