using FluentAssertions;
using FoodTruckNationApi.FoodTrucks.Schedules.Create;
using Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FoodTruckNationApi.Test.FoodTrucks.Schedules.Create
{
    public class CreateFoodTruckScheduleModelValidatorTest
    {



        [Fact]
        public void ValidateGoodSchedulesShouldPass()
        {
            // Arrange
            DateTime testDateTime = new DateTime(2017, 10, 9, 8, 0, 0);  // 8:00 AM
            IDateTimeProvider dateTimeProvider = new UnitTestDateTimeProvider(testDateTime);

            CreateFoodTruckScheduleModel model = new CreateFoodTruckScheduleModel()
            {
                LocationId = 1,
                StartTime = new DateTime(2017, 10, 9, 10, 0, 0),
                EndTime = new DateTime(2017, 10, 9, 14, 0, 0)
            };

            // Act
            var validator = new CreateFoodTruckScheduleModelValidator(dateTimeProvider);
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeTrue();
        }


        [Fact]
        public void ValidateScheduleStartCannotBeInThePast()
        {
            // Arrange
            DateTime testDateTime = new DateTime(2017, 10, 9, 13, 0, 0);  // 1:00 PM
            IDateTimeProvider dateTimeProvider = new UnitTestDateTimeProvider(testDateTime);

            CreateFoodTruckScheduleModel model = new CreateFoodTruckScheduleModel()
            {
                LocationId = 1,
                StartTime = new DateTime(2017, 10, 9, hour: 10, minute: 0, second: 0),
                EndTime = new DateTime(2017, 10, 9, hour: 14, minute: 0, second: 0)
            };

            // Act
            var validator = new CreateFoodTruckScheduleModelValidator(dateTimeProvider);
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }


        [Fact]
        public void ValidateScheduleStartMustBeBeforeScheduleEnd()
        {
            // Arrange
            DateTime testDateTime = new DateTime(2017, 10, 9, 8, 0, 0);  // 1:00 PM
            IDateTimeProvider dateTimeProvider = new UnitTestDateTimeProvider(testDateTime);

            CreateFoodTruckScheduleModel model = new CreateFoodTruckScheduleModel()
            {
                LocationId = 1,
                StartTime = new DateTime(2017, 10, 9, 14, 0, 0),
                EndTime = new DateTime(2017, 10, 9, 12, 0, 0)
            };

            // Act
            var validator = new CreateFoodTruckScheduleModelValidator(dateTimeProvider);
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }


        [Fact]
        public void ValidateScheduleStartAndEndCannotBeTheSame()
        {
            // Arrange
            DateTime testDateTime = new DateTime(2017, 10, 9, 8, 0, 0);  // 1:00 PM
            IDateTimeProvider dateTimeProvider = new UnitTestDateTimeProvider(testDateTime);

            CreateFoodTruckScheduleModel model = new CreateFoodTruckScheduleModel()
            {
                LocationId = 1,
                StartTime = new DateTime(2017, 10, 9, 11, 0, 0),
                EndTime = new DateTime(2017, 10, 9, 11, 0, 0)
            };

            // Act
            var validator = new CreateFoodTruckScheduleModelValidator(dateTimeProvider);
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }



        [Fact]
        public void ValidateScheduleCannotExceed16Hours()
        {
            // Arrange
            DateTime testDateTime = new DateTime(2017, 10, 9, 6, 0, 0);  // 1:00 PM
            IDateTimeProvider dateTimeProvider = new UnitTestDateTimeProvider(testDateTime);

            CreateFoodTruckScheduleModel model = new CreateFoodTruckScheduleModel()
            {
                LocationId = 1,
                StartTime = new DateTime(2017, 10, 14, 6, 0, 0),
                EndTime = new DateTime(2017, 10, 14, 23, 0, 0)
            };

            // Act
            var validator = new CreateFoodTruckScheduleModelValidator(dateTimeProvider);
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }


        [Fact]
        public void Validate16HourEventPasses()
        {
            // Arrange
            DateTime testDateTime = new DateTime(2017, 10, 9, 6, 0, 0);  // 1:00 PM
            IDateTimeProvider dateTimeProvider = new UnitTestDateTimeProvider(testDateTime);

            CreateFoodTruckScheduleModel model = new CreateFoodTruckScheduleModel()
            {
                LocationId = 1,
                StartTime = new DateTime(2017, 10, 14, 7, 0, 0),
                EndTime = new DateTime(2017, 10, 14, 23, 0, 0)
            };

            // Act
            var validator = new CreateFoodTruckScheduleModelValidator(dateTimeProvider);
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeTrue();
        }



    }
}
