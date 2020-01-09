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



        public Result<Location> GetLocation(int id)
        {
            var location = _locationRepository.GetLocation(id);
            return (location != null)
                ? Result.Success<Location>(location)
                : Result.Failure<Location>($"No location found with the id of {id}");
        }



        public Result<List<Location>> GetLocations()
        {
            var locations = _locationRepository.GetLocations();
            return Result.Success<List<Location>>(locations);
        }



        public Result<Location> CreateLocation(CreateLocationCommand createLocationCommand)
        {
            // TODO: Standardize address and check to see if this location already exists

            Location location = new Location(createLocationCommand.Name, createLocationCommand.StreetAddress, createLocationCommand.City,
                createLocationCommand.State, createLocationCommand.ZipCode);

            _locationRepository.Save(location);
            UnitOfWork.SaveChanges();

            return Result.Success<Location>(location);
        }


        public Result<Location> UpdateLocation(UpdateLocationCommand updateLocationCommand)
        {
            Location location = _locationRepository.GetLocation(updateLocationCommand.LocationId);
            if (location == null)
                return Result.Failure<Location>($"No location was found with the id {updateLocationCommand.LocationId}");

            // Update the properties
            location.Name = updateLocationCommand.Name;
            location.StreetAddress = updateLocationCommand.StreetAddress;
            location.City = updateLocationCommand.City;
            location.State = updateLocationCommand.State;
            location.ZipCode = updateLocationCommand.ZipCode;



            _locationRepository.Save(location);
            UnitOfWork.SaveChanges();

            return Result.Success<Location>(location);
        }


        public Result DeleteLocation(int locationId)
        {
            Location location = _locationRepository.GetLocation(locationId);

            if (location == null)
                return Result.Failure(new ObjectNotFoundError($"Location id {locationId} not found so it could not be deleted"));

            _locationRepository.Delete(location);
            UnitOfWork.SaveChanges();

            return Result.Success();
        }


    }
}
