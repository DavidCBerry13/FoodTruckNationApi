using FoodTruckNation.Core.Domain;
using FoodTruckNation.Core.DataInterfaces;
using Framework;
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
            this.foodTruckContext = context;
        }


        private FoodTruckContext foodTruckContext;




        public Location GetLocation(int locationId)
        {
            var location = this.foodTruckContext.Locations
                .Where(l => l.LocationId == locationId)
                .AsNoTracking()
                .SingleOrDefault();

            return location;
        }

        public List<Location> GetLocations()
        {
            var locations = this.foodTruckContext.Locations
                .AsNoTracking()
                .ToList();

            return locations;
        }



        public void Save(Location location)
        {
            this.foodTruckContext.ChangeTracker.TrackGraph(location, EfExtensions.ConvertStateOfNode);
        }


        public void Delete(Location location)
        {
            this.foodTruckContext.Remove(location);
        }

    }
}
