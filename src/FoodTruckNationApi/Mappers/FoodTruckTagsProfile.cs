using AutoMapper;
using FoodTruckNation.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Mappers
{
    public class FoodTruckTagsProfile : Profile
    {


        public FoodTruckTagsProfile()
        {
            this.AddFoodTruckToFoodTruckModelMap();

        }


        internal void AddFoodTruckToFoodTruckModelMap()
        {
            this.CreateMap<FoodTruckTag, String>()
                .ConvertUsing(x => x.Tag.Text);
        }

    }
}
