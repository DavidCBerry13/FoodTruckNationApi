using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using FoodTruckNation.Core.Commands;
using DavidBerry.Framework.TimeAndDate;

namespace FoodTruckNationApi.FoodTrucks.Schedules
{

    /// <summary>
    /// Defines the data needed to schedule a food truck to be at a certain location at a certain time
    /// </summary>
    public class CreateFoodTruckScheduleModel
    {

        /// <summary>
        /// The id of the location where the food truck will be at this time
        /// </summary>
        public int LocationId { get; set; }

        /// <summary>
        /// The date/time the food truck will arrive
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// The date/time the food truck will leave
        /// </summary>
        public DateTime EndTime { get; set; }

    }


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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


    public class CreateFoodTruckScheduleModelAutomapperProfile : Profile
    {
        public CreateFoodTruckScheduleModelAutomapperProfile()
        {
            CreateMap<CreateFoodTruckScheduleModel, CreateFoodTruckScheduleCommand>();
        }
    }




#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

}
