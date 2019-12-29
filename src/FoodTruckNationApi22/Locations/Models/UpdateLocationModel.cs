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
    /// Model used to update the properties of a location
    /// </summary>
    /// <remarks>
    /// This object is intended to be used in a PUT operation to the API.  Therefore, every field 
    /// on the food truck will be updated with the information in this object.  Therefore, if there is
    /// a field you do not want changed, then you need to populate that field with its current value
    /// in this object
    /// </remarks>
    public class UpdateLocationModel
    {

        /// <summary>
        /// The descriptive name to give to this location
        /// </summary>
        public string Name { get; set; }

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


    public class UpdateLocationModelAutomapperProfile : Profile
    {
        public UpdateLocationModelAutomapperProfile()
        {
            CreateMap<UpdateLocationModel, UpdateLocationCommand>();
        }
    }

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member



}
