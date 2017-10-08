using AutoMapper;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;
using Framework.ApiUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Api.FoodTrucks.Reviews
{
    public class ReviewMappingProfile : Profile
    {



        public ReviewMappingProfile()
        {
            this.AddFoodTruckToFoodTruckModelMap();
            this.AddCreateReviewModelToCommandMap();
        }

        internal void AddFoodTruckToFoodTruckModelMap()
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


        internal void AddCreateReviewModelToCommandMap()
        {
            this.CreateMap<CreateReviewModel, CreateReviewCommand>();
        }




    }
}
