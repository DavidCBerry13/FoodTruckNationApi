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

        public LocationService(ILoggerFactory loggerFactory, IUnitOfWork uow, ILocationRepository locationRepository)
            : base(loggerFactory, uow)
        {
            _locationRepository = locationRepository;
        }


        #region Member Variables


        private readonly ILocationRepository _locationRepository;

        #endregion



        public async Task<Result<Location>> GetLocationAsync(int id)
        {
            var location = await _locationRepository.GetLocationAsync(id);
            return (location != null)
                ? Result.Success<Location>(location)
                : Result.Failure<Location>($"No location found with the id of {id}");
        }



        public async Task<Result<IEnumerable<Location>>> GetLocationsAsync()
        {
            var locations = await _locationRepository.GetLocationsAsync();
            return Result.Success<IEnumerable<Location>>(locations);
        }



        public async Task<Result<Location>> CreateLocationAsync(CreateLocationCommand createLocationCommand)
        {
            // TODO: Standardize address and check to see if this location already exists

            Location location = new Location(createLocationCommand.Name, createLocationCommand.StreetAddress, createLocationCommand.City,
                createLocationCommand.State, createLocationCommand.ZipCode);

            await _locationRepository.SaveAsync(location);
            await UnitOfWork.SaveChangesAsync();

            return Result.Success<Location>(location);
        }


        public async Task<Result<Location>> UpdateLocationAsync(UpdateLocationCommand updateLocationCommand)
        {
            Location location = await _locationRepository.GetLocationAsync(updateLocationCommand.LocationId);
            if (location == null)
                return Result.Failure<Location>($"No location was found with the id {updateLocationCommand.LocationId}");

            // Update the properties
            location.Name = updateLocationCommand.Name;
            location.StreetAddress = updateLocationCommand.StreetAddress;
            location.City = updateLocationCommand.City;
            location.State = updateLocationCommand.State;
            location.ZipCode = updateLocationCommand.ZipCode;



            await _locationRepository.SaveAsync(location);
            await UnitOfWork.SaveChangesAsync();

            return Result.Success<Location>(location);
        }


        public async Task<Result> DeleteLocationAsync(int locationId)
        {
            Location location = await _locationRepository.GetLocationAsync(locationId);

            if (location == null)
                return Result.Failure(new ObjectNotFoundError($"Location id {locationId} not found so it could not be deleted"));

            await _locationRepository.DeleteAsync(location);
            await UnitOfWork.SaveChangesAsync();

            return Result.Success();
        }


    }
}
