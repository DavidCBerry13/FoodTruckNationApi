using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DavidBerry.Framework.Functional;
using DavidBerry.Framework.TimeAndDate;
using DavidBerry.Framework.Util;
using FoodTruckNation.Core.AppServices;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.DataInterfaces;
using FoodTruckNation.Core.Domain;
using FoodTruckNation.Core.Util;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace FoodTruckNation.Core.Test.AppServices;


public class ScheduleServiceTests
{
    // ----------------------------------------------------------------
    // Helpers
    // ----------------------------------------------------------------

    public Location GetLocation(int id, string name, string localityCode, string localityName)
    {
        return new Location()
        {
            LocationId = id,
            Name = name,
            Locality = new Locality() { LocalityCode = localityCode, Name = localityName },
            StreetAddress = "",
            City = "",
            State = "",
            ZipCode = "",
            Latitude = 0,
            Longitude = 0
        };
    }

    public FoodTruck GetFoodTruck(int id, string name, string localityCode, string localityName)
    {
        return new FoodTruck(id, name, "Description", "http://foodtruck.com/", new Locality() { LocalityCode = localityCode, Name = localityName });
    }



    [Fact]
    public async Task AddFoodTruckScehdule_ShouldAddScheduleSuccessfully_WhenFoodTruckHasNoSchedules()
    {
        // Data
        var foodTruck = GetFoodTruck(1, "Tasty Truck", "NYC", "New York City");
        var location = GetLocation(1, "Central Park", "NYC", "New York City");

        // Arrange
        var loggerFactoryMock = new Mock<ILoggerFactory>();

        var foodTruckRepositoryMock = new Mock<IFoodTruckRepository>();
        foodTruckRepositoryMock.Setup(x => x.GetFoodTruckAsync(foodTruck.FoodTruckId)).ReturnsAsync(foodTruck);

        var locationRepositoryMock = new Mock<ILocationRepository>();
        locationRepositoryMock.Setup(x => x.GetLocationAsync(location.LocationId)).ReturnsAsync(location);

        var scheduleRepositoryMock = new Mock<IScheduleRepository>();

        var databaseMock = new Mock<IFoodTruckDatabase>();
        databaseMock.Setup(x => x.FoodTruckRepository).Returns(foodTruckRepositoryMock.Object);
        databaseMock.Setup(x => x.LocationRepository).Returns(locationRepositoryMock.Object);
        databaseMock.Setup(x => x.ScheduleRepository).Returns(scheduleRepositoryMock.Object);

        var dateTimeProviderMock = new UnitTestDateTimeProvider(new DateTime(2025, 9, 15, 9, 0, 0));

        var scheduleService = new ScheduleService(loggerFactoryMock.Object, databaseMock.Object, dateTimeProviderMock);

        // Act
        var result = await scheduleService.AddFoodTruckScheduleAsync(new CreateFoodTruckScheduleCommand
        {
            FoodTruckId = 1,
            LocationId = 1,
            StartTime = new DateTime(2025, 9, 20, 11, 0, 0),
            EndTime = new DateTime(2025, 9, 20, 14, 0, 0)
        });

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();

        foodTruckRepositoryMock.Verify(x => x.SaveAsync(foodTruck), Times.Once);
    }




    [Fact]
    public async Task AddFoodTruckScehdule_ShouldAddScheduleSuccessfully_WhenFoodTruckScheduleButSlotOpen()
    {
        // Data
        var foodTruck = GetFoodTruck(1, "Tasty Truck", "NYC", "New York City");
        foodTruck.AddSchedule(new Schedule(foodTruck, GetLocation(2, "Times Square", "NYC", "New York City"), new DateTime(2025, 9, 20, 15, 0, 0), new DateTime(2025, 9, 20, 18, 0, 0)));
        var location = GetLocation(1, "Central Park", "NYC", "New York City");

        // Arrange
        var loggerFactoryMock = new Mock<ILoggerFactory>();

        var foodTruckRepositoryMock = new Mock<IFoodTruckRepository>();
        foodTruckRepositoryMock.Setup(x => x.GetFoodTruckAsync(foodTruck.FoodTruckId)).ReturnsAsync(foodTruck);

        var locationRepositoryMock = new Mock<ILocationRepository>();
        locationRepositoryMock.Setup(x => x.GetLocationAsync(location.LocationId)).ReturnsAsync(location);

        var databaseMock = new Mock<IFoodTruckDatabase>();
        databaseMock.Setup(x => x.FoodTruckRepository).Returns(foodTruckRepositoryMock.Object);
        databaseMock.Setup(x => x.LocationRepository).Returns(locationRepositoryMock.Object);

        var dateTimeProviderMock = new UnitTestDateTimeProvider(new DateTime(2025, 9, 15, 9, 0, 0));

        var scheduleService = new ScheduleService(loggerFactoryMock.Object, databaseMock.Object, dateTimeProviderMock);

        // Act
        var result = await scheduleService.AddFoodTruckScheduleAsync(new CreateFoodTruckScheduleCommand
        {
            FoodTruckId = 1,
            LocationId = 1,
            StartTime = new DateTime(2025, 9, 20, 11, 0, 0),
            EndTime = new DateTime(2025, 9, 20, 14, 0, 0)
        });


        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();

        foodTruck.Schedules.Count.ShouldBe(2);
        foodTruckRepositoryMock.Verify(x => x.SaveAsync(foodTruck), Times.Once);
    }



    [Fact]
    public async Task AddFoodTruckScehdule_ShouldFail_WhenFoodTruckScheduleOverlaps()
    {
        // Data
        var foodTruck = GetFoodTruck(1, "Tasty Truck", "NYC", "New York City");
        foodTruck.AddSchedule(new Schedule(foodTruck, GetLocation(2, "Times Square", "NYC", "New York City"), new DateTime(2025, 9, 20, 15, 0, 0), new DateTime(2025, 9, 20, 18, 0, 0)));
        var location = GetLocation(1, "Central Park", "NYC", "New York City");

        // Arrange
        var loggerFactoryMock = new Mock<ILoggerFactory>();

        var foodTruckRepositoryMock = new Mock<IFoodTruckRepository>();
        foodTruckRepositoryMock.Setup(x => x.GetFoodTruckAsync(foodTruck.FoodTruckId)).ReturnsAsync(foodTruck);

        var locationRepositoryMock = new Mock<ILocationRepository>();
        locationRepositoryMock.Setup(x => x.GetLocationAsync(location.LocationId)).ReturnsAsync(location);

        var databaseMock = new Mock<IFoodTruckDatabase>();
        databaseMock.Setup(x => x.FoodTruckRepository).Returns(foodTruckRepositoryMock.Object);
        databaseMock.Setup(x => x.LocationRepository).Returns(locationRepositoryMock.Object);

        var dateTimeProviderMock = new UnitTestDateTimeProvider(new DateTime(2025, 9, 15, 9, 0, 0));

        var scheduleService = new ScheduleService(loggerFactoryMock.Object, databaseMock.Object, dateTimeProviderMock);

        // Act
        var result = await scheduleService.AddFoodTruckScheduleAsync(new CreateFoodTruckScheduleCommand
        {
            FoodTruckId = 1,
            LocationId = 1,
            StartTime = new DateTime(2025, 9, 20, 14, 0, 0),
            EndTime = new DateTime(2025, 9, 20, 16, 0, 0)
        });


        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Value.ShouldBeNull();
        result.Error.ShouldBeOfType<SchedulingConflictError>();

        result.Error.Message.ShouldBe("The scheduled time (9/20/2025 2:00:00 PM-9/20/2025 4:00:00 PM) conflicts with an existing scheduled time for this food truck");
        result.Error.As<SchedulingConflictError>()?.ConflictingSchedules.Count.ShouldBe(1);
        result.Error.As<SchedulingConflictError>()?.ConflictingSchedules.Any(s => s.StartTime == new DateTime(2025, 9, 20, 15, 0, 0) && s.EndTime == new DateTime(2025, 9, 20, 18, 0, 0)).ShouldBeTrue();

        foodTruck.Schedules.Count.ShouldBe(1);
        foodTruckRepositoryMock.Verify(x => x.SaveAsync(foodTruck), Times.Never);
    }


    [Fact]
    public async Task AddFoodTruckScehdule_ShouldFailAndReturnAllOverlappingSchedules_WhenConflictsWithMultipleSchedules()
    {
        // Data
        var foodTruck = GetFoodTruck(1, "Tasty Truck", "NYC", "New York City");
        foodTruck.AddSchedule(new Schedule(foodTruck, GetLocation(3, "Brooklyn MetroTech", "NYC", "Brooklyn"), new DateTime(2025, 9, 20, 11, 0, 0), new DateTime(2025, 9, 20, 14, 00, 0)));
        foodTruck.AddSchedule(new Schedule(foodTruck, GetLocation(2, "Times Square", "NYC", "New York City"), new DateTime(2025, 9, 20, 15, 0, 0), new DateTime(2025, 9, 20, 18, 0, 0)));
        var location = GetLocation(1, "Central Park", "NYC", "New York City");

        // Arrange
        var loggerFactoryMock = new Mock<ILoggerFactory>();

        var foodTruckRepositoryMock = new Mock<IFoodTruckRepository>();
        foodTruckRepositoryMock.Setup(x => x.GetFoodTruckAsync(foodTruck.FoodTruckId)).ReturnsAsync(foodTruck);

        var locationRepositoryMock = new Mock<ILocationRepository>();
        locationRepositoryMock.Setup(x => x.GetLocationAsync(location.LocationId)).ReturnsAsync(location);

        var databaseMock = new Mock<IFoodTruckDatabase>();
        databaseMock.Setup(x => x.FoodTruckRepository).Returns(foodTruckRepositoryMock.Object);
        databaseMock.Setup(x => x.LocationRepository).Returns(locationRepositoryMock.Object);

        var dateTimeProviderMock = new UnitTestDateTimeProvider(new DateTime(2025, 9, 15, 9, 0, 0));

        var scheduleService = new ScheduleService(loggerFactoryMock.Object, databaseMock.Object, dateTimeProviderMock);

        // Act
        var result = await scheduleService.AddFoodTruckScheduleAsync(new CreateFoodTruckScheduleCommand
        {
            FoodTruckId = 1,
            LocationId = 1,
            StartTime = new DateTime(2025, 9, 20, 13, 0, 0),
            EndTime = new DateTime(2025, 9, 20, 17, 0, 0)
        });


        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Value.ShouldBeNull();
        result.Error.ShouldBeOfType<SchedulingConflictError>();

        result.Error.Message.ShouldBe("The scheduled time (9/20/2025 1:00:00 PM-9/20/2025 5:00:00 PM) conflicts with an existing scheduled time for this food truck");
        result.Error.As<SchedulingConflictError>()?.ConflictingSchedules.Count.ShouldBe(2);
        result.Error.As<SchedulingConflictError>()?.ConflictingSchedules.Any(s => s.StartTime == new DateTime(2025, 9, 20, 11, 0, 0) && s.EndTime == new DateTime(2025, 9, 20, 14, 0, 0)).ShouldBeTrue();
        result.Error.As<SchedulingConflictError>()?.ConflictingSchedules.Any(s => s.StartTime == new DateTime(2025, 9, 20, 15, 0, 0) && s.EndTime == new DateTime(2025, 9, 20, 18, 0, 0)).ShouldBeTrue();

        foodTruck.Schedules.Count.ShouldBe(2);
        foodTruckRepositoryMock.Verify(x => x.SaveAsync(foodTruck), Times.Never);
    }





    [Fact]
    public async Task UpdateFoodTruckScehdule_ShouldUpdateScheduleSuccessfully_WhenOnlyOneScheduleObject()
    {
        // Data
        var foodTruck = GetFoodTruck(1, "Tasty Truck", "NYC", "New York City");
        Schedule schedule = new Schedule(1, foodTruck, GetLocation(2, "Times Square", "NYC", "New York City"), new DateTime(2025, 9, 20, 15, 0, 0), new DateTime(2025, 9, 20, 18, 0, 0));
        foodTruck.AddSchedule(schedule);

        // Arrange
        var loggerFactoryMock = new Mock<ILoggerFactory>();

        var foodTruckRepositoryMock = new Mock<IFoodTruckRepository>();
        foodTruckRepositoryMock.Setup(x => x.GetFoodTruckAsync(foodTruck.FoodTruckId)).ReturnsAsync(foodTruck);

        var databaseMock = new Mock<IFoodTruckDatabase>();
        databaseMock.Setup(x => x.FoodTruckRepository).Returns(foodTruckRepositoryMock.Object);

        var dateTimeProviderMock = new UnitTestDateTimeProvider(new DateTime(2025, 9, 15, 9, 0, 0));

        var scheduleService = new ScheduleService(loggerFactoryMock.Object, databaseMock.Object, dateTimeProviderMock);

        // Act
        var result = await scheduleService.UpdateFoodTruckScheduleAsync(new UpdateFoodTruckScheduleCommand
        {
            FoodTruckId = 1,
            ScheduleId = 1,
            StartTime = new DateTime(2025, 9, 20, 11, 0, 0),
            EndTime = new DateTime(2025, 9, 20, 14, 0, 0)
        });


        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();

        foodTruck.Schedules.Count.ShouldBe(1);
        foodTruck.Schedules.Any(s => s.ScheduleId == 1 && s.StartTime == new DateTime(2025, 9, 20, 11, 0, 0) && s.EndTime == new DateTime(2025, 9, 20, 14, 0, 0)).ShouldBeTrue();
        foodTruckRepositoryMock.Verify(x => x.SaveAsync(foodTruck), Times.Once);
    }



    [Fact]
    public async Task UpdateFoodTruckScehdule_ShouldUpdateScheduleSuccessfully_WhenOnlyScheduleDoesNotOverlap()
    {
        // Data
        var foodTruck = GetFoodTruck(1, "Tasty Truck", "NYC", "New York City");
        foodTruck.AddSchedule(new Schedule(1, foodTruck, GetLocation(2, "Times Square", "NYC", "New York City"), new DateTime(2025, 9, 20, 15, 0, 0), new DateTime(2025, 9, 20, 18, 0, 0)));
        foodTruck.AddSchedule(new Schedule(2, foodTruck, GetLocation(1, "Central Park", "NYC", "New York City"), new DateTime(2025, 9, 20, 11, 0, 0), new DateTime(2025, 9, 20, 14, 0, 0)));

        // Arrange
        var loggerFactoryMock = new Mock<ILoggerFactory>();

        var foodTruckRepositoryMock = new Mock<IFoodTruckRepository>();
        foodTruckRepositoryMock.Setup(x => x.GetFoodTruckAsync(foodTruck.FoodTruckId)).ReturnsAsync(foodTruck);

        var databaseMock = new Mock<IFoodTruckDatabase>();
        databaseMock.Setup(x => x.FoodTruckRepository).Returns(foodTruckRepositoryMock.Object);

        var dateTimeProviderMock = new UnitTestDateTimeProvider(new DateTime(2025, 9, 15, 9, 0, 0));

        var scheduleService = new ScheduleService(loggerFactoryMock.Object, databaseMock.Object, dateTimeProviderMock);

        // Act
        var result = await scheduleService.UpdateFoodTruckScheduleAsync(new UpdateFoodTruckScheduleCommand
        {
            FoodTruckId = 1,
            ScheduleId = 1,
            StartTime = new DateTime(2025, 9, 20, 14, 0, 0),
            EndTime = new DateTime(2025, 9, 20, 19, 0, 0)
        });


        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();

        foodTruck.Schedules.Count.ShouldBe(2);
        foodTruck.Schedules.Any(s => s.ScheduleId == 1 && s.StartTime == new DateTime(2025, 9, 20, 14, 0, 0) && s.EndTime == new DateTime(2025, 9, 20, 19, 0, 0)).ShouldBeTrue();
        foodTruckRepositoryMock.Verify(x => x.SaveAsync(foodTruck), Times.Once);
    }


    [Fact]
    public async Task UpdateFoodTruckScehdule_ShouldFail_WhenScheduleOverlapsWithAnother()
    {
        // Data
        var foodTruck = GetFoodTruck(1, "Tasty Truck", "NYC", "New York City");
        foodTruck.AddSchedule(new Schedule(1, foodTruck, GetLocation(1, "Central Park", "NYC", "New York City"), new DateTime(2025, 9, 20, 11, 0, 0), new DateTime(2025, 9, 20, 14, 30, 0)));
        foodTruck.AddSchedule(new Schedule(2, foodTruck, GetLocation(2, "Times Square", "NYC", "New York City"), new DateTime(2025, 9, 20, 15, 0, 0), new DateTime(2025, 9, 20, 18, 0, 0)));

        // Arrange
        var loggerFactoryMock = new Mock<ILoggerFactory>();

        var foodTruckRepositoryMock = new Mock<IFoodTruckRepository>();
        foodTruckRepositoryMock.Setup(x => x.GetFoodTruckAsync(foodTruck.FoodTruckId)).ReturnsAsync(foodTruck);

        var databaseMock = new Mock<IFoodTruckDatabase>();
        databaseMock.Setup(x => x.FoodTruckRepository).Returns(foodTruckRepositoryMock.Object);

        var dateTimeProviderMock = new UnitTestDateTimeProvider(new DateTime(2025, 9, 15, 9, 0, 0));

        var scheduleService = new ScheduleService(loggerFactoryMock.Object, databaseMock.Object, dateTimeProviderMock);

        // Act
        var result = await scheduleService.UpdateFoodTruckScheduleAsync(new UpdateFoodTruckScheduleCommand
        {
            FoodTruckId = 1,
            ScheduleId = 2,
            StartTime = new DateTime(2025, 9, 20, 14, 0, 0),
            EndTime = new DateTime(2025, 9, 20, 19, 0, 0)
        });


        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Value.ShouldBeNull();

        foodTruck.Schedules.Count.ShouldBe(2);

        result.Error.As<SchedulingConflictError>()?.ConflictingSchedules.Count.ShouldBe(1);
        result.Error.As<SchedulingConflictError>()?.ConflictingSchedules.Any(s => s.ScheduleId == 1).ShouldBeTrue();  // Scheduel 2 now overlaps with Schedule 1

        // Schedule 2 should remain unchanged
        foodTruck.Schedules.Any(s => s.ScheduleId == 2 && s.StartTime == new DateTime(2025, 9, 20, 15, 0, 0) && s.EndTime == new DateTime(2025, 9, 20, 18, 0, 0)).ShouldBeTrue();
        foodTruckRepositoryMock.Verify(x => x.SaveAsync(foodTruck), Times.Never);
    }




    [Fact]
    public async Task UpdateFoodTruckScehdule_ShouldFail_WhenScheduleIdNotForFoodTruck()
    {
        // Data
        var foodTruck = GetFoodTruck(1, "Tasty Truck", "NYC", "New York City");
        foodTruck.AddSchedule(new Schedule(1, foodTruck, GetLocation(1, "Central Park", "NYC", "New York City"), new DateTime(2025, 9, 20, 11, 0, 0), new DateTime(2025, 9, 20, 14, 30, 0)));
        foodTruck.AddSchedule(new Schedule(2, foodTruck, GetLocation(2, "Times Square", "NYC", "New York City"), new DateTime(2025, 9, 20, 15, 0, 0), new DateTime(2025, 9, 20, 18, 0, 0)));

        // Arrange
        var loggerFactoryMock = new Mock<ILoggerFactory>();

        var foodTruckRepositoryMock = new Mock<IFoodTruckRepository>();
        foodTruckRepositoryMock.Setup(x => x.GetFoodTruckAsync(foodTruck.FoodTruckId)).ReturnsAsync(foodTruck);

        var databaseMock = new Mock<IFoodTruckDatabase>();
        databaseMock.Setup(x => x.FoodTruckRepository).Returns(foodTruckRepositoryMock.Object);

        var dateTimeProviderMock = new UnitTestDateTimeProvider(new DateTime(2025, 9, 15, 9, 0, 0));

        var scheduleService = new ScheduleService(loggerFactoryMock.Object, databaseMock.Object, dateTimeProviderMock);

        // Act
        var result = await scheduleService.UpdateFoodTruckScheduleAsync(new UpdateFoodTruckScheduleCommand
        {
            FoodTruckId = 1,
            ScheduleId = 3,
            StartTime = new DateTime(2025, 9, 20, 14, 0, 0),
            EndTime = new DateTime(2025, 9, 20, 19, 0, 0)
        });


        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Value.ShouldBeNull();

        foodTruck.Schedules.Count.ShouldBe(2);

        result.Error.ShouldBeOfType<ObjectNotFoundError>();

        // Schedule 2 should remain unchanged
        foodTruckRepositoryMock.Verify(x => x.SaveAsync(foodTruck), Times.Never);
    }




    [Fact]
    public async Task UpdateFoodTruckScehdule_ShouldFail_WhenFoodTruckNotFound()
    {
        // Data
        int foodTruckId = 1;

        // Arrange
        var loggerFactoryMock = new Mock<ILoggerFactory>();

        var foodTruckRepositoryMock = new Mock<IFoodTruckRepository>();
        foodTruckRepositoryMock.Setup(x => x.GetFoodTruckAsync(foodTruckId)).ReturnsAsync((FoodTruck)null);

        var databaseMock = new Mock<IFoodTruckDatabase>();
        databaseMock.Setup(x => x.FoodTruckRepository).Returns(foodTruckRepositoryMock.Object);

        var dateTimeProviderMock = new UnitTestDateTimeProvider(new DateTime(2025, 9, 15, 9, 0, 0));

        var scheduleService = new ScheduleService(loggerFactoryMock.Object, databaseMock.Object, dateTimeProviderMock);

        // Act
        var result = await scheduleService.UpdateFoodTruckScheduleAsync(new UpdateFoodTruckScheduleCommand
        {
            FoodTruckId = 1,
            ScheduleId = 3,
            StartTime = new DateTime(2025, 9, 20, 14, 0, 0),
            EndTime = new DateTime(2025, 9, 20, 19, 0, 0)
        });


        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Value.ShouldBeNull();

        result.Error.ShouldBeOfType<ObjectNotFoundError>();

        // Schedule 2 should remain unchanged
        foodTruckRepositoryMock.Verify(x => x.SaveAsync(It.IsAny<FoodTruck>()), Times.Never);
    }


}

