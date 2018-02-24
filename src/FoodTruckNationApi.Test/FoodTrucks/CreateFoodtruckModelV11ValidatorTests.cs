using FluentAssertions;
using FoodTruckNationApi.FoodTrucks;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FoodTruckNationApi.Test.FoodTrucks
{
    public class CreateFoodtruckModelV11ValidatorTests
    {

        [Theory]
        [InlineData("Little Havana")]
        [InlineData("Burger-o-rama")]
        [InlineData("Hoppin' Jalepeno")]
        public void ValidFoodTruckNamesPass(String name)
        {
            // Arrange
            CreateFoodTruckModelV11 model = new CreateFoodTruckModelV11()
            {
                Name = name,
                Description = "Some very interesting description",
                Website = @"http://www.foodtruck.com",
                Tags = new List<string>() { "Burgers", "Sandwiches" }
            };

            // Act
            var validator = new CreateFoodTruckModelV11Validator();
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
            var model = new CreateFoodTruckModelV11()
            {
                Name = name,
                Description = "Some very interesting description",
                Website = @"http://www.foodtruck.com",
                Tags = new List<string>() { "Burgers", "Sandwiches" }
            };

            // Act
            var validator = new CreateFoodTruckModelV11Validator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }


        [Fact]
        public void FoodTruckWithEmptyNameFails()
        {
            // Arrange
            var model = new CreateFoodTruckModelV11()
            {
                Name = "",
                Description = "Some very interesting description",
                Website = @"http://www.foodtruck.com",
                Tags = new List<string>() { "Burgers", "Sandwiches" }
            };

            // Act
            var validator = new CreateFoodTruckModelV11Validator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void FoodTruckWithNullNameFails()
        {
            // Arrange
            var model = new CreateFoodTruckModelV11()
            {
                Name = null,
                Description = "Some very interesting description",
                Website = @"http://www.foodtruck.com",
                Tags = new List<string>() { "Burgers", "Sandwiches" }
            };

            // Act
            var validator = new CreateFoodTruckModelV11Validator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }


        [Fact]
        public void FoodTruckWithOneCharacterNameFails()
        {
            // Arrange
            var model = new CreateFoodTruckModelV11()
            {
                Name = "X",
                Description = "Some very interesting description",
                Website = @"http://www.foodtruck.com",
                Tags = new List<string>() { "Burgers", "Sandwiches" }
            };

            // Act
            var validator = new CreateFoodTruckModelV11Validator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }


        [Fact]
        public void FoodTruckWith41CharacterNameFails()
        {
            // Arrange
            var model = new CreateFoodTruckModelV11()
            {
                Name = "One really looooong name for a food truck",
                Description = "Some very interesting description",
                Website = @"http://www.foodtruck.com",
                Tags = new List<string>() { "Burgers", "Sandwiches" }
            };

            // Act
            var validator = new CreateFoodTruckModelV11Validator();
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
            var model = new CreateFoodTruckModelV11()
            {
                Name = "Food Truck Name",
                Description = "Some very interesting description",
                Website = website,
                Tags = new List<string>() { "Burgers", "Sandwiches" }
            };

            // Act
            var validator = new CreateFoodTruckModelV11Validator();
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
            var model = new CreateFoodTruckModelV11()
            {
                Name = "Food Truck Name",
                Description = "Some very interesting description",
                Website = website,
                Tags = new List<string>() { "Burgers", "Sandwiches" }
            };

            // Act
            var validator = new CreateFoodTruckModelV11Validator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }



        [Fact]
        public void TagsArrayCanBeEmptyList()
        {
            // Arrange
            var model = new CreateFoodTruckModelV11()
            {
                Name = "Food Truck Name",
                Description = "Some very interesting description",
                Website = @"http://www.foodtruck.com",
                Tags = new List<string>()
            };

            // Act
            var validator = new CreateFoodTruckModelV11Validator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void TagsArrayCanBeNull()
        {
            // Arrange
            var model = new CreateFoodTruckModelV11()
            {
                Name = "Food Truck Name",
                Description = "Some very interesting description",
                Website = @"http://www.foodtruck.com",
                Tags = null
            };

            // Act
            var validator = new CreateFoodTruckModelV11Validator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeTrue();
        }


        [Fact]
        public void TagsArrayCannotContainEmptyElements()
        {
            // Arrange
            var model = new CreateFoodTruckModelV11()
            {
                Name = "Food Truck Name",
                Description = "Some very interesting description",
                Website = @"http://www.foodtruck.com",
                Tags = new List<string>() { "Burgers", "" }
            };

            // Act
            var validator = new CreateFoodTruckModelV11Validator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }


        [Fact]
        public void TagsArrayCannotContainInvalidEntries()
        {
            // Arrange
            var model = new CreateFoodTruckModelV11()
            {
                Name = "Food Truck Name",
                Description = "Some very interesting description",
                Website = @"http://www.foodtruck.com",
                Tags = new List<string>() { "Burgers", "Pizza@" }
            };

            // Act
            var validator = new CreateFoodTruckModelV11Validator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }


        [Fact]
        public void SocialMediaAccountNamePassesWhenNameIsIncluded()
        {
            // Arrange
            var model = new CreateFoodTruckModelV11()
            {
                Name = "Food Truck Name",
                Description = "Some very interesting description",
                Website = @"http://www.foodtruck.com",
                Tags = new List<string>() { "Burgers", "Pizza" },
                SocialMediaAccounts = new List<CreateFoodTruckModelV11.CreateFoodTruckSocialMediaAccountModelV11>()
                {
                    new CreateFoodTruckModelV11.CreateFoodTruckSocialMediaAccountModelV11()
                    {
                        SocialMediaPlatformId = 1,
                        AccountName = "FoodTruck"
                    }
                }
            };

            // Act
            var validator = new CreateFoodTruckModelV11Validator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeTrue();
        }


        [Fact]
        public void SocialMediaAccountNameCannotBeEmpty()
        {
            // Arrange
            var model = new CreateFoodTruckModelV11()
            {
                Name = "Food Truck Name",
                Description = "Some very interesting description",
                Website = @"http://www.foodtruck.com",
                Tags = new List<string>() { "Burgers", "Pizza" },
                SocialMediaAccounts = new List<CreateFoodTruckModelV11.CreateFoodTruckSocialMediaAccountModelV11>()
                {
                    new CreateFoodTruckModelV11.CreateFoodTruckSocialMediaAccountModelV11()
                    {
                        SocialMediaPlatformId = 1,
                        AccountName = ""
                    }
                }
            };

            // Act
            var validator = new CreateFoodTruckModelV11Validator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }


    }
}
