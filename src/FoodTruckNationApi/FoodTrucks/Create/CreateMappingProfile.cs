using AutoMapper;
using FoodTruckNation.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.FoodTrucks.Create
{
    public class CreateMappingProfile : Profile
    {

        public CreateMappingProfile()
        {
            this.CreateMap<CreateFoodTruckModel, CreateFoodTruckCommand>();

            this.CreateMap<CreateFoodTruckModelV11, CreateFoodTruckCommand>()
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
}
