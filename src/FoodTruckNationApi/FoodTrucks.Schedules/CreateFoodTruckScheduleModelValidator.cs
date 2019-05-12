using FluentValidation;
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.FoodTrucks.Schedules
{
    public class CreateFoodTruckScheduleModelValidator : AbstractValidator<CreateFoodTruckScheduleModel>
    {

        public CreateFoodTruckScheduleModelValidator(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;

            RuleFor(s => s.EndTime)
                .GreaterThan(s => s.StartTime)
                .WithMessage("The end time must be greater than the start time");

            RuleFor(s => s.StartTime)
                .Must(startTime => startTime >= _dateTimeProvider.CurrentDateTime).WithMessage("The start time must be after the current date/time");

            RuleFor(s => s.EndTime)
                .Must((s, endTime) => endTime.Subtract(s.StartTime).TotalHours <= 16)
                .WithMessage("The maximum time to schedule a food truck at a location is 16 hours");
        }

        private readonly IDateTimeProvider _dateTimeProvider;

    }
}
