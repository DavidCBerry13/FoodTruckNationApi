using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Domain.Repositories
{
    public interface ILocationRepository
    {


        Location GetLocation(int locationId);


        List<Location> GetLocations();


        void Save(Location location);


        void Delete(Location location);


    }
}
