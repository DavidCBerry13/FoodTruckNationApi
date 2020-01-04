using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoodTruckNation.Core.Domain;
using DavidBerry.Framework.ApiUtil;
using static FoodTruckNationApi.FoodTrucks.Reviews.ReviewModel;

namespace FoodTruckNationApi.FoodTrucks.Reviews
{

    /// <summary>
    /// Represents the review of a food truck as returned by the API
    /// </summary>
    public class ReviewModel
    {

        /// <summary>
        /// The unique id number of this review
        /// </summary>
        public int ReviewId { get; set; }

        /// <summary>
        /// The id number of the food truck this review is for
        /// </summary>
        public int FoodTruckId { get; set; }

        /// <summary>
        /// The date this review was submitted
        /// </summary>
        public DateTime ReviewDate { get; set; }

        /// <summary>
        /// The rating (1-5) of this review
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Additional comments left on the review
        /// </summary>
        public String Comments { get; set; }

        /// <summary>
        /// Metadata object containing the URL links to related information for this review
        /// </summary>
        public ReviewLinks Meta { get; set; }

        /// <summary>
        /// Represents URL links to related data in the API for a review
        /// </summary>
        public class ReviewLinks
        {
            /// <summary>
            /// URL link the this individual review
            /// </summary>
            public string Self { get; set; }

            /// <summary>
            /// URL link to the Food Truck the review is for
            /// </summary>
            public string FoodTruck { get; set; }
        }

    }


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ReviewModelAutomapperProfile : Profile
    {

        public ReviewModelAutomapperProfile()
        {
            CreateMap<Review, ReviewModel>()
                .ForMember(
                    dest => dest.Comments,
                    opt => opt.MapFrom(src => src.Details)
                )
                .ForMember(
                    dest => dest.Meta,
                    opt => opt.MapFrom(src => src)
                );


            // For the links in the Meta object
            CreateMap<Review, ReviewLinks>()
                .ForMember(
                    dest => dest.Self,
                    opt => opt.MapFrom<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = FoodTruckReviewsController.GET_SINGLE_FOOD_TRUCK_REVIEW,
                            RouteParams = new { foodTruckId = src.FoodTruckId, reviewId = src.ReviewId }
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
        }

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member





}
