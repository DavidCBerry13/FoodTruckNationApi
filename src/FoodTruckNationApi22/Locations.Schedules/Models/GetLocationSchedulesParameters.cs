using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace FoodTruckNationApi.Locations.Schedules
{

    /// <summary>
    /// Represents the parameters that can be passed to the GET operation of the 
    /// Locations/{locationId}/Schedules end point
    /// </summary>
    public class GetLocationSchedulesParameters
    {

        /// <summary>
        /// Optional start time for the date range to get schedules for
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Optional end time for the date range to get schedules for
        /// </summary>
        public DateTime? EndDate { get; set; }

    }


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public class GetLocationSchedulesParametersValidator : AbstractValidator<GetLocationSchedulesParameters>
    {

        public GetLocationSchedulesParametersValidator()
        {

            RuleFor(p => p.EndDate)
                .NotEmpty().WithMessage("An end date must be provided when a start date is provided")
                .When(p => p.StartDate.HasValue);

            RuleFor(p => p.StartDate)
                .NotEmpty().WithMessage("A start date must be provided when an end date is provided")
                .When(p => p.EndDate.HasValue);

            // When start and end date provided, end date >= start date
            RuleFor(p => p.EndDate)
                .Must((p, endDate) => endDate.Value.Date >= p.StartDate.Value.Date)
                .WithMessage("The end date must be on or after the start date")
                .When(p => p.StartDate.HasValue)
                .When(p => p.EndDate.HasValue);

            RuleFor(p => p.EndDate)
                .Must((p, endDate) => endDate.Value.Date.Subtract(p.StartDate.Value.Date).TotalDays <= MAX_DAY_RANGE)
                .WithMessage($"A maximum date range of {MAX_DAY_RANGE} days is allowed")
                .When(p => p.StartDate.HasValue)
                .When(p => p.EndDate.HasValue);
        }


        /// <summary>
        /// Maximum date range (in days) that can be specified to get schedules for
        /// </summary>
        public const int MAX_DAY_RANGE = 60;
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member



}
