using FluentValidation;
using FoodTruckNation.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.FoodTrucks.Base.Create
{
    public class CreateFoodTruckModelValidator : AbstractValidator<CreateFoodTruckModel>
    {

        public CreateFoodTruckModelValidator()
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


            RuleForEach(f => f.Tags)
                .NotNull().WithMessage("Tags cannot be empty")
                .Matches(Tag.TAG_TEXT_REGEX).WithMessage("Tags can only contain characters and spaces");
        }

    }
}
