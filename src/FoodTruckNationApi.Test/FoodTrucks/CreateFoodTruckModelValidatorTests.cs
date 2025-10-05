using FluentAssertions;
using FoodTruckNationApi.FoodTrucks;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FoodTruckNationApi.Test.FoodTrucks
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
                LocalityCode = "CHI",
                Tags = new List<string>() { "Burgers", "Sandwiches" }
            };

            // Act
            CreateFoodTruckModelValidator validator = new CreateFoodTruckModelValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeTrue();
        }


        [Theory]
        [InlineData("Name With %")]
        [InlineData("Name with ^")]
        [InlineData("Name with &")]
        public void InvalidFoodTruckNamesFail(String name)
        {
            // Arrange
            CreateFoodTruckModel model = new CreateFoodTruckModel()
            {
                Name = name,
                Description = "Some very interesting description",
                Website = @"http://www.foodtruck.com",
                Tags = new List<string>() { "Burgers", "Sandwiches" }
            };

            // Act
            CreateFoodTruckModelValidator validator = new CreateFoodTruckModelValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }


        [Fact]
        public void FoodTruckWithEmptyNameFails()
        {
            // Arrange
            CreateFoodTruckModel model = new CreateFoodTruckModel()
            {
                Name = "",
                Description = "Some very interesting description",
                Website = @"http://www.foodtruck.com",
                Tags = new List<string>() { "Burgers", "Sandwiches" }
            };

            // Act
            CreateFoodTruckModelValidator validator = new CreateFoodTruckModelValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void FoodTruckWithNullNameFails()
        {
            // Arrange
            CreateFoodTruckModel model = new CreateFoodTruckModel()
            {
                Name = null,
                Description = "Some very interesting description",
                Website = @"http://www.foodtruck.com",
                Tags = new List<string>() { "Burgers", "Sandwiches" }
            };

            // Act
            CreateFoodTruckModelValidator validator = new CreateFoodTruckModelValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }


        [Fact]
        public void FoodTruckWithOneCharacterNameFails()
        {
            // Arrange
            CreateFoodTruckModel model = new CreateFoodTruckModel()
            {
                Name = "X",
                Description = "Some very interesting description",
                Website = @"http://www.foodtruck.com",
                Tags = new List<string>() { "Burgers", "Sandwiches" }
            };

            // Act
            CreateFoodTruckModelValidator validator = new CreateFoodTruckModelValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }


        [Fact]
        public void FoodTruckWith41CharacterNameFails()
        {
            // Arrange
            CreateFoodTruckModel model = new CreateFoodTruckModel()
            {
                Name = "One really looooong name for a food truck",
                Description = "Some very interesting description",
                Website = @"http://www.foodtruck.com",
                Tags = new List<string>() { "Burgers", "Sandwiches" }
            };

            // Act
            CreateFoodTruckModelValidator validator = new CreateFoodTruckModelValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Theory]
        [InlineData(@"http://www.foodtruck.com")]
        [InlineData(@"https://www.foodtruck.com")]
        [InlineData(@"http://foodtruck.com")]
        [InlineData(@"https://foodtruck.com")]
        [InlineData(@"http://www.foodtruck.com/TheFoodTruck")]
        [InlineData(@"https://www.foodtruck.com/TheFoodTruck")]
        [InlineData(@"http://www.foodtruck.net")]
        [InlineData(@"https://www.foodtruck.net")]
        public void ValidWebsitesPass(String website)
        {
            // Arrange
            CreateFoodTruckModel model = new CreateFoodTruckModel()
            {
                Name = "Food Truck Name",
                Description = "Some very interesting description",
                Website = website,
                LocalityCode = "CHI",
                Tags = new List<string>() { "Burgers", "Sandwiches" }
            };

            // Act
            CreateFoodTruckModelValidator validator = new CreateFoodTruckModelValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeTrue();
        }


        [Theory]
        [InlineData(@"www.foodtruck.com")]
        [InlineData(@"ftp://www.foodtruck.com")]
        [InlineData(@"http://foodtruck")]
        [InlineData(@"https://foodtruck")]
        [InlineData(@"http:/www.foodtruck.net")]
        [InlineData(@"https:/www.foodtruck.net")]
        [InlineData(@"htp://www.foodtruck.net")]
        [InlineData(@"htps://www.foodtruck.net")]
        public void InvalidWebsitesFail(String website)
        {
            // Arrange
            CreateFoodTruckModel model = new CreateFoodTruckModel()
            {
                Name = "Food Truck Name",
                Description = "Some very interesting description",
                Website = website,
                Tags = new List<string>() { "Burgers", "Sandwiches" }
            };

            // Act
            CreateFoodTruckModelValidator validator = new CreateFoodTruckModelValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }



        [Fact]
        public void TagsArrayCanBeEmptyList()
        {
            // Arrange
            CreateFoodTruckModel model = new CreateFoodTruckModel()
            {
                Name = "Food Truck Name",
                Description = "Some very interesting description",
                Website = @"http://www.foodtruck.com",
                LocalityCode = "CHI",
                Tags = new List<string>()
            };

            // Act
            CreateFoodTruckModelValidator validator = new CreateFoodTruckModelValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void TagsArrayCanBeNull()
        {
            // Arrange
            CreateFoodTruckModel model = new CreateFoodTruckModel()
            {
                Name = "Food Truck Name",
                Description = "Some very interesting description",
                Website = @"http://www.foodtruck.com",
                LocalityCode = "CHI",
                Tags = null
            };

            // Act
            CreateFoodTruckModelValidator validator = new CreateFoodTruckModelValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeTrue();
        }


        [Fact]
        public void TagsArrayCannotContainEmptyElements()
        {
            // Arrange
            CreateFoodTruckModel model = new CreateFoodTruckModel()
            {
                Name = "Food Truck Name",
                Description = "Some very interesting description",
                Website = @"http://www.foodtruck.com",
                Tags = new List<string>() { "Burgers", "" }
            };

            // Act
            CreateFoodTruckModelValidator validator = new CreateFoodTruckModelValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }


        [Fact]
        public void TagsArrayCannotContainInvalidEntries()
        {
            // Arrange
            CreateFoodTruckModel model = new CreateFoodTruckModel()
            {
                Name = "Food Truck Name",
                Description = "Some very interesting description",
                Website = @"http://www.foodtruck.com",
                Tags = new List<string>() { "Burgers", "Pizza@" }
            };

            // Act
            CreateFoodTruckModelValidator validator = new CreateFoodTruckModelValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }


    }
}
