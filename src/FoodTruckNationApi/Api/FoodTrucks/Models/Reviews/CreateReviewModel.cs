using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Api.FoodTrucks.Models
{

    /// <summary>
    /// Represents the data that needs to be submitted to create a review for a food truck
    /// </summary>
    /// <remarks>
    /// Note that this 
    /// </remarks>
    public class CreateReviewModel
    {
       
        /// <summary>
        /// The overall rating on a scale of 1 through 5
        /// </summary>
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        /// <summary>
        /// Additional comments to include with the review
        /// </summary>
        public String Comments { get; set; }

    }
}
