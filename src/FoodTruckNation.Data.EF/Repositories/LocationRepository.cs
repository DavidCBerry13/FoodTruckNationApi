using FoodTruckNation.Core.Domain;
using FoodTruckNation.Core.DataInterfaces;
using DavidBerry.Framework.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruckNation.Data.EF.Repositories
{
    public class LocationRepository : ILocationRepository
    {

        public LocationRepository(FoodTruckContext context)
        {
            _foodTruckContext = context;
        }


        private readonly FoodTruckContext _foodTruckContext;




        public async Task<Location> GetLocationAsync(int locationId)
        {
            var location = await _foodTruckContext.Locations
                .Where(l => l.LocationId == locationId)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return location;
        }

        public async Task<IEnumerable<Location>> GetLocationsAsync()
        {
            var locations = await _foodTruckContext.Locations
                .AsNoTracking()
                .ToListAsync();

            return locations;
        }



        public Task SaveAsync(Location location)
        {
            _foodTruckContext.ChangeTracker.TrackGraph(location, EfExtensions.ConvertStateOfNode);
            return Task.CompletedTask;
        }


        public Task DeleteAsync(Location location)
        {
            _foodTruckContext.Remove(location);
            return Task.CompletedTask;
        }


    }
}
