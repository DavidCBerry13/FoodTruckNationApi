using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.FoodTrucks
{
    public class CreateFoodTruckModelV11
    {

        /// <summary>
        /// Represents version 1.1 of the create food truck model
        /// </summary>
        public CreateFoodTruckModelV11()
        {
            Tags = new List<string>();
            SocialMediaAccounts = new List<SocialMediaAccountModel>();
        }

        /// <summary>
        /// Gets the name to give to the new Food Truck
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Gets the description of the new food truck
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets the Website of the new food truck
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// Gets a list of tags to be attached to the new food truck.
        /// </summary>
        /// <remarks>
        /// This list is just a list of strings, so the application has to match these strings up
        /// with tag objects in the system.  Also, some tags may exist, some may not, so it is up
        /// to the application to determine this and treat each tag appropriately.
        /// </remarks>
        public List<string> Tags { get; set; }


        /// <summary>
        /// Gets a list of social media accounts to be attached to the new food truck.
        /// </summary>
        /// <remarks>
        /// This property is included because it is likely on the form that creates a food
        /// truck will collect this information as well, so the client will want to create
        /// a new food truck in one operation, not multiple operations
        /// </remarks>
        public List<SocialMediaAccountModel> SocialMediaAccounts { get; set; }


        #region Nested Classes

        /// <summary>
        /// Represents a Social Media Account for the Food Truck that is being added at creation time of the truck
        /// </summary>
        public class SocialMediaAccountModel
        {

            /// <summary>
            /// The id number of the social media platform this account is for
            /// </summary>
            /// <remarks>
            /// Use the SocialMediaPlatforms endpoint to get the valid ids of Social Media Platforms
            /// </remarks>
            public int SocialMediaPlatformId { get; set; }

            /// <summary>
            /// The account name on this social media platform for the food truck
            /// </summary>
            public string AccountName { get; set; }

        }


        #endregion


    }
}
