using AutoMapper;
using FoodTruckNation.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.FoodTrucks.Base.Create
{
    public class CreateMappingProfile : Profile
    {

        public CreateMappingProfile()
        {
            this.CreateMap<CreateFoodTruckModel, CreateFoodTruckCommand>();
        }
    }
}
