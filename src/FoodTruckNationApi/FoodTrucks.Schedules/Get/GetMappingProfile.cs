using AutoMapper;
using FoodTruckNation.Core.Domain;
using Framework.ApiUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.FoodTrucks.Schedules.Get
{
    public class GetMappingProfile : Profile
    {

        public GetMappingProfile()
        {
            this.ScheduleToFoodTruckScheduleModelMap();
            this.LocationToScheduleLocationModelMap();
        }


        internal void ScheduleToFoodTruckScheduleModelMap()
        {
            this.CreateMap<Schedule, FoodTruckScheduleModel>()
                .ForMember(
                    dest => dest.StartTime,
                    opt => opt.MapFrom(src => src.ScheduledStart)
                )
                .ForMember(
                    dest => dest.EndTime,
                    opt => opt.MapFrom(src => src.ScheduledEnd)
                )
                .ForMember(
                    dest => dest.Location,
                    opt => opt.MapFrom(src => src.Location)
                )
                .ForMember(
                    dest => dest.Meta,
                    opt => opt.MapFrom(src => src)
                );


            // For the links in the Meta object
            this.CreateMap<Schedule, FoodTruckScheduleLinks>()
                .ForMember(
                    dest => dest.Self,
                    opt => opt.ResolveUsing<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = FoodTruckSchedulesController.GET_SINGLE_FOOD_TRUCK_SCHEDULE,
                            RouteParams = new { foodTruckId = src.FoodTruckId, scheduleId = src.ScheduleId }
                        }
                    )
                )
                .ForMember(
                    dest => dest.FoodTruck,
                    opt => opt.ResolveUsing<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = FoodTrucksController.GET_FOOD_TRUCK_BY_ID,
                            RouteParams = new { id = src.FoodTruckId }
                        }
                    )
                );
        }





        internal void LocationToScheduleLocationModelMap()
        {
            this.CreateMap<Location, FoodTruckScheduleLocationModel>()
                .ForMember(
                    dest => dest.LocationName,
                    opt => opt.MapFrom(src => src.Name)
                )
                .ForMember(
                    dest => dest.Meta,
                    opt => opt.MapFrom(src => src)
                );


            // For the links in the Meta object
            this.CreateMap<Location, FoodTruckScheduleLocationLinks>()
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
