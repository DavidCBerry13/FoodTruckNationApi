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
            _locations = _foodTruckContext.Locations
                .AsNoTracking()
                .Include(x => x.Locality);
        }


        private readonly FoodTruckContext _foodTruckContext;
        private readonly IQueryable<Location> _locations;



        public async Task<Location> GetLocationAsync(int locationId)
        {
            var location = await _locations
                .Where(l => l.LocationId == locationId)
                .SingleOrDefaultAsync();

            return location;
        }

        public async Task<IEnumerable<Location>> GetLocationsAsync()
        {
            var locations = await _locations
                .ToListAsync();

            return locations;
        }


        public async Task<IEnumerable<Location>> GetLocationsAsync(Locality locality)
        {
            var locations = await _locations
                 .Where(l => l.LocalityCode == locality.LocalityCode)
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
