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
    /// Model class representing the data needed to create a new FoodTruck
    /// </summary>
    public class CreateFoodTruckModel
    {

        /// <summary>
        /// Gets the name to give to the new Food Truck
        /// </summary>
        [Required]
        [RegularExpression(FoodTruck.NAME_VALIDATION)]
        public String Name { get; set; }


        /// <summary>
        /// Gets the description of the new food truck
        /// </summary>
        [Required]
        [RegularExpression(FoodTruck.DESCRIPTION_VALIDATION)]
        public String Description { get; set; }

        /// <summary>
        /// Gets the Website of the new food truck
        /// </summary>
        [Required]
        [RegularExpression(FoodTruck.WEBSITE_VALIDATION)]
        public String Website { get; set; }

        /// <summary>
        /// Gets a list of tags to be attached to the new food truck.
        /// </summary>
        /// <remarks>
        /// This list is just a list of strings, so the application has to match these strings up
        /// with tag objects in the system.  Also, some tags may exist, some may not, so it is up
        /// to the application to determine this and treat each tag appropriately.
        /// </remarks>
        [StringCollectionValidationAttribute(Tag.TAG_TEXT_REGEX)]
        public List<String> Tags { get; set; }


    }
}
