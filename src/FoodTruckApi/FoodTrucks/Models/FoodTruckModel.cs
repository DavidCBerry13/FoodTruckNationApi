using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FoodTruckNation.Core.Domain;
using DavidBerry.Framework.ApiUtil;

namespace FoodTruckNationApi.FoodTrucks
{

    /// <summary>
    /// Model class to represent the food truck data sent back to the client
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
        /// The locality (region) code this location is in
        /// </summary>
        public string LocalityCode { get; set; }

        /// <summary>
        /// The locality (region) name this location is in
        /// </summary>
        public string LocalityName { get; set; }

        /// <summary>
        /// Gets the number of reviews present for this food truck
        /// </summary>
        public int ReviewCount { get; set; }

        /// <summary>
        /// Gets the average review for the food truck
        /// </summary>
        public double ReviewAverage { get; set; }

        /// <summary>
        /// The last time this food truck object was modified.  This is needed when updating the object
        /// </summary>
        public DateTime LastModifiedDate { get; set; }

        /// <summary>
        /// Meta data object containing associated links for the food truck
        /// </summary>
        public FoodTruckLinks Meta { get; set; }

        #region Nested Types

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
    public class FoodTruckModelAutomapperProfile : Profile
    {
        public FoodTruckModelAutomapperProfile()
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
                )
                .ForMember(
                    dest => dest.LocalityCode,
                    opt => opt.MapFrom(src => src.Locality.LocalityCode))
                .ForMember(
                    dest => dest.LocalityName,
                    opt => opt.MapFrom(src => src.Locality.Name));

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

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

}
