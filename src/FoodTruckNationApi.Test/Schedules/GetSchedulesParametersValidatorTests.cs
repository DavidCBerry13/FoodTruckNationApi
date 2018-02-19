using FluentAssertions;
using FoodTruckNationApi.Schedules.Get;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FoodTruckNationApi.Test.Schedules
{
    public class GetSchedulesParametersValidatorTests
    {

        [Fact]
        public void WhenStartAndEndDateAreNull_Passes()
        {
            // Arrange
            var model = new GetSchedulesParameters();

            // Act
            var validator = new GetSchedulesParametersValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void WhenStartAndEndDateProvidedInCorrectOrder_Passes()
        {
            // Arrange
            var model = new GetSchedulesParameters();
            model.StartDate = DateTime.Today;
            model.EndDate = DateTime.Today.AddDays(10);

            // Act
            var validator = new GetSchedulesParametersValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void WhenOnlyStartDateProvided_Fails()
        {
            // Arrange
            var model = new GetSchedulesParameters();
            model.StartDate = DateTime.Today;

            // Act
            var validator = new GetSchedulesParametersValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void WhenOnlyEndDateProvided_Fails()
        {
            // Arrange
            var model = new GetSchedulesParameters();
            model.EndDate = DateTime.Today.AddDays(10);

            // Act
            var validator = new GetSchedulesParametersValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void WhenOnlyEndDateBeforeStartDate_Fails()
        {
            // Arrange
            var model = new GetSchedulesParameters();
            model.StartDate = DateTime.Today.AddDays(10);
            model.EndDate = DateTime.Today;

            // Act
            var validator = new GetSchedulesParametersValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void WhenEndDate60DaysAfterStartDate_Passes()
        {
            // Arrange
            var model = new GetSchedulesParameters();
            model.StartDate = DateTime.Today;
            model.EndDate = DateTime.Today.AddDays(60);

            // Act
            var validator = new GetSchedulesParametersValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void WhenEndDate61DaysAfterStartDate_Passes()
        {
            // Arrange
            var model = new GetSchedulesParameters();
            model.StartDate = DateTime.Today.AddDays(10);
            model.EndDate = DateTime.Today;

            // Act
            var validator = new GetSchedulesParametersValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }


    }
}
