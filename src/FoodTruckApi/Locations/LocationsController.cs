using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodTruckNation.Core.Domain;
using Microsoft.Extensions.Logging;
using DavidBerry.Framework.ApiUtil;
using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.Commands;
using DavidBerry.Framework;
using AutoMapper;
using DavidBerry.Framework.ApiUtil.Models;
using DavidBerry.Framework.ApiUtil.Controllers;
using DavidBerry.Framework.ResultType;

namespace FoodTruckNationApi.Locations
{

    /// <summary>
    /// Controller containing endpoints related to locations where food trucks gather at
    /// </summary>
    [Produces("application/json")]
    [Route("api/Locations")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class LocationsController : ApiControllerBase
    {
        /// <summary>
        /// Create a new LocationsController
        /// </summary>
        /// <param name="logger">An ILogger instance to be used for any logging from this controller</param>
        /// <param name="locationService">An ILocationService of the service layer object for this controller to use</param>
        /// <param name="mapper">An IMapper class used by this controller to map objects</param>
        public LocationsController(ILogger<LocationsController> logger, ILocationService locationService, IMapper mapper)
            : base(logger, mapper)
        {
            _locationService = locationService;
        }

        private readonly ILocationService _locationService;


        #region Constants

        /// <summary>
        /// Route Name Constant for route that will get all locations
        /// </summary>
        public const string GET_ALL_LOCATIONS = "GetLocations";

        /// <summary>
        /// Route name constant for route that gets an individual location
        /// </summary>
        public const string GET_LOCATION_BY_ID = "GetLocationById";

        #endregion



        /// <summary>
        /// Gets a list of all locations where food trucks can gather at
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet(Name = GET_ALL_LOCATIONS)]
        [ProducesResponseType(typeof(List<LocationModel>), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public IActionResult Get()
        {
            Result<List<Location>> result = _locationService.GetLocations();

            if (result.IsSuccess)
            {
                var model = _mapper.Map<List<Location>, List<LocationModel>>(result.Value);
                return Ok(model);
            }
            else
            {
                return MapErrorResult<List<Location>, List<LocationModel>>(result);
            }
            return CreateResponse<List<Location>, List<LocationModel>>(result);
        }


        /// <summary>
        ///  Gets the location identified by the given id number
        /// </summary>
        /// <param name="id">The unique id number of this location</param>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="404">No location with the supplied ID was found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{id}", Name = GET_LOCATION_BY_ID)]
        [ProducesResponseType(typeof(LocationModel), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public ActionResult<LocationModel> Get(int id)
        {
            Result<Location> result = _locationService.GetLocation(id);
            if (result.IsSuccess)
            {
                var model = _mapper.Map<Location, LocationModel>(result.Value);
                return Ok(model);
            }
            else
            {
                switch (result.Error)
                {
                    case InvalidDataError error:
                        return BadRequest(new ApiMessageModel() { Message = error.Message });
                    case ObjectNotFoundError error:
                        return NotFound(new ApiMessageModel() { Message = error.Message });
                    default:
                        return this.InternalServerError(new ApiMessageModel() { Message = "An unexpected error has occured.  The error has been logged and is being investigated" });
                }
            }
        }

        /// <summary>
        /// Creates a new Location where food trucks may be at
        /// </summary>
        /// <param name="createModel">A CreateLocationModel object containing all of the data needed to create a new location</param>
        /// <returns></returns>
        /// <response code="201">Created</response>
        /// <response code="400">Request not valid.  Check the errors and try again</response>
        /// <response code="409">A Location with the same address already exists</response>
        [HttpPost]
        [ProducesResponseType(typeof(LocationModel), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ApiMessageModel), 409)]
        public IActionResult Post([FromBody]CreateLocationModel createModel)
        {
            var createLocationCommand = _mapper.Map<CreateLocationModel, CreateLocationCommand>(createModel);
            var result = _locationService.CreateLocation(createLocationCommand);

            return CreateResponse<Location, LocationModel>(result, (entity) => {
                var model = _mapper.Map<Location, LocationModel>(entity);
                return CreatedAtRoute("GetLocationById", new { id = entity.LocationId }, model);
            });
        }


        /// <summary>
        /// Updates the given location with new information
        /// </summary>
        /// <param name="id">The id number of the location to update</param>
        /// <param name="updateModel">An UpdateLocationModel object with the new information for the location</param>
        /// <response code="200">OK.  The location has been updated with the provided information</response>
        /// <response code="400">Bad Requst.  The provided data to update the location with is invalid (check the message in the response for specifics)</response>
        /// <response code="404">Not Found.  No location could be foudn with the specified id</response>
        /// <response code="409">Conflict.  Updating the location as requested would conflict with another already existing location.  Check the response message for details</response>
        /// <response code="500">Internal Server Error.  An unexpected error occured.  The error has been logged so support teams can look into the problem</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(LocationModel), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ApiMessageModel), 409)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public IActionResult Put(int id, [FromBody]UpdateLocationModel updateModel)
        {
            var updateCommand = new UpdateLocationCommand() { LocationId = id };
            _mapper.Map<UpdateLocationModel, UpdateLocationCommand>(updateModel, updateCommand);

            var result = _locationService.UpdateLocation(updateCommand);
            return CreateResponse<Location, LocationModel>(result);
        }



        /// <summary>
        /// Delete the location with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiMessageModel), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 404)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public IActionResult Delete(int id)
        {
            var result = _locationService.DeleteLocation(id);

            return ( result.IsSuccess )
                ? Ok(new ApiMessageModel() { Message = $"Location {id} has been deleted" })
                : MapErrorResult(result);
        }
    }
}
