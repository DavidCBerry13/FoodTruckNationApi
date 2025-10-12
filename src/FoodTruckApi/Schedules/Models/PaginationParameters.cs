using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Schedules
{
    /// <summary>
    /// Defines pagination parameters for schedule requests that return large sets of data
    /// </summary>
    public class PaginationParameters
    {

        /// <summary>
        /// Sets the current page number in a paginated collection.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Sets the number of items to include on each page of results.
        /// </summary>
        public int PageSize { get; set; }
    }
}
