using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;

namespace FoodTruckNationApi.FoodTrucks.Reviews
{

    /// <summary>
    /// Represents the data that needs to be submitted to create a review for a food truck
    /// </summary>
    /// <remarks>
    /// Note that this 
    /// </remarks>
    public class CreateReviewModel
    {
       
        /// <summary>
        /// The overall rating on a scale of 1 through 5
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Additional comments to include with the review
        /// </summary>
        public string Comments { get; set; }

    }


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class CreateReviewModelValidator : AbstractValidator<CreateReviewModel>
    {
        public CreateReviewModelValidator()
        {

            RuleFor(r => r.Rating)
                .NotEmpty().WithMessage("A rating value of 1 through 5 must be supplied")
                .InclusiveBetween(1, 5).WithMessage("The rating value must be between 1 and 5");

            RuleFor(r => r.Comments)
                .Matches(Review.COMMENTS_VALIDATION).WithMessage("");

        }
    }



    public class CreateReviewModelAutomapperProfile : Profile
    {
        public CreateReviewModelAutomapperProfile()
        {
            CreateMap<CreateReviewModel, CreateReviewCommand>();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member



}
