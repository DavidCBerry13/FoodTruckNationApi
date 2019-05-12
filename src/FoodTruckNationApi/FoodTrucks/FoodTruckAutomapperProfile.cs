#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using AutoMapper;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;
using Framework.ApiUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.FoodTrucks
{
    public class FoodTruckAutomapperProfile : Profile
    {

        public FoodTruckAutomapperProfile()
        {
            AddFoodTruckModelMaps();
            AddFoodTruckModelV11Maps();
            AddCreateFoodTruckModelMaps();
            AddUpdateFoodTruckModelMaps();
        }


        internal void AddFoodTruckModelMaps()
        {
            CreateMap<FoodTruck, FoodTruckModel>()
                .ForMember(
                    dest => dest.Tags,
                    opt => opt.MapFrom(src => src.Tags.Select(t => t.Tag.Text).ToList())
                )
                .ForMember(
                    dest => dest.ReviewAverage,
                    opt => opt.MapFrom(src => Math.Round(src.ReviewAverage, 2))
                )
                .ForMember(
                    dest => dest.Meta,
                    opt => opt.MapFrom(src => src)
                );


            // For the links in the Meta object
            CreateMap<FoodTruck, FoodTruckModel.FoodTruckLinks>()
                .ForMember(
                    dest => dest.Self,
                    opt => opt.MapFrom<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = FoodTrucksController.GET_FOOD_TRUCK_BY_ID,
                            RouteParams = new { id = src.FoodTruckId }
                        }
                    )
                )
                .ForMember(
                    dest => dest.Reviews,
                    opt => opt.MapFrom<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = Reviews.FoodTruckReviewsController.GET_ALL_FOOD_TRUCK_REVIEWS,
                            RouteParams = new { foodTruckId = src.FoodTruckId }
                        }
                    )
                )
                .ForMember(
                    dest => dest.Schedules,
                    opt => opt.MapFrom<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = Schedules.FoodTruckSchedulesController.GET_FOOD_TRUCK_SCHEDULE,
                            RouteParams = new { foodTruckId = src.FoodTruckId }
                        }
                    )
                );
        }



        internal void AddFoodTruckModelV11Maps()
        {
            CreateMap<FoodTruck, FoodTruckModelV11>()
                .ForMember(
                    dest => dest.Tags,
                    opt => opt.MapFrom(src => src.Tags.Select(t => t.Tag.Text).ToList())
                )
                .ForMember(
                    dest => dest.SocialMediaAccounts,
                    opt => opt.MapFrom(src => src.SocialMediaAccounts
                        .Select(a => new FoodTruckModelV11.SocialMediaAccountModel()
                        { PlatformName = a.Platform.Name, AccountName = a.AccountName }
                        ).ToList())
                )
                .ForMember(
                    dest => dest.ReviewAverage,
                    opt => opt.MapFrom(src => Math.Round(src.ReviewAverage, 2))
                )
                .ForMember(
                    dest => dest.Meta,
                    opt => opt.MapFrom(src => src)
                );

            // For the links in the Meta object
            CreateMap<FoodTruck, FoodTruckModelV11.FoodTruckLinks>()
                .ForMember(
                    dest => dest.Self,
                    opt => opt.MapFrom<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = FoodTrucksController.GET_FOOD_TRUCK_BY_ID,
                            RouteParams = new { id = src.FoodTruckId }
                        }
                    )
                )
                .ForMember(
                    dest => dest.Reviews,
                    opt => opt.MapFrom<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = Reviews.FoodTruckReviewsController.GET_ALL_FOOD_TRUCK_REVIEWS,
                            RouteParams = new { foodTruckId = src.FoodTruckId }
                        }
                    )
                )
                .ForMember(
                    dest => dest.Schedules,
                    opt => opt.MapFrom<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = Schedules.FoodTruckSchedulesController.GET_FOOD_TRUCK_SCHEDULE,
                            RouteParams = new { foodTruckId = src.FoodTruckId }
                        }
                    )
                );

        }


        internal void AddCreateFoodTruckModelMaps()
        {
            CreateMap<CreateFoodTruckModel, CreateFoodTruckCommand>();

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


        internal void AddUpdateFoodTruckModelMaps()
        {
            CreateMap<UpdateFoodTruckModel, UpdateFoodTruckCommand>();        
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member