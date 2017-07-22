using FoodTruckNation.AppServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FoodTruckNation.Domain;
using FoodTruckNation.Domain.Repositories;
using Microsoft.Extensions.Logging;
using FoodTruckNation.AppServices.Framework;
using FoodTruckNation.AppServices.Models;
using Framework;

namespace FoodTruckNation.AppServices
{
    public class LocationService : BaseService, ILocationService
    {

        public LocationService(ILoggerFactory loggerFactory, IUnitOfWork uow, ILocationRepository locationRepository)
            : base(loggerFactory, uow)
        {
            this.locationRepository = locationRepository;
        }


        #region Member Variables


        private ILocationRepository locationRepository;

        #endregion



        public EntityResult<Location> GetLocation(int id)
        {
            try
            {
                var locations = this.locationRepository.GetLocation(id);
                return EntityResult<Location>.Success(locations);
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling LocationService.GetLocation()");
                return EntityResult<Location>.Error("An error occured processing your request");
            }
        }



        public EntityResult<List<Location>> GetLocations()
        {
            try
            {
                var locations = this.locationRepository.GetLocations();
                return EntityResult<List<Location>>.Success(locations);
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling LocationService.GetLocation()");
                return EntityResult<List<Location>>.Error("An error occured processing your request");
            }
        }



        public EntityResult<Location> CreateLocation(CreateLocationInfo locationInfo)
        {
            try
            {
                Location location = new Location(locationInfo.Name, locationInfo.StreetAddress, locationInfo.City,
                    locationInfo.State, locationInfo.ZipCode);

                this.locationRepository.Save(location);
                this.UnitOfWork.SaveChanges();

                return EntityResult<Location>.Success(location);
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling LocationService.CreateLocation()");
                return EntityResult<Location>.Error("An error occured processing your request");
            }
        }


        public EntityResult<Location> UpdateLocation(UpdateLocationInfo locationInfo)
        {
            try
            {
                Location location = this.locationRepository.GetLocation(locationInfo.LocationId);

                if (location == null)
                    return EntityResult<Location>.Failure($"No location was found with the id {locationInfo.LocationId}");

                // Update the properties
                location.Name = locationInfo.Name;
                location.StreetAddress = locationInfo.StreetAddress;
                location.City = locationInfo.City;
                location.State = locationInfo.State;
                location.ZipCode = locationInfo.ZipCode;

                this.locationRepository.Save(location);
                this.UnitOfWork.SaveChanges();

                return EntityResult<Location>.Success(location);
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling LocationService.CreateLocation()");
                return EntityResult<Location>.Error("An error occured processing your request");
            }
        }


        public Result DeleteLocation(int locationId)
        {
            Location location = this.locationRepository.GetLocation(locationId);

            if (location == null)
                return Result.Failure($"Location id {locationId} not found so it could not be deleted");
            

            this.locationRepository.Delete(location);
            this.UnitOfWork.SaveChanges();

            return Result.Success();
        }


    }
}
