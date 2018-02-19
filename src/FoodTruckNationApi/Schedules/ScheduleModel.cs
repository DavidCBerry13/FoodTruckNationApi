using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Schedules
{

    /// <summary>
    /// Represents a Schedule of a food truck being at a certain location at a certain time
    /// </summary>
    public class ScheduleModel
    {

        /// <summary>
        /// Unique identifier of this schedule record
        /// </summary>
        public int ScheduleId { get; set; }

        /// <summary>
        /// Food truck associated with this schedule
        /// </summary>
        public FoodTruckModel FoodTruck { get; set; }

        /// <summary>
        /// Location of where this scheduled event is to take place
        /// </summary>
        public LocationModel Location { get; set; }

        /// <summary>
        /// The starting date/time this food truck will be at this location
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// The ending date/time the food truck is scheduled to be at this location
        /// </summary>
        public DateTime EndTime { get; set; }



        #region Nested Types - Location

        public class LocationModel
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


        #endregion

        #region Nested Types - Food Truck

        /// <summary>
        /// Represents a food truck for a schedule (appointment) item
        /// </summary>
        public class FoodTruckModel
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
            /// Meta data object containing associated links for the food truck
            /// </summary>
            public FoodTruckLinks Meta { get; set; }
        }


        /// <summary>
        /// Class to encapsulate the links (urls) for a food truck
        /// </summary>
        public class FoodTruckLinks
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

        #endregion
    }
}
