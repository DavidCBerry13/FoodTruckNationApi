using System;
using System.Collections.Generic;
using System.Text;
using FoodTruckNation.Core.Domain;
using FoodTruckNation.Core.DataInterfaces;
using Microsoft.Extensions.Logging;
using FoodTruckNation.Core.Commands;
using DavidBerry.Framework.Data;
using DavidBerry.Framework.Exceptions;
using FoodTruckNation.Core.AppInterfaces;
using DavidBerry.Framework.Functional;
using System.Threading.Tasks;

namespace FoodTruckNation.Core.AppServices
{
    public class LocationService : BaseService, ILocationService
    {

        public LocationService(ILoggerFactory loggerFactory, IFoodTruckDatabase foodTruckDatabase)
            : base(loggerFactory, foodTruckDatabase)
        {
        }




        public async Task<Result<Location>> GetLocationAsync(int id)
        {
            var location = await FoodTruckDatabase.LocationRepository.GetLocationAsync(id);
            return (location != null)
                ? Result.Success<Location>(location)
                : Result.Failure<Location>(new ObjectNotFoundError($"No location found with the id of {id}"));
        }



        public async Task<Result<IEnumerable<Location>>> GetLocationsAsync()
        {
            var locations = await FoodTruckDatabase.LocationRepository.GetLocationsAsync();
            return Result.Success<IEnumerable<Location>>(locations);
        }


        public async Task<Result<IEnumerable<Location>>> GetLocationsAsync(string localityCode)
        {
            var locality = await FoodTruckDatabase.LocalityRepository.GetLocalityAsync(localityCode);
            if (locality == null)
                return Result.Failure<IEnumerable<Location>>(new InvalidDataError($"The locality code {localityCode} does not exist"));

            var locations = await FoodTruckDatabase.LocationRepository.GetLocationsAsync(locality);
            return Result.Success<IEnumerable<Location>>(locations);
        }



        public async Task<Result<Location>> CreateLocationAsync(CreateLocationCommand createLocationCommand)
        {
            // TODO: Standardize address and check to see if this location already exists

            var locality = await FoodTruckDatabase.LocalityRepository.GetLocalityAsync(createLocationCommand.LocalityCode);
            if (locality == null)
                return Result.Failure<Location>(new InvalidDataError($"No locality with the locality code {createLocationCommand.LocalityCode} exists"));


            Location location = new Location(createLocationCommand.Name, locality, createLocationCommand.StreetAddress, createLocationCommand.City,
                createLocationCommand.State, createLocationCommand.ZipCode);

            await FoodTruckDatabase.LocationRepository.SaveAsync(location);
            FoodTruckDatabase.CommitChanges();

            return Result.Success<Location>(location);
        }


        public async Task<Result<Location>> UpdateLocationAsync(UpdateLocationCommand updateLocationCommand)
        {
            Location location = await FoodTruckDatabase.LocationRepository.GetLocationAsync(updateLocationCommand.LocationId);
            if (location == null)
                return Result.Failure<Location>($"No location was found with the id {updateLocationCommand.LocationId}");

            // Update the properties
            location.Name = updateLocationCommand.Name;
            location.StreetAddress = updateLocationCommand.StreetAddress;
            location.City = updateLocationCommand.City;
            location.State = updateLocationCommand.State;
            location.ZipCode = updateLocationCommand.ZipCode;



            await FoodTruckDatabase.LocationRepository.SaveAsync(location);
            FoodTruckDatabase.CommitChanges();

            return Result.Success<Location>(location);
        }


        public async Task<Result> DeleteLocationAsync(int locationId)
        {
            Location location = await FoodTruckDatabase.LocationRepository.GetLocationAsync(locationId);

            if (location == null)
                return Result.Failure(new ObjectNotFoundError($"Location id {locationId} not found so it could not be deleted"));

            await FoodTruckDatabase.LocationRepository.DeleteAsync(location);
            FoodTruckDatabase.CommitChanges();

            return Result.Success();
        }


    }
}
