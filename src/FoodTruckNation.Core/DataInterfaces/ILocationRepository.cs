using FoodTruckNation.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruckNation.Core.DataInterfaces
{

    /// <summary>
    /// Defines the interface for querying Location objects from the data store
    /// </summary>
    public interface ILocationRepository
    {

        /// <summary>
        /// Gets the Location specified by the given location id.  If no location has the specified id, null is returned
        /// </summary>
        /// <param name="locationId">An into of the location id</param>
        /// <returns>A Location object or null</returns>
        public Task<Location> GetLocationAsync(int locationId);


        public Task<IEnumerable<Location>> GetLocationsAsync();


        /// <summary>
        /// Get all of the locations in the locality identified by the given locality code
        /// </summary>
        /// <param name="locality">The Locality to get the locations for</param>
        /// <returns>An IEnurable of Locations</returns>
        public Task<IEnumerable<Location>> GetLocationsAsync(Locality locality);



        public Task SaveAsync(Location location);


        public Task DeleteAsync(Location location);


    }
}
