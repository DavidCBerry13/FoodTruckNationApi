using AutoMapper;
using FluentValidation;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;
using FoodTruckNationApi.FoodTrucks;

namespace FoodTruckApi.Localities.Models
{
    /// <summary>
    /// Schema object to represent the fields that can be updated on a Locality
    /// </summary>
    public class UpdateLocalityModel
    {

        /// <summary>
        /// The name for the locality
        /// </summary>
        public string Name { get; set; }

    }


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class UpdateLocalityModelValidator : AbstractValidator<UpdateLocalityModel>
    {
        public UpdateLocalityModelValidator()
        {
            RuleFor(f => f.Name)
                .NotEmpty().WithMessage("A locality name must be provided")
                .Matches(Locality.NAME_VALIDATION).WithMessage("The locality name must be less than 30 characters and contain only letters, numbers, spaces");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class UpdateLocalityModelAutomapperProfile : Profile
    {
        public UpdateLocalityModelAutomapperProfile()
        {
            CreateMap<UpdateLocalityModel, UpdateLocalityCommand>();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member


}
