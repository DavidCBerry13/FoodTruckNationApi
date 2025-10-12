using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Api.FoodTrucks.SocialMedia
{

    /// <summary>
    /// Represents a social media account associated with a food truck - Version 1.1
    /// </summary>
    public class SocialMediaAccountModelV11
    {
        /// <summary>
        /// The name of the platform the social media account is for
        /// </summary>
        public string PlatformName { get; set; }

        /// <summary>
        /// The name of the account on the social media platform
        /// </summary>
        public string AccountName { get; set; }

    }
}
