using AutoMapper;
using FoodTruckNation.Core.Domain;
using FoodTruckNationApi.FoodTrucks.Schedules;
using Framework.ApiUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Schedules
{
    public class SchedulesMappingProfile : Profile
    {

        public SchedulesMappingProfile()
        {
            ScheduleToScheduleModel();
            FoodtruckToScheduleFoodTruckModel();
            LocationToScheduleLocationModel();
        }


        internal void ScheduleToScheduleModel()
        {
            this.CreateMap<Schedule, ScheduleModel>()
                .ForMember(
                    dest => dest.FoodTruck,
                    opt => opt.MapFrom(src => src.FoodTruck)
                )
                .ForMember(
                    dest => dest.Location,
                    opt => opt.MapFrom(src => src.Location)
                );
        }


        internal void FoodtruckToScheduleFoodTruckModel()
        {
            // FoodTruck Truck
            this.CreateMap<FoodTruck, ScheduleModel.FoodTruckModel>()
                .ForMember(
                    dest => dest.Tags,
                    opt => opt.MapFrom(src => src.Tags.Select(t => t.Tag.Text).ToList())
                )
                .ForMember(
                    dest => dest.Meta,
                    opt => opt.MapFrom(src => src)
                );


            // For the links in the Meta object
            this.CreateMap<FoodTruck, ScheduleModel.FoodTruckLinks>()
                .ForMember(
                    dest => dest.Self,
                    opt => opt.ResolveUsing<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = FoodTrucks.FoodTrucksController.GET_FOOD_TRUCK_BY_ID,
                            RouteParams = new { id = src.FoodTruckId }
                        }
                    )
                )
                .ForMember(
                    dest => dest.Reviews,
                    opt => opt.ResolveUsing<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = FoodTrucks.Reviews.FoodTruckReviewsController.GET_ALL_FOOD_TRUCK_REVIEWS,
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


        internal void LocationToScheduleLocationModel()
        {
            this.CreateMap<Location, ScheduleModel.LocationModel>()
                .ForMember(
                    dest => dest.LocationName,
                    opt => opt.MapFrom(src => src.Name)
                )
                .ForMember(
                    dest => dest.Meta,
                    opt => opt.MapFrom(src => src)
                );


            // For the links in the Meta object
            this.CreateMap<Location, ScheduleModel.LocationLinks>()
                .ForMember(
                    dest => dest.Self,
                    opt => opt.ResolveUsing<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = Locations.LocationsController.GET_LOCATION_BY_ID,
                            RouteParams = new { id = src.LocationId }
                        }
                    )
                )
                .ForMember(
                    dest => dest.Schedules,
                    opt => opt.ResolveUsing<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = Locations.Schedules.LocationSchedulesController.GET_ALL_SCHEDULES_FOR_LOCATION,
                            RouteParams = new { locationId = src.LocationId }
                        }
                    )
                );

        }


    }
}
