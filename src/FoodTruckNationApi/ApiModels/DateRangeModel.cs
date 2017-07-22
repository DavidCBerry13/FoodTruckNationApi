using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.ApiModels
{
    public class DateRangeModel : IValidatableObject
    {


        public DateTime? StartDate { get; set; }


        public DateTime? EndDate { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate < StartDate)
            {
                yield return
                  new ValidationResult(errorMessage: "End Date must be greater than Start Date",
                                       memberNames: new[] { "EndDate" });
            }
        }

    }
}
