using AutoMapper;
using FoodTruckNation.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.FoodTrucks.Update
{
    public class UpdateMappingProfile : Profile
    {

        public UpdateMappingProfile()
        {
            this.CreateMap<UpdateFoodTruckModel, UpdateFoodTruckCommand>();
        }

    }
}
