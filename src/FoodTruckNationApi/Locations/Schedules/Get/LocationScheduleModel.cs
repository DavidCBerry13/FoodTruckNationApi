using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Locations.Schedules.Get
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
        public LocationScheduleFoodTruckModel FoodTruck { get; set; }

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

    }

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

}
