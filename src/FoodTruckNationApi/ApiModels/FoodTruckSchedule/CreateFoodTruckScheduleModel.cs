using Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.ApiModels
{
    public class CreateFoodTruckScheduleModel : IValidatableObject
    {

        public CreateFoodTruckScheduleModel(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }


        private IDateTimeProvider _dateTimeProvider;


        /// <summary>
        /// The id of the location where the food truck will be at this time
        /// </summary>
        [Required]
        public int LocationId { get; set; }

        /// <summary>
        /// The date/time the food truck will arrive
        /// </summary>
        [Required]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// The date/time the food truck will leave
        /// </summary>
        [Required]
        public DateTime EndTime { get; set; }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndTime < StartTime)
            {
                yield return
                  new ValidationResult(errorMessage: "End Date must be greater than Start Date",
                                       memberNames: new[] { "EndDate" });
            }

            if ( EndTime.Subtract(StartTime).TotalHours >= 24)
            {
                yield return
                  new ValidationResult(errorMessage: "A food truck cannot be schedule for more than 24 hours in one schedule item",
                                       memberNames: new[] { "EndDate" });
            }

        }
    }
}
