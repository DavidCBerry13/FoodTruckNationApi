using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;
using DavidBerry.Framework.Functional;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodTruckNation.Core.AppInterfaces
{
    public interface ILocationService
    {

        public Task<Result<IEnumerable<Location>>> GetLocationsAsync();

        public Task<Result<IEnumerable<Location>>> GetLocationsAsync(string localityCode);

        public Task<Result<Location>> GetLocationAsync(int id);


        public Task<Result<Location>> CreateLocationAsync(CreateLocationCommand locationInfo);


        public Task<Result<Location>> UpdateLocationAsync(UpdateLocationCommand locationInfo);


        public Task<Result> DeleteLocationAsync(int locationId);


    }
}
