using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Api.Locations.Schedules
{
    /// <summary>
    /// Represents the information about a Food truck at a location at a certain time
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    public class LocationScheduleFoodTruckModel
    {

        /// <summary>
        /// The unique id number of this food truck.  This is the id used to get a single food truck on get by id API calls
        /// </summary>
        public int FoodTruckId { get; set; }

        /// <summary>
        /// The name of the food truck
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// A text description of the offerings of the food truck
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// The website for the food truck
        /// </summary>
        public String Website { get; set; }

        /// <summary>
        /// A list of tags associated with the food truck.  This list is just a string list of the tag text
        /// </summary>
        public List<String> Tags { get; set; }

        /// <summary>
        /// Links for the Food truck object
        /// </summary>
        public LocationScheduleFoodTruckLinks Meta { get; set; }

    }


    /// <summary>
    /// Class to encapsulate the links (urls) for a food truck
    /// </summary>
    public class LocationScheduleFoodTruckLinks
    {
        /// <summary>
        /// Gets the URL that refers to this food truck
        /// </summary>
        public String Self { get; set; }

        /// <summary>
        /// Gets the url for the reviews of this food truck
        /// </summary>
        public String Reviews { get; set; }

        /// <summary>
        /// Gets the url for the schedules of this food truck
        /// </summary>
        public String Schedules { get; set; }
    }


}
