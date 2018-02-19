using FluentAssertions;
using FoodTruckNationApi.FoodTrucks.Create;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FoodTruckNationApi.Test.FoodTrucks.Base.Create
{
    public class CreateFoodTruckModelValidatorTest
    {




        [Theory]
        [InlineData("Little Havana")]
        [InlineData("Burger-o-rama")]
        [InlineData("Hoppin' Jalepeno")]
        public void ValidFoodTruckNamesPass(String name)
        {
            // Arrange
            CreateFoodTruckModel model = new CreateFoodTruckModel()
            {
                Name = name,
                Description = "Some very interesting description",
                Website = @"http://www.foodtruck.com",
                Tags = new List<string>() {  "Burgers", "Sandwiches"}
            };

            // Act
            CreateFoodTruckModelValidator validator = new CreateFoodTruckModelValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeTrue();
        }



    }
}
