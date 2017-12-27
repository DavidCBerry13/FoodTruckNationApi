using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodTruckNationApi.FoodTrucks.Base.Update
{
    /// <summary>
    /// Model used to update the properties of a Food Truck
    /// </summary>
    /// <remarks>
    /// This object is intended to be used in a PUT operation to the API.  Therefore, every field 
    /// on the food truck will be updated with the information in this object.  Therefore, if there is
    /// a field you do not want changed, then you need to populate that field with its current value
    /// in this object
    /// </remarks>
    public class UpdateFoodTruckModel
    {

        /// <summary>
        /// The name to give a food truck
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// The description of the food truck
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// The website of the food truck
        /// </summary>
        public String Website { get; set; }

        /// <summary>
        /// The last time this food truck object was modified.
        /// </summary>
        /// <remarks>
        /// This value is required so the API can perform a concurrency check on the object
        /// being updated and make sure it has not changed since fetched by the client
        /// </remarks>
        public DateTime LastModifiedDate { get; set; }

    }
}
