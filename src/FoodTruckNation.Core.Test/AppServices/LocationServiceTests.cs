using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DavidBerry.Framework.Data;
using DavidBerry.Framework.Functional;
using FoodTruckNation.Core.AppServices;
using FoodTruckNation.Core.DataInterfaces;
using FoodTruckNation.Core.Domain;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace FoodTruckNation.Core.Test.AppServices;


public class LocationServiceTests
{
    [Fact]
    public async Task GetLocationsWithLocalityCode_ReturnsSuccessWithLocations_WhenValidLocalityCodeIsUsed()
    {
        // Data
        string localityCode = "CHI";
        Locality locality = new Locality() {  LocalityCode = localityCode, Name = "Chicago"};
        IEnumerable<Location> locations = new List<Location>()
        {
            new Location() { LocationId = 1, Name = "", Locality = locality, StreetAddress = "", City = "", State = "", ZipCode = "", Latitude = 0, Longitude = 0 },
            new Location() { LocationId = 2, Name = "", Locality = locality, StreetAddress = "", City = "", State = "", ZipCode = "", Latitude = 0, Longitude = 0 },
            new Location() { LocationId = 3, Name = "", Locality = locality, StreetAddress = "", City = "", State = "", ZipCode = "", Latitude = 0, Longitude = 0 },
        };

        // Arrange
        var loggerFactoryMock = new Mock<ILoggerFactory>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var locationRepositoryMock = new Mock<ILocationRepository>();
        locationRepositoryMock.Setup(x => x.GetLocationsAsync(locality)).ReturnsAsync(locations);
        var localityRepositoryMock = new Mock<ILocalityRepository>();
        localityRepositoryMock.Setup(x => x.GetLocalityAsync(localityCode)).ReturnsAsync(locality);

        var locationService = new LocationService(loggerFactoryMock.Object, unitOfWorkMock.Object, locationRepositoryMock.Object, localityRepositoryMock.Object);

        // Act
        var result = await locationService.GetLocationsAsync(localityCode);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Count().ShouldBe(3);
    }


    [Fact]
    public async Task GetLocationsWithLocalityCode_ReturnsFailureWithInvalidDataError_WhenUnknownLocalityCodePassed()
    {
        // Data
        string localityCode = "CHI";

        // Arrange
        var loggerFactoryMock = new Mock<ILoggerFactory>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var locationRepositoryMock = new Mock<ILocationRepository>();
        var localityRepositoryMock = new Mock<ILocalityRepository>();
        localityRepositoryMock.Setup(x => x.GetLocalityAsync(localityCode)).Returns(Task.FromResult((Locality)null));

        var locationService = new LocationService(loggerFactoryMock.Object, unitOfWorkMock.Object, locationRepositoryMock.Object, localityRepositoryMock.Object);

        // Act
        var result = await locationService.GetLocationsAsync(localityCode);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBeOfType<InvalidDataError>();
        locationRepositoryMock.Verify(x => x.GetLocationsAsync(It.IsAny<Locality>()), Times.Never());
    }

}

