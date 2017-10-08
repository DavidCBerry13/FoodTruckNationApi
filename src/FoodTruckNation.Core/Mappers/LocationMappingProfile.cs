using AutoMapper;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNation.Core.Mappers
{

    /// <summary>
    /// Class to define AutoMapper profiles for location related objects
    /// </summary>
    public class LocationMappingProfile : Profile
    {

        public LocationMappingProfile()
        {
            this.AddUpdateLocationCommandToLocationMap();
        }




        internal void AddUpdateLocationCommandToLocationMap()
        {
            this.CreateMap<UpdateLocationCommand, Location>();
        }

    }
}
