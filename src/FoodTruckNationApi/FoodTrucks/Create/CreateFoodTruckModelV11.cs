using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.FoodTrucks.Create
{
    public class CreateFoodTruckModelV11
    {

        public CreateFoodTruckModelV11()
        {
            this.Tags = new List<string>();
            this.SocialMediaAccounts = new List<CreateFoodTruckSocialMediaAccountModelV11>();
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


        /// <summary>
        /// Gets a list of social media accounts to be attached to the new food truck.
        /// </summary>
        /// <remarks>
        /// This property is included because it is likely on the form that creates a food
        /// truck will collect this information as well, so the client will want to create
        /// a new food truck in one operation, not multiple operations
        /// </remarks>
        public List<CreateFoodTruckSocialMediaAccountModelV11> SocialMediaAccounts { get; set; }

    }
}
