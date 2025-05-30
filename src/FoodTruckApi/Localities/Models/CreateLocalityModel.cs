using AutoMapper;
using FluentValidation;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;
using FoodTruckNationApi.FoodTrucks;

namespace FoodTruckApi.Localities.Models
{

    /// <summary>
    /// Schema object to represent the data needed to create a new locality (a city or region)
    /// </summary>
    public class CreateLocalityModel
    {

        /// <summary>
        /// The unique code to be given to this locality
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The name of this Locality
        /// </summary>
        public string Name { get; set; }

    }



    public class CreateLocalityModelValidator : AbstractValidator<CreateLocalityModel>
    {

        public CreateLocalityModelValidator()
        {
            RuleFor(f => f.Code)
                .NotEmpty().WithMessage("A locality code must be provided")
                .Matches(Locality.CODE_VALIDATION).WithMessage("The locality code must be 2-5 upper-case characters");

            RuleFor(f => f.Name)
                .NotEmpty().WithMessage("A locality name must be provided")
                .Matches(Locality.NAME_VALIDATION).WithMessage("The locality name must be less than 30 characters and contain only letters, numbers, spaces");
        }

    }


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class CreateLocalityModelAutomapperProfile : Profile
    {
        public CreateLocalityModelAutomapperProfile()
        {
            CreateMap<CreateLocalityModel, CreateLocalityCommand>();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member


}
