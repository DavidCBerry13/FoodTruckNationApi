using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DavidBerry.Framework.Functional;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;

namespace FoodTruckNation.Core.AppInterfaces
{
    public interface ILocalityService
    {

        public Task<Result<IEnumerable<Locality>>> GetLocalitiesAsync();


        public Task<Result<Locality>> GetLocalityAsync(string code);


        public Task<Result<Locality>> CreateLocalityAsync(CreateLocalityCommand localityInfo);


        public Task<Result<Locality>> UpdateLocalityAsync(UpdateLocalityCommand localityInfo);


        public Task<Result> DeleteLocalityAsync(string code);


    }
}
