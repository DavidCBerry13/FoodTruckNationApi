using FoodTruckNation.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Api.Locations.Models
{


    /// <summary>
    /// Represents the information that must be provided when creating a new location where food trucks gather
    /// </summary>
    public class CreateLocationModel
    {

        /// <summary>
        /// The descriptive name to give to this location
        /// </summary>
        [Required]
        [RegularExpression(Location.NAME_VALIDATION)]
        public String Name { get; set; }

        /// <summary>
        /// The street address of this location
        /// </summary>
        [Required]
        [RegularExpression(Location.ADDRESS_VALIDATION)]
        public String StreetAddress { get; set; }

        /// <summary>
        /// The city this location is in
        /// </summary>
        [Required]
        [RegularExpression(Location.CITY_VALIDATION)]
        public String City { get; set; }

        /// <summary>
        /// The two digit state abbreviation of where this lcoation is
        /// </summary>
        [Required]
        [RegularExpression(Location.STATE_VALIDATION)]
        public String State { get; set; }

        /// <summary>
        /// The five digit zip code of this location
        /// </summary>
        [Required]
        [RegularExpression(Location.ZIP_CODE_VALIDATION)]
        public String ZipCode { get; set; }

    }
}
