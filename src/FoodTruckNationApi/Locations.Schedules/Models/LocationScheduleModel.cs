using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FoodTruckNation.Core.Domain;
using Framework.ApiUtil;

namespace FoodTruckNationApi.Locations.Schedules
{

    /// <summary>
    /// Model object to represent the schedule of a food truck being at a location
    /// </summary>
    public class LocationScheduleModel
    {
        /// <summary>
        /// id of this schedule
        /// </summary>
        public int ScheduleId { get; set; }

        /// <summary>
        /// Id number of this location
        /// </summary>
        public int LocationId { get; set; }

        /// <summary>
        /// The food truck for this scheduled event.  This is the Model of a food truck,
        /// not just a food truck id so clients don't have to make subsequent calls from
        /// the API to get the food truck information
        /// </summary>
        public FoodTruckModel FoodTruck { get; set; }

        /// <summary>
        /// The start date/time the food truck will be at this location
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// The end date/time the food truck will be at this location
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// URL links to related data about this location schedule event
        /// </summary>
        public LocationScheduleLinks Meta { get; set; }



        #region Nested Class - Food Truck

        /// <summary>
        /// Represents the information about a Food truck at a location at a certain time
        /// </summary>
        public class FoodTruckModel
        {

            /// <summary>
            /// The unique id number of this food truck.  This is the id used to get a single food truck on get by id API calls
            /// </summary>
            public int FoodTruckId { get; set; }

            /// <summary>
            /// The name of the food truck
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// A text description of the offerings of the food truck
            /// </summary>
            public string Description { get; set; }

            /// <summary>
            /// The website for the food truck
            /// </summary>
            public string Website { get; set; }

            /// <summary>
            /// A list of tags associated with the food truck.  This list is just a string list of the tag text
            /// </summary>
            public List<string> Tags { get; set; }

            /// <summary>
            /// Links for the Food truck object
            /// </summary>
            public FoodTruckLinks Meta { get; set; }

        }


        /// <summary>
        /// Class to encapsulate the links (urls) for a food truck
        /// </summary>
        public class FoodTruckLinks
        {
            /// <summary>
            /// Gets the URL that refers to this food truck
            /// </summary>
            public string Self { get; set; }

            /// <summary>
            /// Gets the url for the reviews of this food truck
            /// </summary>
            public string Reviews { get; set; }

            /// <summary>
            /// Gets the url for the schedules of this food truck
            /// </summary>
            public string Schedules { get; set; }
        }

        #endregion


        #region Nested Class - LocationScheduleLinks

        /// <summary>
        /// Model representing links to associated information for this location schedule
        /// </summary>
        public class LocationScheduleLinks
        {
            /// <summary>
            /// URL Link to the schedule event
            /// </summary>
            public string Self { get; set; }

            /// <summary>
            /// URL link to the location this schedule event is at
            /// </summary>
            public string Location { get; set; }
        }

        #endregion
    }


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member




    public class LocationScheduleModelAutomapperProfile : Profile
    {
        public LocationScheduleModelAutomapperProfile()
        {
            CreateMap<FoodTruck, LocationScheduleModel.FoodTruckModel>()
                .ForMember(
                    dest => dest.Tags,
                    opt => opt.MapFrom(src => src.Tags.Select(t => t.Tag.Text).ToList())
                )
                .ForMember(
                    dest => dest.Meta,
                    opt => opt.MapFrom(src => src)
                );


            // For the links in the Meta object
            CreateMap<FoodTruck, LocationScheduleModel.FoodTruckLinks>()
                .ForMember(
                    dest => dest.Self,
                    opt => opt.MapFrom<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = FoodTrucks.FoodTrucksController.GET_FOOD_TRUCK_BY_ID,
                            RouteParams = new { id = src.FoodTruckId }
                        }
                    )
                )
                .ForMember(
                    dest => dest.Reviews,
                    opt => opt.MapFrom<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = FoodTrucks.Reviews.FoodTruckReviewsController.GET_ALL_FOOD_TRUCK_REVIEWS,
                            RouteParams = new { foodTruckId = src.FoodTruckId }
                        }
                    )
                )
                .ForMember(
                    dest => dest.Schedules,
                    opt => opt.MapFrom<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = FoodTrucks.Schedules.FoodTruckSchedulesController.GET_FOOD_TRUCK_SCHEDULE,
                            RouteParams = new { foodTruckId = src.FoodTruckId }
                        }
                    )
                );

            CreateMap<Schedule, LocationScheduleModel>()
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
            CreateMap<Schedule, LocationScheduleModel.LocationScheduleLinks>()
                .ForMember(
                    dest => dest.Self,
                    opt => opt.MapFrom<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = FoodTrucks.Schedules.FoodTruckSchedulesController.GET_SINGLE_FOOD_TRUCK_SCHEDULE,
                            RouteParams = new { foodTruckId = src.FoodTruckId, scheduleId = src.ScheduleId }
                        }
                    )
                )
                .ForMember(
                    dest => dest.Location,
                    opt => opt.MapFrom<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = LocationsController.GET_LOCATION_BY_ID,
                            RouteParams = new { id = src.LocationId }
                        }
                    )
                );
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member


}
