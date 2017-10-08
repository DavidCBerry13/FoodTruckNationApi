using AutoMapper;
using FoodTruckNation.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.FoodTrucks.Reviews.Create
{
    public class CreateMappingProfile : Profile
    {


        public CreateMappingProfile()
        {
            this.CreateMap<CreateReviewModel, CreateReviewCommand>();
        }



    }
}
