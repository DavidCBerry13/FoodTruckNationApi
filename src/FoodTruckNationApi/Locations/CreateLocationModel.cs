using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodTruckNationApi.Locations
{


    /// <summary>
    /// Represents the information that must be provided when creating a new location where food trucks gather
    /// </summary>
    public class CreateLocationModel
    {

        /// <summary>
        /// The descriptive name to give to this location
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// The street address of this location
        /// </summary>
        public String StreetAddress { get; set; }

        /// <summary>
        /// The city this location is in
        /// </summary>
        public String City { get; set; }

        /// <summary>
        /// The two digit state abbreviation of where this lcoation is
        /// </summary>
        public String State { get; set; }

        /// <summary>
        /// The five digit zip code of this location
        /// </summary>
        public String ZipCode { get; set; }

    }
}
