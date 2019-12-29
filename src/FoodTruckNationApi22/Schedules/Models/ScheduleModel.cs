using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoodTruckNation.Core.Domain;
using FoodTruckNationApi.FoodTrucks.Schedules;
using Framework.ApiUtil;

namespace FoodTruckNationApi.Schedules
{

    /// <summary>
    /// Represents a Schedule of a food truck being at a certain location at a certain time
    /// </summary>
    public class ScheduleModel
    {

        /// <summary>
        /// Unique identifier of this schedule record
        /// </summary>
        public int ScheduleId { get; set; }

        /// <summary>
        /// Food truck associated with this schedule
        /// </summary>
        public FoodTruckModel FoodTruck { get; set; }

        /// <summary>
        /// Location of where this scheduled event is to take place
        /// </summary>
        public LocationModel Location { get; set; }

        /// <summary>
        /// The starting date/time this food truck will be at this location
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// The ending date/time the food truck is scheduled to be at this location
        /// </summary>
        public DateTime EndTime { get; set; }



        #region Nested Types - Location

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
        }


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


        #endregion

        #region Nested Types - Food Truck

        /// <summary>
        /// Represents a food truck for a schedule (appointment) item
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
            /// Meta data object containing associated links for the food truck
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
    }


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public class ScheduleModelAutomapperProfile : Profile
    {

        public ScheduleModelAutomapperProfile()
        {
            CreateMap<Schedule, ScheduleModel>()
                    .ForMember(
                        dest => dest.FoodTruck,
                        opt => opt.MapFrom(src => src.FoodTruck)
                    )
                    .ForMember(
                        dest => dest.Location,
                        opt => opt.MapFrom(src => src.Location)
                    );

            // FoodTruck Truck
            CreateMap<FoodTruck, ScheduleModel.FoodTruckModel>()
                .ForMember(
                    dest => dest.Tags,
                    opt => opt.MapFrom(src => src.Tags.Select(t => t.Tag.Text).ToList())
                )
                .ForMember(
                    dest => dest.Meta,
                    opt => opt.MapFrom(src => src)
                );


            // For the links in the Meta object
            CreateMap<FoodTruck, ScheduleModel.FoodTruckLinks>()
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
                            RouteName = FoodTruckSchedulesController.GET_FOOD_TRUCK_SCHEDULE,
                            RouteParams = new { foodTruckId = src.FoodTruckId }
                        }
                    )
                );

            CreateMap<Location, ScheduleModel.LocationModel>()
                .ForMember(
                    dest => dest.LocationName,
                    opt => opt.MapFrom(src => src.Name)
                )
                .ForMember(
                    dest => dest.Meta,
                    opt => opt.MapFrom(src => src)
                );


            // For the links in the Meta object
            CreateMap<Location, ScheduleModel.LocationLinks>()
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
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member



        }

    }


}
