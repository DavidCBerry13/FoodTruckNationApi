using AutoMapper;
using FoodTruckNation.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Locations.Base.Update
{
    public class UpdateMappingProfile : Profile
    {

        public UpdateMappingProfile()
        {
            this.CreateMap<UpdateLocationModel, UpdateLocationCommand>();

        }

    }
}
