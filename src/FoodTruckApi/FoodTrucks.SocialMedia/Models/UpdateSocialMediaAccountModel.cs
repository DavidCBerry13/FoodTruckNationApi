using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodTruckNationApi.Api.FoodTrucks.SocialMedia
{
    /// <summary>
    /// Model object used to update a social media account for a food truck
    /// </summary>
    public class UpdateSocialMediaAccount
    {

        /// <summary>
        /// The new account name for this social media account
        /// </summary>
        public string AccountName { get; set; }
    }
}
