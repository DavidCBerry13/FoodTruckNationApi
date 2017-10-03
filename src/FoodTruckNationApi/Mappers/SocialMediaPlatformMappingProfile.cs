using AutoMapper;
using FoodTruckNation.Core.Domain;
using FoodTruckNationApi.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Mappers
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
