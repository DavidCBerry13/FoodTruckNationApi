using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoodTruckNation.Core.Domain;

namespace FoodTruckNationApi.SocialMediaPlatforms
{

    /// <summary>
    /// Represents a social media platform a food truck may have an account on
    /// </summary>
    public class SocialMediaPlatformModel
    {

        /// <summary>
        /// Unique id number for this social media platform.
        /// </summary>
        /// <remarks>
        /// When adding a new social media account to a Food Truck, this is the id number
        /// passed with the request to identify the type of account being added
        /// </remarks>
        public int SocialMediaPlatformId { get; set; }

        /// <summary>
        /// The name of the Social Media Platform (Facebook, Twitter, Instagram, etc)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The regex that encapsulates the rules around an account name on this platform
        /// </summary>
        public string AccountNameRegex { get; set; }

    }


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class SocialMediaPlatformModelAutomapperProfile : Profile
    {
        public SocialMediaPlatformModelAutomapperProfile()
        {
            CreateMap<SocialMediaPlatform, SocialMediaPlatformModel>()
                .ForMember(
                dest => dest.SocialMediaPlatformId,
                opt => opt.MapFrom(src => src.PlatformId)
                );
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member


}
