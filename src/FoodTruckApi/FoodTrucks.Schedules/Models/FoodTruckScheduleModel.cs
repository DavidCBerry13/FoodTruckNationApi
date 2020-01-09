using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoodTruckNation.Core.Domain;
using DavidBerry.Framework.ApiUtil;

namespace FoodTruckNationApi.FoodTrucks.Schedules
{

    /// <summary>
    /// Defines the data received when querying the schedule for a particular food truck
    /// </summary>
    public class FoodTruckScheduleModel
    {
        /// <summary>
        /// Unique identifier of this schedule record
        /// </summary>
        public int ScheduleId { get; set; }

        /// <summary>
        /// Id number of the food truck this schedule is for
        /// </summary>
        public int FoodTruckId { get; set; }

        /// <summary>
        /// Location of where this scheduled event is to take place
        /// </summary>
        /// <remarks>
        /// This model object contains the full LocationModel object rather than
        /// just a location id so clients do not have to make subsequent calls to
        /// the API to get the location information for each schedule
        /// </remarks>
        public LocationModel Location { get; set; }

        /// <summary>
        /// The starting date/time this food truck will be at this location
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// The ending date/time the food truck is scheduled to be at this location
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// URL Links to related objects
        /// </summary>
        public FoodTruckScheduleLinks Meta { get; set; }


        #region Nested Types

        public class LocationModel
        {
            /// <summary>
            /// Gets the unique id of this location
            /// </summary>
            public int LocationId { get; set; }

            /// <summary>
            /// The name of this location, usually the name of a park or business that is associated with the location
            /// </summary>
            public string LocationName { get; set; }

            /// <summary>
            /// The street address of this location
            /// </summary>
            public string StreetAddress { get; set; }

            /// <summary>
            /// The city this location is in
            /// </summary>
            public string City { get; set; }

            /// <summary>
            /// The state of this location
            /// </summary>
            public string State { get; set; }

            /// <summary>
            /// The zip code of this location
            /// </summary>
            public string ZipCode { get; set; }


            /// <summary>
            /// Meta data object containing associated links for the location
            /// </summary>
            public LocationLinks Meta { get; set; }


            /// <summary>
            /// Class to encapsulate the links (urls) for a location
            /// </summary>
            public class LocationLinks
            {
                /// <summary>
                /// Gets the URL that refers to this location
                /// </summary>
                public string Self { get; set; }

                /// <summary>
                /// URL link containing the upcoming schedules of food trucks at this location
                /// </summary>
                public string Schedules { get; set; }
            }
        }


        /// <summary>
        /// Model class to encapsulate the links for a FoodTruckSchedule object
        /// </summary>
        public class FoodTruckScheduleLinks
        {
            /// <summary>
            /// URL Link to this food truck schedule object
            /// </summary>
            public string Self { get; set; }

            /// <summary>
            /// URL Link to the Food Truck this schedule belongs to
            /// </summary>
            public string FoodTruck { get; set; }

        }

        #endregion

    }


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class FoodTruckScheduleModelAutomapperProfile : Profile
    {
        public FoodTruckScheduleModelAutomapperProfile()
        {
            CreateMap<Schedule, FoodTruckScheduleModel>()
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
            CreateMap<Schedule, FoodTruckScheduleModel.FoodTruckScheduleLinks>()
                .ForMember(
                    dest => dest.Self,
                    opt => opt.MapFrom<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = FoodTruckSchedulesController.GET_SINGLE_FOOD_TRUCK_SCHEDULE,
                            RouteParams = new { foodTruckId = src.FoodTruckId, scheduleId = src.ScheduleId }
                        }
                    )
                )
                .ForMember(
                    dest => dest.FoodTruck,
                    opt => opt.MapFrom<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = FoodTrucksController.GET_FOOD_TRUCK_BY_ID,
                            RouteParams = new { id = src.FoodTruckId }
                        }
                    )
                );

            CreateMap<Location, FoodTruckScheduleModel.LocationModel>()
                .ForMember(
                    dest => dest.LocationName,
                    opt => opt.MapFrom(src => src.Name)
                )
                .ForMember(
                    dest => dest.Meta,
                    opt => opt.MapFrom(src => src)
                );


            // For the links in the Meta object
            CreateMap<Location, FoodTruckScheduleModel.LocationModel.LocationLinks>()
                .ForMember(
                    dest => dest.Self,
                    opt => opt.MapFrom<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = Locations.LocationsController.GET_LOCATION_BY_ID,
                            RouteParams = new { id = src.LocationId }
                        }
                    )
                )
                .ForMember(
                    dest => dest.Schedules,
                    opt => opt.MapFrom<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = Locations.Schedules.LocationSchedulesController.GET_ALL_SCHEDULES_FOR_LOCATION,
                            RouteParams = new { locationId = src.LocationId }
                        }
                    )
                );
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member


}
