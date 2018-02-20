using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.FoodTrucks.Schedules
{
    public class FoodTruckScheduleModel
    {
        /// <summary>
        /// Unique identifier of this schedule record
        /// </summary>
        public int ScheduleId { get; set; }

        /// <summary>
        /// Id number of the food truck this schedule is for
        /// </summary>
        public int FoodTruckId { get; set; }

        /// <summary>
        /// Location of where this scheduled event is to take place
        /// </summary>
        /// <remarks>
        /// This model object contains the full LocationModel object rather than
        /// just a location id so clients do not have to make subsequent calls to
        /// the API to get the location information for each schedule
        /// </remarks>
        public LocationModel Location { get; set; }

        /// <summary>
        /// The starting date/time this food truck will be at this location
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// The ending date/time the food truck is scheduled to be at this location
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// URL Links to related objects
        /// </summary>
        public FoodTruckScheduleLinks Meta { get; set; }


        #region Nested Types

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


        /// <summary>
        /// Model class to encapsulate the links for a FoodTruckSchedule object
        /// </summary>
        public class FoodTruckScheduleLinks
        {
            /// <summary>
            /// URL Link to this food truck scedule object
            /// </summary>
            public String Self { get; set; }

            /// <summary>
            /// URL Link to the Food Truck this schedule belongs to
            /// </summary>
            public String FoodTruck { get; set; }

        }

        #endregion




    }



}
