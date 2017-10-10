using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodTruckNationApi.Locations.Base.Update
{

    /// <summary>
    /// Model used to update the properties of a location
    /// </summary>
    /// <remarks>
    /// This object is intended to be used in a PUT operation to the API.  Therefore, every field 
    /// on the food truck will be updated with the information in this object.  Therefore, if there is
    /// a field you do not want changed, then you need to populate that field with its current value
    /// in this object
    /// </remarks>
    public class UpdateLocationModel
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
