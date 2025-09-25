using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;

namespace FoodTruckNationApi.Locations
{


    /// <summary>
    /// Represents the information that must be provided when creating a new location where food trucks gather
    /// </summary>
    public class CreateLocationModel
    {

        /// <summary>
        /// The descriptive name to give to this location
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The locality (metro area) code of where this location is
        /// </summary>
        public string LocalityCode { get; set; }

        /// <summary>
        /// The street address of this location
        /// </summary>
        public string StreetAddress { get; set; }

        /// <summary>
        /// The city this location is in
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The two digit state abbreviation of where this lcoation is
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The five digit zip code of this location
        /// </summary>
        public string ZipCode { get; set; }

    }


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public class CreateLocationModelValidator : AbstractValidator<CreateLocationModel>
    {

        public CreateLocationModelValidator()
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


    public class CreateLocationModelAutomapperProfile : Profile
    {
        public CreateLocationModelAutomapperProfile()
        {
            CreateMap<CreateLocationModel, CreateLocationCommand>();
        }
    }

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

}
