using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Locations.Schedules.Get
{

    /// <summary>
    /// Represents the parameters that can be passed to the GET operation of the 
    /// Locations/{locationId}/Schedules end point
    /// </summary>
    public class GetLocationSchedulesParameters
    {

        /// <summary>
        /// Optional start time for the date range to get schedules for
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Optional end time for the date range to get schedules for
        /// </summary>
        public DateTime? EndDate { get; set; }



    }
}
