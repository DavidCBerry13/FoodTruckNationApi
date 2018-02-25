using FluentValidation;
using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.FoodTrucks
{
    /// <summary>
    /// Validator class for Version 1.1 of CreateFoodTruck Models
    /// </summary>
    public class CreateFoodTruckModelV11Validator : AbstractValidator<CreateFoodTruckModelV11>
    {

        public CreateFoodTruckModelV11Validator(ISocialMediaPlatformService socialMediaPlatformService)
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
                .NotNull().WithMessage("An empty tag is not allowed")
                .Matches(Tag.TAG_TEXT_REGEX).WithMessage("Tags can only contain characters and spaces");

            var socialMediaPlatforms = socialMediaPlatformService.GetAllSocialMediaPlatforms();

            RuleForEach(f => f.SocialMediaAccounts)
                .Must(sma => !String.IsNullOrWhiteSpace(sma.AccountName))
                .WithMessage("Social media account names cannot be blank or null")
                .Must(sma => socialMediaPlatforms.Any(p => p.PlatformId == sma.SocialMediaPlatformId))
                .WithMessage((model, sma) => $"No social media platform with an id of ${sma.SocialMediaPlatformId} could be found")
                .Must(sma => this.ValidateAccountNamePassesRegex(sma, socialMediaPlatforms))
                .WithMessage((model, sma) => $"No social media platform with an id of ${sma.SocialMediaPlatformId} could be found");
        }

        internal bool ValidateAccountNamePassesRegex(CreateFoodTruckModelV11.SocialMediaAccountModel sma, List<SocialMediaPlatform> platforms)
        {
            var platform = platforms.FirstOrDefault(p => p.PlatformId == sma.SocialMediaPlatformId);
            if (platform != null)
                return platform.IsValidAccountName(sma.AccountName);

            return false;
        }

    }
}
