using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodTruckNationApi.FoodTrucks.Base.Create
{


    /// <summary>
    /// Model class representing the data needed to create a new FoodTruck
    /// </summary>
    public class CreateFoodTruckModel
    {

        public CreateFoodTruckModel()
        {
            this.Tags = new List<string>();
        }

        /// <summary>
        /// Gets the name to give to the new Food Truck
        /// </summary>
        public String Name { get; set; }


        /// <summary>
        /// Gets the description of the new food truck
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// Gets the Website of the new food truck
        /// </summary>
        public String Website { get; set; }

        /// <summary>
        /// Gets a list of tags to be attached to the new food truck.
        /// </summary>
        /// <remarks>
        /// This list is just a list of strings, so the application has to match these strings up
        /// with tag objects in the system.  Also, some tags may exist, some may not, so it is up
        /// to the application to determine this and treat each tag appropriately.
        /// </remarks>
        public List<String> Tags { get; set; }

    }

}
