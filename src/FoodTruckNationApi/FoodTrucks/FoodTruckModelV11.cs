using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.FoodTrucks
{
    /// <summary>
    /// Version 1.1 of the food ruck model.  This version includes social media
    /// account for the food truck
    /// </summary>
    public class FoodTruckModelV11
    {
        /// <summary>
        /// The unique id number of this food truck.  This is the id used to get a single food truck on get by id API calls
        /// </summary>
        public int FoodTruckId { get; set; }

        /// <summary>
        /// The name of the food truck
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// A text description of the offerings of the food truck
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// The website for the food truck
        /// </summary>
        public String Website { get; set; }

        /// <summary>
        /// The last time this food truck object was modified.  This is needed when updating the object
        /// </summary>
        public DateTime LastModifiedDate { get; set; }

        /// <summary>
        /// A list of tags associated with the food truck.  This list is just a string list of the tag text
        /// </summary>
        public List<String> Tags { get; set; }

        /// <summary>
        /// List of social media accounts for this food truck
        /// </summary>
        public List<SocialMediaAccountModel> SocialMediaAccounts { get; set; }

        /// <summary>
        /// Meta data object containing associated links for the food truck
        /// </summary>
        public FoodTruckLinks Meta { get; set; }


        #region Nested Types


        /// <summary>
        /// Class to encapsulate the links (urls) for a food truck
        /// </summary>
        public class FoodTruckLinks
        {
            /// <summary>
            /// Gets the URL that refers to this food truck
            /// </summary>
            public String Self { get; set; }

            /// <summary>
            /// Gets the url for the reviews of this food truck
            /// </summary>
            public String Reviews { get; set; }

            /// <summary>
            /// Gets the url for the schedules of this food truck
            /// </summary>
            public String Schedules { get; set; }
        }




        /// <summary>
        /// Represents the social media account for a food truck
        /// </summary>
        public class SocialMediaAccountModel
        {
            /// <summary>
            /// The social media platform this account is for (Facebook, Twitter, etc)
            /// </summary>
            public String PlatformName { get; set; }

            /// <summary>
            /// The account name on the platform for this food truck
            /// </summary>
            public String AccountName { get; set; }

        }

        #endregion

    }
}
