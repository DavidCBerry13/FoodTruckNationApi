using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Schedules
{

    /// <summary>
    /// Represents the query string parameters that can be passed to the Get operation
    /// on the Schedules controller
    /// </summary>
    /// <remarks>
    /// These parameters are encapsulated into an object rather than just being
    /// method parameters because then we can use Model State Validation to validate
    /// any passed parameters are correct rather than doing that validation by hand
    /// in the operation method itself
    /// </remarks>
    public class GetSchedulesParameters
    {

        /// <summary>
        /// Optional start date of the date range to get schedules for
        /// </summary>
        /// <remarks>
        /// If you provide a start date, then you must also provide an end date
        /// </remarks>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Optional end date of the date range to get schedules for
        /// </summary>
        /// <remarks>
        /// If you provide an end date, then a start fate must also be provided
        /// </remarks>
        public DateTime? EndDate { get; set; }

    }
}
