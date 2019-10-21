using FoodTruckNation.Core.Domain;
using FoodTruckNation.Core.DataInterfaces;
using Framework.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodTruckNation.Data.EF.Repositories
{
    public class LocationRepository : ILocationRepository
    {

        public LocationRepository(FoodTruckContext context)
        {
            _foodTruckContext = context;
        }


        private readonly FoodTruckContext _foodTruckContext;




        public Location GetLocation(int locationId)
        {
            var location = _foodTruckContext.Locations
                .Where(l => l.LocationId == locationId)
                .AsNoTracking()
                .SingleOrDefault();

            return location;
        }

        public List<Location> GetLocations()
        {
            var locations = _foodTruckContext.Locations
                .AsNoTracking()
                .ToList();

            return locations;
        }



        public void Save(Location location)
        {
            _foodTruckContext.ChangeTracker.TrackGraph(location, EfExtensions.ConvertStateOfNode);
        }


        public void Delete(Location location)
        {
            _foodTruckContext.Remove(location);
        }

    }
}
