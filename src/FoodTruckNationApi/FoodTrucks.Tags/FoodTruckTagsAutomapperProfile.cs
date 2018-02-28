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
            this.AddFoodTruckToFoodTruckModelMap();

        }


        internal void AddFoodTruckToFoodTruckModelMap()
        {
            this.CreateMap<FoodTruckTag, String>()
                .ConvertUsing(x => x.Tag.Text);
        }

    }
}
