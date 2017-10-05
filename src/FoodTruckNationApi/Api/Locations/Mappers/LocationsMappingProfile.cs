using AutoMapper;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;
using FoodTruckNationApi.Api.FoodTrucks;
using FoodTruckNationApi.Api.Locations.Models;
using FoodTruckNationApi.Controllers;
using Framework.ApiUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Api.Locations
{

    /// <summary>
    /// Mapping profile for dealing with Location related objects in the Locations feature
    /// </summary>
    public class LocationsMappingProfile : Profile
    {

        public LocationsMappingProfile()
        {
            this.AddLocationToLocationModelMap();
            this.AddCreateLocationModelToCreateLocationCommandMap();
            this.AddUpdateLocationModelToUpdateLocationCommand();
            this.AddScheduleToLocationScheduleModelMap();
        }


        internal void AddLocationToLocationModelMap()
        {
            this.CreateMap<Location, LocationModel>()
                .ForMember(
                    dest => dest.LocationName,
                    opt => opt.MapFrom(src => src.Name)
                )
                .ForMember(
                    dest => dest.Meta,
                    opt => opt.MapFrom(src => src)
                );


            // For the links in the Meta object
            this.CreateMap<Location, LocationLinks>()
                .ForMember(
                    dest => dest.Self,
                    opt => opt.ResolveUsing<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = LocationsController.GET_LOCATION_BY_ID,
                            RouteParams = new { id = src.LocationId }
                        }
                    )
                )
                .ForMember(
                    dest => dest.Schedules,
                    opt => opt.ResolveUsing<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = LocationSchedulesController.GET_ALL_SCHEDULES_FOR_LOCATION,
                            RouteParams = new { locationId = src.LocationId }
                        }
                    )
                );

        }


        internal void AddCreateLocationModelToCreateLocationCommandMap()
        {
            this.CreateMap<CreateLocationModel, CreateLocationCommand>();

        }


        internal void AddUpdateLocationModelToUpdateLocationCommand()
        {
            this.CreateMap<UpdateLocationModel, UpdateLocationCommand>();
        }


        internal void AddFoodTruckToLocationScheduleFoodTruckModel()
        {
            this.CreateMap<FoodTruck, LocationScheduleFoodTruckModel>()
                            .ForMember(
                                dest => dest.Tags,
                                opt => opt.MapFrom(src => src.Tags.Select(t => t.Tag.Text).ToList())
                            )
                            .ForMember(
                                dest => dest.Meta,
                                opt => opt.MapFrom(src => src)
                            );


            // For the links in the Meta object
            this.CreateMap<FoodTruck, LocationScheduleFoodTruckLinks>()
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
            this.CreateMap<Schedule, LocationScheduleLinks>()
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
