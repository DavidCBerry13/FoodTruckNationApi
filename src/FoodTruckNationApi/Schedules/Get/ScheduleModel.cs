using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Schedules.Get
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
        public ScheduleFoodTruckModel FoodTruck { get; set; }

        /// <summary>
        /// Location of where this scheduled event is to take place
        /// </summary>
        public ScheduleLocationModel Location { get; set; }

        /// <summary>
        /// The starting date/time this food truck will be at this location
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// The ending date/time the food truck is scheduled to be at this location
        /// </summary>
        public DateTime EndTime { get; set; }


    }
}
