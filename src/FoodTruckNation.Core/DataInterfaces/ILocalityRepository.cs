using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodTruckNation.Core.Domain;

namespace FoodTruckNation.Core.DataInterfaces
{
    public interface ILocalityRepository
    {

        public Task<Locality> GetLocalityAsync(string localityCode);


        public Task<IEnumerable<Locality>> GetLocalitiesAsync();


        public Task SaveAsync(Locality locality);


        public Task DeleteAsync(Locality locality);

    }
}
