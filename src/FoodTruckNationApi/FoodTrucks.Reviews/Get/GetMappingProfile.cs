using AutoMapper;
using FoodTruckNation.Core.Domain;
using FoodTruckNationApi.FoodTrucks.Base;
using Framework.ApiUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.FoodTrucks.Reviews.Get
{
    public class GetMappingProfile : Profile
    {

        public GetMappingProfile()
        {
            this.CreateMap<Review, ReviewModel>()
                .ForMember(
                    dest => dest.Comments,
                    opt => opt.MapFrom(src => src.Details)
                )
                .ForMember(
                    dest => dest.Meta,
                    opt => opt.MapFrom(src => src)
                );


            // For the links in the Meta object
            this.CreateMap<Review, ReviewLinks>()
                .ForMember(
                    dest => dest.Self,
                    opt => opt.ResolveUsing<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = FoodTruckReviewsController.GET_SINGLE_FOOD_TRUCK_REVIEW,
                            RouteParams = new { foodTruckId = src.FoodTruckId, reviewId = src.ReviewId }
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

    }
}
