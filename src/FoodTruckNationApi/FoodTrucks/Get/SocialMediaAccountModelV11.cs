using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.FoodTrucks.Base.Get
{
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
}
