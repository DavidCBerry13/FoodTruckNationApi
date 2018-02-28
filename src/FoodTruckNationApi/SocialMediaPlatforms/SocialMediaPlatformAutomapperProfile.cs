using AutoMapper;
using FoodTruckNation.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.SocialMediaPlatforms
{
    /// <summary>
    /// Class defining mappings used in the SocialMediaPlatforms feature
    /// </summary>
    public class SocialMediaPlatformAutomapperProfile : Profile
    {
        public SocialMediaPlatformAutomapperProfile()
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
