using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DavidBerry.Framework.Data;
using DavidBerry.Framework.Functional;
using DavidBerry.Framework.TimeAndDate;
using FoodTruckNation.Core.AppServices;
using FoodTruckNation.Core.DataInterfaces;
using FoodTruckNation.Core.Domain;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace FoodTruckNation.Core.Test.AppServices
{
    public class FoodTrucksServiceTests
    {



        [Fact]
        public async Task GetFoodTrucksWithLocalityCode_ReturnsSuccessWithFoodTrucks_WhenValidLocalityCodeIsUsed()
        {
            // Data
            string localityCode = "CHI";
            Locality locality = new Locality() { LocalityCode = localityCode, Name = "Chicago" };
            IEnumerable<FoodTruck> foodTrucks = new List<FoodTruck>()
            {
                new FoodTruck(1, "Food Truck One", "", "", locality),
                new FoodTruck(2, "Food Truck Two", "", "", locality),
                new FoodTruck(3, "Food Truck Three", "", "", locality),
            };

            // Arrange
            var loggerFactoryMock = new Mock<ILoggerFactory>();

            var foodTruckRepositoryMock = new Mock<IFoodTruckRepository>();
            foodTruckRepositoryMock.Setup(x => x.GetFoodTrucksAsync(locality)).ReturnsAsync(foodTrucks);

            var localityRepositoryMock = new Mock<ILocalityRepository>();
            localityRepositoryMock.Setup(x => x.GetLocalityAsync(localityCode)).ReturnsAsync(locality);

            var databaseMock = new Mock<IFoodTruckDatabase>();
            databaseMock.Setup(x => x.FoodTruckRepository).Returns(foodTruckRepositoryMock.Object);
            databaseMock.Setup(x => x.LocalityRepository).Returns(localityRepositoryMock.Object);

            var foodTruckService = new FoodTruckService(loggerFactoryMock.Object, databaseMock.Object, new StandardDateTimeProvider());

            // Act
            var result = await foodTruckService.GetFoodTrucksAsync(localityCode, null);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.Count().ShouldBe(3);
            foodTruckRepositoryMock.Verify(x => x.GetFoodTrucksAsync(locality), Times.Once());
        }


        [Fact]
        public async Task GetFoodTrucksWithLocalityCode_ReturnsFailureWithInvalidDataError_WhenUnknownLocalityCodePassed()
        {
            // Data
            string localityCode = "CHI";

            // Arrange
            var loggerFactoryMock = new Mock<ILoggerFactory>();
            var foodTruckRepositoryMock = new Mock<IFoodTruckRepository>();
            var localityRepositoryMock = new Mock<ILocalityRepository>();
            localityRepositoryMock.Setup(x => x.GetLocalityAsync(localityCode)).ReturnsAsync(null as Locality);

            var databaseMock = new Mock<IFoodTruckDatabase>();
            databaseMock.Setup(x => x.FoodTruckRepository).Returns(foodTruckRepositoryMock.Object);
            databaseMock.Setup(x => x.LocalityRepository).Returns(localityRepositoryMock.Object);

            var foodTruckService = new FoodTruckService(loggerFactoryMock.Object, databaseMock.Object, new StandardDateTimeProvider());

            // Act
            var result = await foodTruckService.GetFoodTrucksAsync(localityCode, null);

            // Assert
            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldBeOfType<InvalidDataError>();
            foodTruckRepositoryMock.Verify(x => x.GetFoodTrucksAsync(It.IsAny<Locality>()), Times.Never());
        }


        [Fact]
        public async Task GetFoodTrucksWithLocalityCodeAndTag_ReturnsSuccessWithFoodTrucks_WhenValidLocalityCodeAndTagAreUsed()
        {
            // Data
            string tag = "Thai";
            string localityCode = "CHI";
            Locality locality = new Locality() { LocalityCode = localityCode, Name = "Chicago" };
            IEnumerable<FoodTruck> foodTrucks = new List<FoodTruck>()
            {
                new FoodTruck(1, "Thai Food Truck One", "", "", locality),
                new FoodTruck(2, "Thai Food Truck Two", "", "", locality),
                new FoodTruck(3, "Thai Food Truck Three", "", "", locality),
            };

            // Arrange
            var loggerFactoryMock = new Mock<ILoggerFactory>();

            var foodTruckRepositoryMock = new Mock<IFoodTruckRepository>();
            foodTruckRepositoryMock.Setup(x => x.GetFoodTrucksAsync(locality, tag)).ReturnsAsync(foodTrucks);

            var localityRepositoryMock = new Mock<ILocalityRepository>();
            localityRepositoryMock.Setup(x => x.GetLocalityAsync(localityCode)).ReturnsAsync(locality);

            var databaseMock = new Mock<IFoodTruckDatabase>();
            databaseMock.Setup(x => x.FoodTruckRepository).Returns(foodTruckRepositoryMock.Object);
            databaseMock.Setup(x => x.LocalityRepository).Returns(localityRepositoryMock.Object);

            var tagRepositoryMock = new Mock<ITagRepository>();
            var socialMediaPlatformRepositoryMock = new Mock<ISocialMediaPlatformRepository>();

            var foodTruckService = new FoodTruckService(loggerFactoryMock.Object, databaseMock.Object, new StandardDateTimeProvider());

            // Act
            var result = await foodTruckService.GetFoodTrucksAsync(localityCode, tag);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.Count().ShouldBe(3);
            foodTruckRepositoryMock.Verify(x => x.GetFoodTrucksAsync(locality, tag), Times.Once());
        }


    }
}
