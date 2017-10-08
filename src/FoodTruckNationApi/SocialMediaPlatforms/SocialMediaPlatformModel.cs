using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.SocialMediaPlatforms
{

    /// <summary>
    /// Represents a social media platform a food truck may have an account on
    /// </summary>
    public class SocialMediaPlatformModel
    {

        /// <summary>
        /// Unique id number for this social media platform.
        /// </summary>
        /// <remarks>
        /// When adding a new social media account to a Food Truck, this is the id number
        /// passed with the request to identify the type of account being added
        /// </remarks>
        public int SocialMediaPlatformId { get; set; }

        /// <summary>
        /// The name of the Social Media Platform (Facebook, Twitter, Instagram, etc)
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// The regex that encapsulates the rules around an account name on this platform
        /// </summary>
        public String AccountNameRegex { get; set; }

    }
}
