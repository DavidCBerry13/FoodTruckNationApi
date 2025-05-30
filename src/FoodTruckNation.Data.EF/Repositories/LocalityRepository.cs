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


        public void Delete(Locality locality)
        {
            _foodTruckContext.Remove(locality);
        }

        public List<Locality> GetLocalities()
        {
            var localities = _foodTruckContext.Localities
                .AsNoTracking()
                .ToList();

            return localities;
        }

        public Locality GetLocality(string localityCode)
        {
            var locality = _foodTruckContext.Localities
                .Where(l => l.LocalityCode == localityCode)
                .AsNoTracking()
                .SingleOrDefault();

            return locality;
        }

        public void Save(Locality locality)
        {
            _foodTruckContext.ChangeTracker.TrackGraph(locality, EfExtensions.ConvertStateOfNode);
        }
    }
}
