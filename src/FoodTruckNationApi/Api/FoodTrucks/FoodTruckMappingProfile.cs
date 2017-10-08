using AutoMapper;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;
using FoodTruckNationApi.Api.FoodTrucks.Reviews;
using FoodTruckNationApi.Api.FoodTrucks.Schedules;
using Framework.ApiUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Api.FoodTrucks
{

    /// <summary>
    /// Defines object maps related to FoodTruck objects
    /// </summary>
    public class FoodTruckMappingProfile : Profile
    {

        public FoodTruckMappingProfile()
        {
            // Query
            this.AddFoodTruckToFoodTruckModelMap();
            this.AddFoodTruckToFoodTruckModelV11Map();

            // Create
            this.AddCreateFoodTruckModelToCreateFoodTruckCommandMap();

            // Update
            this.AddUpdateFoodTruckModelToUpdateFoodTruckCommandMap();
            //this.AddUpdateFoodTruckCommandToFoodTruckMap();
        }



        internal void AddFoodTruckToFoodTruckModelMap()
        {
            this.CreateMap<FoodTruck, FoodTruckModel>()
                .ForMember(
                    dest => dest.Tags,
                    opt => opt.MapFrom(src => src.Tags.Select(t => t.Tag.Text).ToList())
                )
                .ForMember(
                    dest => dest.Meta,
                    opt => opt.MapFrom(src => src)
                );


            // For the links in the Meta object
            this.CreateMap<FoodTruck, FoodTruckLinks>()
                .ForMember(
                    dest => dest.Self,
                    opt => opt.ResolveUsing<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = FoodTrucksController.GET_FOOD_TRUCK_BY_ID,
                            RouteParams = new { id = src.FoodTruckId }
                        }
                    )
                )
                .ForMember(
                    dest => dest.Reviews,
                    opt => opt.ResolveUsing<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = FoodTruckReviewsController.GET_ALL_FOOD_TRUCK_REVIEWS,
                            RouteParams = new { foodTruckId = src.FoodTruckId }
                        }
                    )
                )
                .ForMember(
                    dest => dest.Schedules,
                    opt => opt.ResolveUsing<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = FoodTruckSchedulesController.GET_FOOD_TRUCK_SCHEDULE,
                            RouteParams = new { foodTruckId = src.FoodTruckId }
                        }
                    )
                );
        }



        internal void AddFoodTruckToFoodTruckModelV11Map()
        {
            this.CreateMap<FoodTruck, FoodTruckModelV11>()
                .ForMember(
                    dest => dest.Tags,
                    opt => opt.MapFrom(src => src.Tags.Select(t => t.Tag.Text).ToList())
                )
                .ForMember(
                    dest => dest.SocialMediaAccounts,
                    opt => opt.MapFrom(src => src.SocialMediaAccounts
                        .Select(a => new SocialMediaAccountModelV11()
                            { PlatformName = a.Platform.Name, AccountName = a.AccountName }
                        ).ToList() )
                )
                .ForMember(
                    dest => dest.Meta,
                    opt => opt.MapFrom(src => src)
                );            
        }





        internal void AddCreateFoodTruckModelToCreateFoodTruckCommandMap()
        {
            this.CreateMap<CreateFoodTruckModel, CreateFoodTruckCommand>();
        }


        internal void AddUpdateFoodTruckModelToUpdateFoodTruckCommandMap()
        {
            this.CreateMap<UpdateFoodTruckModel, UpdateFoodTruckCommand>();
        }


        internal void AddUpdateFoodTruckCommandToFoodTruckMap()
        {
            this.CreateMap<UpdateFoodTruckCommand, FoodTruck>()
                .ForMember(
                    dest => dest.Tags,
                    opt => opt.Ignore()
                );                
        }

    }
}
