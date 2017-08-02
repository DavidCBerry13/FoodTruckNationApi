using FoodTruckNation.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Core.DataInterfaces
{
    public interface ILocationRepository
    {


        Location GetLocation(int locationId);


        List<Location> GetLocations();


        void Save(Location location);


        void Delete(Location location);


    }
}
