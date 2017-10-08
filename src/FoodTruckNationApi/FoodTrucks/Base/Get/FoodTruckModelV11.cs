using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.FoodTrucks.Base.Get
{
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
        /// A list of tags associated with the food truck.  This list is just a string list of the tag text
        /// </summary>
        public List<String> Tags { get; set; }

        public List<SocialMediaAccountModelV11> SocialMediaAccounts { get; set; }

        /// <summary>
        /// Meta data object containing associated links for the food truck
        /// </summary>
        public FoodTruckLinks Meta { get; set; }
    }
}
