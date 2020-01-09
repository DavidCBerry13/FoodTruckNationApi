using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;
using DavidBerry.Framework.Functional;
using System;
using System.Collections.Generic;

namespace FoodTruckNation.Core.AppInterfaces
{
    public interface ILocationService
    {

        Result<List<Location>> GetLocations();


        Result<Location> GetLocation(int id);


        Result<Location> CreateLocation(CreateLocationCommand locationInfo);


        Result<Location> UpdateLocation(UpdateLocationCommand locationInfo);


        Result DeleteLocation(int locationId);


    }
}
