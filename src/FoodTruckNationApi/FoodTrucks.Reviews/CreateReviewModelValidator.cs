using FluentValidation;
using FoodTruckNation.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.FoodTrucks.Reviews
{
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
}
