using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Locations.Schedules
{

    /// <summary>
    /// Model object to represent the schedule of a food truck being at a location
    /// </summary>
    public class LocationScheduleModel
    {
        /// <summary>
        /// id of this schedule
        /// </summary>
        public int ScheduleId { get; set; }

        /// <summary>
        /// Id number of this location
        /// </summary>
        public int LocationId { get; set; }

        /// <summary>
        /// The food truck for this scheduled event.  This is the Model of a food truck,
        /// not just a food truck id so clients don't have to make subsequent calls from
        /// the API to get the food truck information
        /// </summary>
        public FoodTruckModel FoodTruck { get; set; }

        /// <summary>
        /// The start date/time the food truck will be at this location
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// The end date/time the food truck will be at this location
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// URL links to related data about this location schedule event
        /// </summary>
        public LocationScheduleLinks Meta { get; set; }



        #region Nested Class - Food Truck

        /// <summary>
        /// Represents the information about a Food truck at a location at a certain time
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
            /// Links for the Food truck object
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


        #region Nested Class - LocationScheduleLinks

        /// <summary>
        /// Model representing links to associated information for this location schedule
        /// </summary>
        public class LocationScheduleLinks
        {
            /// <summary>
            /// URL Link to the schedule event
            /// </summary>
            public String Self { get; set; }

            /// <summary>
            /// URL link to the location this schedule event is at
            /// </summary>
            public String Location { get; set; }
        }

        #endregion
    }



}
