using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodTruckNation.Core.Domain;
using Microsoft.Extensions.Logging;
using Framework.ApiUtil;
using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.Commands;
using Framework;
using AutoMapper;
using Framework.ApiUtil.Models;
using Framework.ApiUtil.Controllers;
using FoodTruckNationApi.Locations.Base.Get;
using FoodTruckNationApi.Locations.Base.Create;
using FoodTruckNationApi.Locations.Base.Update;

namespace FoodTruckNationApi.Locations
{

    /// <summary>
    /// Controller containing endpoints related to locations where food trucks gather at
    /// </summary>
    [Produces("application/json")]
    [Route("api/Locations")]
    [ApiVersion("1.0")]
    public class LocationsController : BaseController
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
            this.locationService = locationService;
        }

        private ILocationService locationService;


        #region Constants

        /// <summary>
        /// Route Name Constant for route that will get all locations
        /// </summary>
        public const String GET_ALL_LOCATIONS = "GetLocations";

        /// <summary>
        /// Route name constant for route that gets an individual location
        /// </summary>
        public const String GET_LOCATION_BY_ID = "GetLocationById";

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
            var locations = this.locationService.GetLocations();
            var locationModels = this.mapper.Map<List<Location>, List<LocationModel>>(locations);
            return Ok(locationModels);
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
        public IActionResult Get(int id)
        {
            var location = this.locationService.GetLocation(id);

            if ( location != null)
            {
                var locationModel = this.mapper.Map<Location, LocationModel>(location);
                return Ok(locationModel);
            }  
            else
            {
                return this.NotFound(new ApiMessageModel { Message = $"No location could be found with the id {id}"});
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
            var createLocationCommand = this.mapper.Map<CreateLocationModel, CreateLocationCommand>(createModel);
            Location location = this.locationService.CreateLocation(createLocationCommand);

            var model = this.mapper.Map<Location, LocationModel>(location);
            return this.CreatedAtRoute("GetLocationById", new { id = location.LocationId }, model);
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
            this.mapper.Map<UpdateLocationModel, UpdateLocationCommand>(updateModel, updateCommand);

            Location location = this.locationService.UpdateLocation(updateCommand);

            var model = this.mapper.Map<Location, LocationModel>(location);
            return this.Ok(model);
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
            this.locationService.DeleteLocation(id);

            return this.Ok(new ApiMessageModel() { Message = $"Location {id} has been deleted" });
        }
    }
}
