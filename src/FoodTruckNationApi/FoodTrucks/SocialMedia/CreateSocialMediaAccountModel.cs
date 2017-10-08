using FoodTruckNation.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Api.FoodTrucks.SocialMedia
{

    /// <summary>
    /// Represents the data needed to create a social media account for a food truck
    /// </summary>
    /// <remarks>
    /// The id of the food truck the social media account is for is passed in as part of
    /// the URL (in the route), so it is not present in this object that will be POSTed
    /// to the API
    /// </remarks>
    public class CreateSocialMediaAccountModel
    {

        /// <summary>
        /// The id number of the social media platform the account is for
        /// </summary>
        [Required]
        public int SocialMediaPlatformId { get; set; }

        /// <summary>
        /// The social media account name used by the food truck on this platform
        /// </summary>
        [Required]
        public String AccountName { get; set; }

    }
}
