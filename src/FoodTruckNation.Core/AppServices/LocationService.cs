using System;
using System.Collections.Generic;
using System.Text;
using FoodTruckNation.Core.Domain;
using FoodTruckNation.Core.Repositories;
using Microsoft.Extensions.Logging;
using FoodTruckNation.Core.Commands;
using Framework;
using FoodTruckNation.Core.AppInterfaces;

namespace FoodTruckNation.Core.AppServices
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



        public Location GetLocation(int id)
        {
            var locations = this.locationRepository.GetLocation(id);
            return locations;
        }



        public List<Location> GetLocations()
        {
            var locations = this.locationRepository.GetLocations();
            return locations;
        }



        public Location CreateLocation(CreateLocationCommand createLocationCommand)
        {
            // TODO: Standardize address and check to see if this location already exists

            Location location = new Location(createLocationCommand.Name, createLocationCommand.StreetAddress, createLocationCommand.City,
                createLocationCommand.State, createLocationCommand.ZipCode);

            this.locationRepository.Save(location);
            this.UnitOfWork.SaveChanges();

            return location;
        }


        public Location UpdateLocation(UpdateLocationCommand updateLocationCommand)
        {
            Location location = this.locationRepository.GetLocation(updateLocationCommand.LocationId);
            if (location == null)
                throw new ObjectNotFoundException($"No location was found with the id {updateLocationCommand.LocationId}");

            // Update the properties
            location.Name = updateLocationCommand.Name;
            location.StreetAddress = updateLocationCommand.StreetAddress;
            location.City = updateLocationCommand.City;
            location.State = updateLocationCommand.State;
            location.ZipCode = updateLocationCommand.ZipCode;

            

            this.locationRepository.Save(location);
            this.UnitOfWork.SaveChanges();

            return location;
        }


        public void DeleteLocation(int locationId)
        {
            Location location = this.locationRepository.GetLocation(locationId);

            if (location == null)
                throw new ObjectNotFoundException($"Location id {locationId} not found so it could not be deleted");
            
            this.locationRepository.Delete(location);
            this.UnitOfWork.SaveChanges();
        }


    }
}
