using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;

namespace FoodTruckNationApi.FoodTrucks
{
    public class CreateFoodTruckModelV11
    {

        /// <summary>
        /// Represents version 1.1 of the create food truck model
        /// </summary>
        public CreateFoodTruckModelV11()
        {
            Tags = new List<string>();
            SocialMediaAccounts = new List<SocialMediaAccountModel>();
        }

        /// <summary>
        /// Gets the name to give to the new Food Truck
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Gets the description of the new food truck
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets the Website of the new food truck
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// Get the locality code of the locality this food truck is in
        /// </summary>
        public string LocalityCode { get; set; }

        /// <summary>
        /// Gets a list of tags to be attached to the new food truck.
        /// </summary>
        /// <remarks>
        /// This list is just a list of strings, so the application has to match these strings up
        /// with tag objects in the system.  Also, some tags may exist, some may not, so it is up
        /// to the application to determine this and treat each tag appropriately.
        /// </remarks>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Gets a list of social media accounts to be attached to the new food truck.
        /// </summary>
        /// <remarks>
        /// This property is included because it is likely on the form that creates a food
        /// truck will collect this information as well, so the client will want to create
        /// a new food truck in one operation, not multiple operations
        /// </remarks>
        public List<SocialMediaAccountModel> SocialMediaAccounts { get; set; }


        #region Nested Classes

        /// <summary>
        /// Represents a Social Media Account for the Food Truck that is being added at creation time of the truck
        /// </summary>
        public class SocialMediaAccountModel
        {

            /// <summary>
            /// The id number of the social media platform this account is for
            /// </summary>
            /// <remarks>
            /// Use the SocialMediaPlatforms endpoint to get the valid ids of Social Media Platforms
            /// </remarks>
            public int SocialMediaPlatformId { get; set; }

            /// <summary>
            /// The account name on this social media platform for the food truck
            /// </summary>
            public string AccountName { get; set; }

        }


        #endregion

    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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

            var socialMediaPlatformsResult = socialMediaPlatformService.GetAllSocialMediaPlatformsAsync().Result;
            var socialMediaPlatforms = (socialMediaPlatformsResult.IsSuccess) ? socialMediaPlatformsResult.Value : new List<SocialMediaPlatform>();

            RuleForEach(f => f.SocialMediaAccounts)
                .Must(sma => !string.IsNullOrWhiteSpace(sma.AccountName))
                .WithMessage("Social media account names cannot be blank or null")
                .Must(sma => socialMediaPlatforms.Any(p => p.PlatformId == sma.SocialMediaPlatformId))
                .WithMessage((model, sma) => $"No social media platform with an id of ${sma.SocialMediaPlatformId} could be found")
                .Must(sma => ValidateAccountNamePassesRegex(sma, socialMediaPlatforms))
                .WithMessage((model, sma) => $"No social media platform with an id of ${sma.SocialMediaPlatformId} could be found");
        }

        internal bool ValidateAccountNamePassesRegex(CreateFoodTruckModelV11.SocialMediaAccountModel sma, IEnumerable<SocialMediaPlatform> platforms)
        {
            var platform = platforms.FirstOrDefault(p => p.PlatformId == sma.SocialMediaPlatformId);
            if (platform != null)
                return platform.IsValidAccountName(sma.AccountName);

            return false;
        }

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class CreateFoodTruckModelV11AutomapperProfile : Profile
    {
        public CreateFoodTruckModelV11AutomapperProfile()
        {
            CreateMap<CreateFoodTruckModelV11, CreateFoodTruckCommand>()
                .ForMember(
                    dest => dest.SocialMediaAccounts,
                    opt => opt.MapFrom(
                        src => src.SocialMediaAccounts
                        .Select(x => new FoodTruckSocialMediaAccountData()
                        {
                            SocialMediaPlatformId = x.SocialMediaPlatformId,
                            AccountName = x.AccountName
                        }).ToList())
                );
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member



}
