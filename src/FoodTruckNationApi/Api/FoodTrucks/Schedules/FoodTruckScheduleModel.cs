using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Api.FoodTrucks.Schedules
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
        public FoodTruckScheduleLocationModel Location { get; set; }

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

}
