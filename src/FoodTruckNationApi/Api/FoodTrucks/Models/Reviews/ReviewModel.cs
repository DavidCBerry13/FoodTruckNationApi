using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Api.FoodTrucks.Models
{

    /// <summary>
    /// Represents the review of a food truck as returned by the API
    /// </summary>
    public class ReviewModel
    {

        /// <summary>
        /// The unique id number of this review
        /// </summary>
        public int ReviewId { get; set; }

        /// <summary>
        /// The id number of the food truck this review is for
        /// </summary>
        public int FoodTruckId { get; set; }

        /// <summary>
        /// The date this review was submitted
        /// </summary>
        public DateTime ReviewDate { get; set; }

        /// <summary>
        /// The rating (1-5) of this review
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Additional comments left on the review
        /// </summary>
        public String Comments { get; set; }

        /// <summary>
        /// Metadata object containing the URL links to related information for this review
        /// </summary>
        public ReviewLinks Meta { get; set; }

    }


    /// <summary>
    /// Represents URL links to related data in the API for a review
    /// </summary>
    public class ReviewLinks
    {
        /// <summary>
        /// URL link the this individual review
        /// </summary>
        public String Self { get; set; }

        /// <summary>
        /// URL link to the Food Truck the review is for
        /// </summary>
        public String FoodTruck { get; set; }
    }

}
