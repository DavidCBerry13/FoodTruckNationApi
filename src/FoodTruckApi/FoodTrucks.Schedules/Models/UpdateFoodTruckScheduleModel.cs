using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.FoodTrucks.Schedules
{
    /// <summary>
    /// Modul used to update an existing schedule (appointment) for a food truck..
    /// </summary>
    /// <remarks>
    /// Updating a schedule is changing the start and/or end time of the schedule.  If a user wants to change the location
    /// or the food truck for a schedule, they should delete the existing schedule and create a new one.
    /// </remarks>
    public class UpdateFoodTruckScheduleModel
    {

        /// <summary>
        /// The date/time the food truck will arrive
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// The date/time the food truck will leave
        /// </summary>
        public DateTime EndTime { get; set; }

    }
}
