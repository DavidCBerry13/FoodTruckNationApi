using FluentValidation;
using FoodTruckNation.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Locations
{
    public class UpdateLocationModelValidator : AbstractValidator<UpdateLocationModel>
    {
        public UpdateLocationModelValidator()
        {

            RuleFor(location => location.Name)
                .NotEmpty().WithMessage("A location must have a name")
                .Matches(Location.NAME_VALIDATION).WithMessage("A location name can only contain the following: letters, numbers, spaces, period, dash, paranthesis, and the single quote character");

            RuleFor(location => location.StreetAddress)
                .NotEmpty().WithMessage("The street address is required")
                .Matches(Location.ADDRESS_VALIDATION).WithMessage("The address can only contain the following characters: letters, numbers, spaces, period, dash and number sign");

            RuleFor(location => location.City)
                .NotEmpty().WithMessage("The city field is required")
                .Matches(Location.CITY_VALIDATION).WithMessage("The city must only contain the following characters: letters, spaces and the period");

            RuleFor(location => location.State)
                .NotEmpty().WithMessage("The two letter state abbreviation must be included")
                .MaximumLength(2).WithMessage("The state code must be a two letter USPS abbreviation")
                .Matches(Location.STATE_VALIDATION).WithMessage("The state code must match a valid USPS state abbreviation");

            RuleFor(location => location.ZipCode)
                .NotEmpty().WithMessage("A five digit USPS zip code must be included")
                .Length(5).WithMessage("The zip code must be exactly 5 digits")
                .Matches(Location.ZIP_CODE_VALIDATION).WithMessage("The zip code must be exactly 5 digits");
        }

    }
}
