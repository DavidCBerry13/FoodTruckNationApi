using FluentAssertions;
using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.Domain;
using FoodTruckNationApi.FoodTrucks;
using DavidBerry.Framework.Functional;
using Moq;
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
            var mockSocialMediaService = new Mock<ISocialMediaPlatformService>();
            mockSocialMediaService.Setup(m => m.GetAllSocialMediaPlatforms())
                .Returns(Result.Success<List<SocialMediaPlatform>>(new List<SocialMediaPlatform>()));

            // Act
            var validator = new CreateFoodTruckModelV11Validator(mockSocialMediaService.Object);
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
            var mockSocialMediaService = new Mock<ISocialMediaPlatformService>();
            mockSocialMediaService.Setup(m => m.GetAllSocialMediaPlatforms())
                .Returns(Result.Success<List<SocialMediaPlatform>>(new List<SocialMediaPlatform>()));

            // Act
            var validator = new CreateFoodTruckModelV11Validator(mockSocialMediaService.Object);
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
            var mockSocialMediaService = new Mock<ISocialMediaPlatformService>();
            mockSocialMediaService.Setup(m => m.GetAllSocialMediaPlatforms())
                .Returns(Result.Success<List<SocialMediaPlatform>>(new List<SocialMediaPlatform>()));

            // Act
            var validator = new CreateFoodTruckModelV11Validator(mockSocialMediaService.Object);
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
            var mockSocialMediaService = new Mock<ISocialMediaPlatformService>();
            mockSocialMediaService.Setup(m => m.GetAllSocialMediaPlatforms())
                .Returns(Result.Success<List<SocialMediaPlatform>>(new List<SocialMediaPlatform>()));

            // Act
            var validator = new CreateFoodTruckModelV11Validator(mockSocialMediaService.Object);
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
            var mockSocialMediaService = new Mock<ISocialMediaPlatformService>();
            mockSocialMediaService.Setup(m => m.GetAllSocialMediaPlatforms())
                .Returns(Result.Success<List<SocialMediaPlatform>>(new List<SocialMediaPlatform>()));

            // Act
            var validator = new CreateFoodTruckModelV11Validator(mockSocialMediaService.Object);
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
            var mockSocialMediaService = new Mock<ISocialMediaPlatformService>();
            mockSocialMediaService.Setup(m => m.GetAllSocialMediaPlatforms())
                .Returns(Result.Success<List<SocialMediaPlatform>>(new List<SocialMediaPlatform>()));

            // Act
            var validator = new CreateFoodTruckModelV11Validator(mockSocialMediaService.Object);
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
            var mockSocialMediaService = new Mock<ISocialMediaPlatformService>();
            mockSocialMediaService.Setup(m => m.GetAllSocialMediaPlatforms())
                .Returns(Result.Success<List<SocialMediaPlatform>>(new List<SocialMediaPlatform>()));

            // Act
            var validator = new CreateFoodTruckModelV11Validator(mockSocialMediaService.Object);
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
            var mockSocialMediaService = new Mock<ISocialMediaPlatformService>();
            mockSocialMediaService.Setup(m => m.GetAllSocialMediaPlatforms())
                .Returns(Result.Success<List<SocialMediaPlatform>>(new List<SocialMediaPlatform>()));

            // Act
            var validator = new CreateFoodTruckModelV11Validator(mockSocialMediaService.Object);
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
            var mockSocialMediaService = new Mock<ISocialMediaPlatformService>();
            mockSocialMediaService.Setup(m => m.GetAllSocialMediaPlatforms())
                .Returns(Result.Success<List<SocialMediaPlatform>>(new List<SocialMediaPlatform>()));

            // Act
            var validator = new CreateFoodTruckModelV11Validator(mockSocialMediaService.Object);
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
            var mockSocialMediaService = new Mock<ISocialMediaPlatformService>();
            mockSocialMediaService.Setup(m => m.GetAllSocialMediaPlatforms())
                .Returns(Result.Success<List<SocialMediaPlatform>>(new List<SocialMediaPlatform>()));

            // Act
            var validator = new CreateFoodTruckModelV11Validator(mockSocialMediaService.Object);
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
            var mockSocialMediaService = new Mock<ISocialMediaPlatformService>();
            mockSocialMediaService.Setup(m => m.GetAllSocialMediaPlatforms())
                .Returns(Result.Success<List<SocialMediaPlatform>>(new List<SocialMediaPlatform>()));

            // Act
            var validator = new CreateFoodTruckModelV11Validator(mockSocialMediaService.Object);
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
            var mockSocialMediaService = new Mock<ISocialMediaPlatformService>();
            mockSocialMediaService.Setup(m => m.GetAllSocialMediaPlatforms())
                .Returns(Result.Success<List<SocialMediaPlatform>>(new List<SocialMediaPlatform>()));

            // Act
            var validator = new CreateFoodTruckModelV11Validator(mockSocialMediaService.Object);
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }


        [Fact]
        public void SocialMediaAccountNamePassesWhenValidNameIsIncluded()
        {
            // Arrange
            var model = new CreateFoodTruckModelV11()
            {
                Name = "Food Truck Name",
                Description = "Some very interesting description",
                Website = @"http://www.foodtruck.com",
                Tags = new List<string>() { "Burgers", "Pizza" },
                SocialMediaAccounts = new List<CreateFoodTruckModelV11.SocialMediaAccountModel>()
                {
                    new CreateFoodTruckModelV11.SocialMediaAccountModel()
                    {
                        SocialMediaPlatformId = 1,
                        AccountName = "FoodTruck"
                    }
                }
            };
            var mockSocialMediaService = new Mock<ISocialMediaPlatformService>();
            mockSocialMediaService.Setup(m => m.GetAllSocialMediaPlatforms())
                .Returns(Result.Success<List<SocialMediaPlatform>>(new List<SocialMediaPlatform>()
                {
                    new SocialMediaPlatform(1, "FooTruckMedia", "",  @"\w{1,20}")
                }));

            // Act
            var validator = new CreateFoodTruckModelV11Validator(mockSocialMediaService.Object);
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
                SocialMediaAccounts = new List<CreateFoodTruckModelV11.SocialMediaAccountModel>()
                {
                    new CreateFoodTruckModelV11.SocialMediaAccountModel()
                    {
                        SocialMediaPlatformId = 1,
                        AccountName = ""
                    }
                }
            };
            var mockSocialMediaService = new Mock<ISocialMediaPlatformService>();
            mockSocialMediaService.Setup(m => m.GetAllSocialMediaPlatforms())
                .Returns(Result.Success<List<SocialMediaPlatform>>(new List<SocialMediaPlatform>()
                {
                    new SocialMediaPlatform(1, "FooTruckMedia", "",  @"\w{1,20}")
                }));


            // Act
            var validator = new CreateFoodTruckModelV11Validator(mockSocialMediaService.Object);
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }


        [Fact]
        public void SocialMediaAccountFailsWhenPlatformDoesNotExist()
        {
            // Arrange
            var model = new CreateFoodTruckModelV11()
            {
                Name = "Food Truck Name",
                Description = "Some very interesting description",
                Website = @"http://www.foodtruck.com",
                Tags = new List<string>() { "Burgers", "Pizza" },
                SocialMediaAccounts = new List<CreateFoodTruckModelV11.SocialMediaAccountModel>()
                {
                    new CreateFoodTruckModelV11.SocialMediaAccountModel()
                    {
                        SocialMediaPlatformId = 2,
                        AccountName = "MostAwesomeFoodTruck"
                    }
                }
            };
            var mockSocialMediaService = new Mock<ISocialMediaPlatformService>();
            mockSocialMediaService.Setup(m => m.GetAllSocialMediaPlatforms())
                .Returns(Result.Success<List<SocialMediaPlatform>>(new List<SocialMediaPlatform>()
                {
                    new SocialMediaPlatform(1, "FooTruckMedia", "",  @"\w{1,20}")
                }));


            // Act
            var validator = new CreateFoodTruckModelV11Validator(mockSocialMediaService.Object);
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }

    }
}
