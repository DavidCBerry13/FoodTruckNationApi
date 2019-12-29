using AutoMapper;
using FoodTruckNation.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Api.FoodTrucks.Tags
{
    public class FoodTruckTagsAutomapperProfile : Profile
    {


        public FoodTruckTagsAutomapperProfile()
        {
            AddFoodTruckToFoodTruckModelMap();

        }


        internal void AddFoodTruckToFoodTruckModelMap()
        {
            CreateMap<FoodTruckTag, String>()
                .ConvertUsing(x => x.Tag.Text);
        }

    }
}
