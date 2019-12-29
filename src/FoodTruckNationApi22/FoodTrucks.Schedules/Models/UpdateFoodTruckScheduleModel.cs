using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.FoodTrucks.Schedules
{
    public class UpdateFoodTruckScheduleModel
    {

        /// <summary>
        /// The id of the location where the food truck will be at this time
        /// </summary>
        public int LocationId { get; set; }

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
