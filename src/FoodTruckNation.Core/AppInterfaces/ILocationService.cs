using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;
using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Core.AppInterfaces
{
    public interface ILocationService
    {

        List<Location> GetLocations();


        Location GetLocation(int id);


        Location CreateLocation(CreateLocationCommand locationInfo);


        Location UpdateLocation(UpdateLocationCommand locationInfo);


        void DeleteLocation(int locationId);


    }
}
