using FoodTruckNation.AppServices.Framework;
using FoodTruckNation.AppServices.Models;
using FoodTruckNation.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.AppServices.Interfaces
{
    public interface ILocationService
    {

        EntityResult<List<Location>> GetLocations();


        EntityResult<Location> GetLocation(int id);


        EntityResult<Location> CreateLocation(CreateLocationInfo locationInfo);

        EntityResult<Location> UpdateLocation(UpdateLocationInfo locationInfo);

        Result DeleteLocation(int locationId);


    }
}
