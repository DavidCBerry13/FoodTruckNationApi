using System.Collections.Generic;
using Asp.Versioning;
using AutoMapper;
using DavidBerry.Framework.ApiUtil.Controllers;
using DavidBerry.Framework.ApiUtil.Models;
using FoodTruckApi.Localities.Models;
using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.AppServices;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;
using FoodTruckNationApi.FoodTrucks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodTruckApi.Localities
{

    /// <summary>
    /// Controller for the locality endpoints.  Localities represent a city or region like Chicago, Milwaukee, or Madison.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class LocalitiesController : ApiControllerBase
    {


        /// <summary>
        /// Creates a new LocalitiesController
        /// </summary>
        /// <param name="logger">An ILogger object used to for any logging inside of this controller</param>
        /// <param name="mapper">An Automapper IMapper object used for object mapping within this controller</param>
        /// <param name="localityService">An ILocalityService object that contains the business logic for locality functions</param>
        public LocalitiesController(ILogger<LocalitiesController> logger, IMapper mapper, ILocalityService localityService)
            : base(logger, mapper)
        {
            _localityService = localityService;
        }


        private readonly ILocalityService _localityService;


        #region Route Constants

        /// <summary>
        /// Route Name Constant for route that will get all localities
        /// </summary>
        public const string GET_ALL_LOCALITIES = "GetLocalities";

        /// <summary>
        /// Route name constant for route that gets an individual locality
        /// </summary>
        public const string GET_LOCALITY_BY_CODE = "GetLocalityByCode";

        /// <summary>
        /// Route name constant for route that creates a new Food Truck
        /// </summary>
        public const string CREATE_LOCALITY = "CreatLocality";


        #endregion



        /// <summary>
        /// Gets a list of all localities in the system.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success.  A list of localities will be returned</response>
        [HttpGet(Name = GET_ALL_LOCALITIES)]
        [ProducesResponseType(typeof(List<LocalityModel>), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public ActionResult<FoodTruckModel> Get()
        {
            // Since we have just one filter possibility, we'll leave this as a simple conditional statement
            // If we had more/more complex filter criteria, then splitting the logic into multiple methods would be in order
            var result = _localityService.GetLocalities();

            return CreateResponse<List<Locality>, List<LocalityModel>>(result);
        }


        /// <summary>
        /// Gets the locality with the given locality code
        /// </summary>
        /// <param name="code">A string of the unique locality code to get the locality for</param>
        /// <returns></returns>
        /// <response code="200">Success.  A locality with the given code was found and is being returned</response>
        /// <response code="404">Not Found.  No locality was found with the supplied code</response>
        /// <response code="500">Internal Server Error.  An unexpected error internal to the application has occured.  The error has been logged automatically by the system.</response>
        [HttpGet("{code}", Name = GET_LOCALITY_BY_CODE)]
        [ProducesResponseType(typeof(LocalityModel), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public ActionResult<FoodTruckModel> Get(string code)
        {
            var result = _localityService.GetLocality(code);
            return CreateResponse<Locality, LocalityModel>(result);
        }



        /// <summary>
        /// Creates a new locality with the supplied information
        /// </summary>
        /// <param name="createModel">A CreateLocalityModel object with the information needed to create the locality</param>
        /// <returns></returns>
        /// <response code="201">Success. The new Locality has been created</response>
        /// <response code="409">Conflict. A locality with the same name found so this locality could not be created</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost(Name = CREATE_LOCALITY)]
        [ProducesResponseType(typeof(LocalityModel), 201)]
        [ProducesResponseType(typeof(ApiMessageModel), 409)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public ActionResult<LocalityModel> Post([FromBody] CreateLocalityModel createModel)
        {
            var createCommand = _mapper.Map<CreateLocalityModel, CreateLocalityCommand>(createModel);
            var result = _localityService.CreateLocality(createCommand);

            if (result.IsSuccess)
            {
                var model = _mapper.Map<Locality, LocalityModel>(result.Value);
                return CreatedAtRoute(GET_LOCALITY_BY_CODE, new { code = model.Code }, model);
            }
            else
            {
                return MapErrorResult<Locality, LocalityModel>(result);
            }
        }

        /// <summary>
        /// Update the locality with the given locality code
        /// </summary>
        /// <param name="code">The unique code of the locality to update</param>
        /// <param name="updateModel">The UpdateLocalityModel object of the new data for the locality </param>
        /// <returns></returns>
        [HttpPut("{code}", Name = "UpdateLocality")]
        [ProducesResponseType(typeof(LocalityModel), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ConcurrencyErrorModel<FoodTruckModel>), 409)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        [MapToApiVersion("1.0")]
        public ActionResult<LocalityModel> Put(string code, [FromBody] UpdateLocalityModel updateModel)
        {
            var updateCommand = new UpdateLocalityCommand() { Code = code };
            _mapper.Map<UpdateLocalityModel, UpdateLocalityCommand>(updateModel, updateCommand);

            var result = _localityService.UpdateLocality(updateCommand);
            return CreateResponse<Locality, LocalityModel>(result);
        }


        /// <summary>
        /// Deletes the locality with the given locality code
        /// </summary>
        /// <param name="code">A string of the locality code of the locality to be deleted</param>
        /// <returns></returns>
        /// <response code="200">Success. The locality has been deleted</response>
        /// <response code="404">No locality with the given id could be found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{code}", Name = "DeleteLocality")]
        [ProducesResponseType(typeof(ApiMessageModel), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public IActionResult Delete(string code)
        {
            var result = _localityService.DeleteLocality(code);

            return ( result.IsSuccess )
                ? Ok(new ApiMessageModel() { Message = $"Locality {code} has been deleted" })
                : MapErrorResult(result);
        }
    }
}
