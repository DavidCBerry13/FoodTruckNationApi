#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using AutoMapper;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;
using Framework.ApiUtil;
using System;


namespace FoodTruckNationApi.FoodTrucks.Reviews
{
    public class FoodTruckReviewsAutomapperProfile : Profile
    {

        public FoodTruckReviewsAutomapperProfile()
        {
            AddReviewModelMaps();
            AddCreateReviewModelMaps();
        }


        internal void AddReviewModelMaps()
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


        internal void AddCreateReviewModelMaps()
        {
            CreateMap<CreateReviewModel, CreateReviewCommand>();
        }

    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member