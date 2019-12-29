using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace FoodTruckNationApi.FoodTrucks.Schedules
{
    /// <summary>
    /// Object to specify optional parameters when getting a food truck's schedule
    /// </summary>
    public class FoodTruckScheduleParameters
    {
        /// <summary>
        /// The starting date (inclusive) of the date range to get the schedule for
        /// </summary>
        public DateTime? StartDate { get; set; }


        /// <summary>
        /// The ending date (including) of the date range to get the schedule for
        /// </summary>
        public DateTime? EndDate { get; set; }

    }



    /// <summary>
    /// Validator class for the FoodTruckScheduleParameters object - the object that contains optional parameters when
    /// getting a Good Trucks Schedule
    /// </summary>
    public class FoodTruckScheduleParametersValidator : AbstractValidator<FoodTruckScheduleParameters>
    {

        /// <summary>
        /// Creates a FoodTruckScheduleParametersValidator object
        /// </summary>
        public FoodTruckScheduleParametersValidator()
        {

            RuleFor(p => p.StartDate)
                .NotNull()
                .When(p => p.EndDate.HasValue)
                .WithMessage("A start date must be provided when an end date is provided");

            RuleFor(p => p.EndDate)
                .NotNull()
                .When(p => p.StartDate.HasValue)
                .WithMessage("An end date must be provided when a start date is provided");

            RuleFor(p => p.EndDate)
                .Must((p, endDate) => endDate.Value > p.StartDate.Value)
                .When(p => p.EndDate.HasValue)
                .When(p => p.StartDate.HasValue)
                .WithMessage("The end date must be after the start date");
        }
    }

}
