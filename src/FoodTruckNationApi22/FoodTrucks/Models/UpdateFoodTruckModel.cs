using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;

namespace FoodTruckNationApi.FoodTrucks
{
    /// <summary>
    /// Model used to update the properties of a Food Truck
    /// </summary>
    /// <remarks>
    /// This object is intended to be used in a PUT operation to the API.  Therefore, every field 
    /// on the food truck will be updated with the information in this object.  Therefore, if there is
    /// a field you do not want changed, then you need to populate that field with its current value
    /// in this object
    /// </remarks>
    public class UpdateFoodTruckModel
    {

        /// <summary>
        /// The name to give a food truck
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the food truck
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The website of the food truck
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// The last time this food truck object was modified.
        /// </summary>
        /// <remarks>
        /// This value is required so the API can perform a concurrency check on the object
        /// being updated and make sure it has not changed since fetched by the client
        /// </remarks>
        public DateTime LastModifiedDate { get; set; }


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public class UpdateFoodTruckModelValidator : AbstractValidator<UpdateFoodTruckModel>
        {
            public UpdateFoodTruckModelValidator()
            {
                RuleFor(f => f.Name)
                    .NotEmpty().WithMessage("A food truck name must be provided")
                    .Matches(FoodTruck.NAME_VALIDATION).WithMessage("The food truck name must be less than 40 characters and contain only letters, numbers, spaces");

                RuleFor(f => f.Description)
                    .NotEmpty().WithMessage("The food truck must have a description")
                    .Matches(FoodTruck.DESCRIPTION_VALIDATION).WithMessage("The description may only contain the following characters: letters, numbers, spaces, dash, period, comma and single quote");

                RuleFor(f => f.Website)
                    .NotEmpty().WithMessage("The food truck must have a website")
                    .Matches(FoodTruck.WEBSITE_VALIDATION).WithMessage("You must input a valid website url");
            }
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public class UpdateFoodTruckModelAutomapperProfile : Profile
        {
            public UpdateFoodTruckModelAutomapperProfile()
            {
                CreateMap<UpdateFoodTruckModel, UpdateFoodTruckCommand>();
            }
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member


    }
}
