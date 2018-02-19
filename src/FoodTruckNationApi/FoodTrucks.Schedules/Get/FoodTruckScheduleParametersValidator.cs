using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.FoodTrucks.Schedules.Get
{
    public class FoodTruckScheduleParametersValidator : AbstractValidator<FoodTruckScheduleParameters>
    {

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
                .Must((p, endDate) => endDate.Value > p.StartDate.Value )
                .When(p => p.EndDate.HasValue)
                .When(p => p.StartDate.HasValue)
                .WithMessage("The end date must be after the start date");


        }




    }
}
