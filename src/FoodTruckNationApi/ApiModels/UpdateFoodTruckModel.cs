using FoodTruckNation.Core.Domain;
using Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.ApiModels
{
    /// <summary>
    /// Model used to update the properties of a Food Truck
    /// </summary>
    /// <remarks>
    /// This object is intended to be used in a PUT operation to the API.  Therefore, every field 
    /// on the food truck will be updated with the information in this object.  Therefore, if there is
    /// a field you do not want changed, then you need to populate that field with its current value
    /// in this object
    /// </remarks>
    public class UpdateFoodTruckModel
    {

        /// <summary>
        /// The name to give a food truck
        /// </summary>
        [RegularExpression(FoodTruck.NAME_VALIDATION)]
        public String Name { get; set; }

        /// <summary>
        /// The description of the food truck
        /// </summary>
        [RegularExpression(FoodTruck.DESCRIPTION_VALIDATION)]
        public String Description { get; set; }

        /// <summary>
        /// The website of the food truck
        /// </summary>
        [Required]
        [RegularExpression(FoodTruck.WEBSITE_VALIDATION)]
        public String Website { get; set; }

    }
}
