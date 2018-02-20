using AutoMapper;
using FoodTruckNation.Core.Domain;
using Framework.ApiUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Locations.Schedules
{
    public class LocationSchedulesAutomapperProfile : Profile
    {


        public LocationSchedulesAutomapperProfile()
        {
            this.AddScheduleToLocationScheduleModelMap();
            this.AddFoodTruckToLocationScheduleFoodTruckModel();
        }


        internal void AddFoodTruckToLocationScheduleFoodTruckModel()
        {
            this.CreateMap<FoodTruck, LocationScheduleModel.FoodTruckModel>()
                            .ForMember(
                                dest => dest.Tags,
                                opt => opt.MapFrom(src => src.Tags.Select(t => t.Tag.Text).ToList())
                            )
                            .ForMember(
                                dest => dest.Meta,
                                opt => opt.MapFrom(src => src)
                            );


            // For the links in the Meta object
            this.CreateMap<FoodTruck, LocationScheduleModel.FoodTruckLinks>()
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
                            RouteName = FoodTrucks.Schedules.FoodTruckSchedulesController.GET_FOOD_TRUCK_SCHEDULE,
                            RouteParams = new { foodTruckId = src.FoodTruckId }
                        }
                    )
                );

        }


        internal void AddScheduleToLocationScheduleModelMap()
        {
            this.CreateMap<Schedule, LocationScheduleModel>()
                .ForMember(
                    dest => dest.StartTime,
                    opt => opt.MapFrom(src => src.ScheduledStart)
                )
                .ForMember(
                    dest => dest.EndTime,
                    opt => opt.MapFrom(src => src.ScheduledEnd)
                )
                .ForMember(
                    dest => dest.FoodTruck,
                    opt => opt.MapFrom(src => src.FoodTruck)
                )
                .ForMember(
                    dest => dest.Meta,
                    opt => opt.MapFrom(src => src)
                );


            // For the links in the Meta object
            this.CreateMap<Schedule, LocationScheduleModel.LocationScheduleLinks>()
                .ForMember(
                    dest => dest.Self,
                    opt => opt.ResolveUsing<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = FoodTrucks.Schedules.FoodTruckSchedulesController.GET_SINGLE_FOOD_TRUCK_SCHEDULE,
                            RouteParams = new { foodTruckId = src.FoodTruckId, scheduleId = src.ScheduleId }
                        }
                    )
                )
                .ForMember(
                    dest => dest.Location,
                    opt => opt.ResolveUsing<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = LocationsController.GET_LOCATION_BY_ID,
                            RouteParams = new { id = src.LocationId }
                        }
                    )
                );
        }

    }
}
