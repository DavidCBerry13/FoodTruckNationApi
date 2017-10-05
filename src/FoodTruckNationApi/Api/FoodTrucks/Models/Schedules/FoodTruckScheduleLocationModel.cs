using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Api.FoodTrucks.Models
{
    public class FoodTruckScheduleLocationModel
    {
        /// <summary>
        /// Gets the unique id of this location
        /// </summary>
        public int LocationId { get; set; }

        /// <summary>
        /// The name of this location, usually the name of a park or business that is associated with the location
        /// </summary>
        public String LocationName { get; set; }

        /// <summary>
        /// The street address of this location
        /// </summary>
        public String StreetAddress { get; set; }

        /// <summary>
        /// The city this location is in
        /// </summary>
        public String City { get; set; }

        /// <summary>
        /// The state of this location
        /// </summary>
        public String State { get; set; }

        /// <summary>
        /// The zip code of this location
        /// </summary>
        public String ZipCode { get; set; }


        /// <summary>
        /// Meta data object containing associated links for the location
        /// </summary>
        public LocationLinks Meta { get; set; }
    }


    /// <summary>
    /// Class to encapsulate the links (urls) for a location
    /// </summary>
    public class LocationLinks
    {
        /// <summary>
        /// Gets the URL that refers to this location
        /// </summary>
        public String Self { get; set; }

        /// <summary>
        /// URL link containing the upcoming schedules of food trucks at this location
        /// </summary>
        public String Schedules { get; set; }
    }
}
