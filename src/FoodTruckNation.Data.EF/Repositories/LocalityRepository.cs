using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using System.Text;
using System.Threading.Tasks;
using FoodTruckNation.Core.DataInterfaces;
using FoodTruckNation.Core.Domain;
using DavidBerry.Framework.Data;

namespace FoodTruckNation.Data.EF.Repositories
{
    public class LocalityRepository : ILocalityRepository
    {

        public LocalityRepository(FoodTruckContext context)
        {
            _foodTruckContext = context;
        }


        private readonly FoodTruckContext _foodTruckContext;


        public Task DeleteAsync(Locality locality)
        {
            _foodTruckContext.Remove(locality);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Locality>> GetLocalitiesAsync()
        {
            var localities = await _foodTruckContext.Localities
                .AsNoTracking()
                .ToListAsync();

            return localities;
        }

        public async Task<Locality> GetLocalityAsync(string localityCode)
        {
            var locality = await _foodTruckContext.Localities
                .Where(l => l.LocalityCode == localityCode)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return locality;
        }

        public Task SaveAsync(Locality locality)
        {
            _foodTruckContext.ChangeTracker.TrackGraph(locality, EfExtensions.ConvertStateOfNode);
            return Task.CompletedTask;
        }
    }
}
