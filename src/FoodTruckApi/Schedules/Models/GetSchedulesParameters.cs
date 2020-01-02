using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace FoodTruckNationApi.Schedules
{

    /// <summary>
    /// Represents the query string parameters that can be passed to the Get operation
    /// on the Schedules controller
    /// </summary>
    /// <remarks>
    /// These parameters are encapsulated into an object rather than just being
    /// method parameters because then we can use Model State Validation to validate
    /// any passed parameters are correct rather than doing that validation by hand
    /// in the operation method itself
    /// </remarks>
    public class GetSchedulesParameters
    {

        /// <summary>
        /// Optional start date of the date range to get schedules for
        /// </summary>
        /// <remarks>
        /// If you provide a start date, then you must also provide an end date
        /// </remarks>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Optional end date of the date range to get schedules for
        /// </summary>
        /// <remarks>
        /// If you provide an end date, then a start fate must also be provided
        /// </remarks>
        public DateTime? EndDate { get; set; }

    }


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <summary>
    /// Validator for GetSchedulesParameters object
    /// </summary>
    /// <remarks>
    /// If a start date or end date is provided, then both must be provided
    /// If provided, then end date must be >= start date
    /// Maximum range is 60 days
    /// </remarks>
    public class GetSchedulesParametersValidator : AbstractValidator<GetSchedulesParameters>
    {

        public GetSchedulesParametersValidator()
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
