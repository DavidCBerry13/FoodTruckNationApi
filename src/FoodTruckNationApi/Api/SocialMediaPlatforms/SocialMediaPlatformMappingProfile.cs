using AutoMapper;
using FoodTruckNation.Core.Domain;
using FoodTruckNationApi.Api.SocialMediaPlatforms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Api.SocialMediaPlatforms
{
    /// <summary>
    /// Class defining mappings used in the SocialMediaPlatforms feature
    /// </summary>
    public class SocialMediaPlatformMappingProfile : Profile
    {
        public SocialMediaPlatformMappingProfile()
        {
            this.AddSocialMediaPlatformToSocialMediaPlatformModelMapping();
        }


        public void AddSocialMediaPlatformToSocialMediaPlatformModelMapping()
        {

            this.CreateMap<SocialMediaPlatform, SocialMediaPlatformModel>()
                .ForMember(
                    dest => dest.SocialMediaPlatformId,
                    opt => opt.MapFrom(src => src.PlatformId)
                );
        }

    }
}
