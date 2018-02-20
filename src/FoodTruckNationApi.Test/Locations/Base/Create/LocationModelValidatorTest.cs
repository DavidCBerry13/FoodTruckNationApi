using FluentAssertions;
using FoodTruckNationApi.Locations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FoodTruckNationApi.Test.Locations.Base.Create
{
    public class LocationModelValidatorTest
    {

        public LocationModelValidatorTest()
        {
            locationOne = new CreateLocationModel()
            {
                Name = "US Bank Building",
                StreetAddress = "777 E Wisconsin Ave",
                City = "Milwaukee",
                State = "WI",
                ZipCode = "53202"
            };
        }


        private CreateLocationModel locationOne;


        [Theory]
        [InlineData("US Bank Center", "777 E Wisconsin Ave", "Milwaukee", "WI", "53202")]
        [InlineData("U.S. Bank Center", "777 E Wisconsin Ave", "Milwaukee", "WI", "53202")]
        [InlineData("U.S. Bank Center", "777 E. Wisconsin Ave", "Milwaukee", "WI", "53202")]
        [InlineData("L'Enfant Plaza", "429 L'Enfant Plaza SW", "Washington", "DC", "20024")] 
        public void ValidLocationObjectsShouldPass(String name, String address, String city, 
            String state, String zipCode)
        {
            // Arrange
            CreateLocationModel model = new CreateLocationModel()
            {
                Name = name,
                StreetAddress = address,
                City = city,
                State = state,
                ZipCode = zipCode
            };

            // Act
            CreateLocationModelValidator validator = new CreateLocationModelValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeTrue();
        }



        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void InvalidLocationNamesShouldFail(String name)
        {
            // Arrange
            locationOne.Name = name;

            // Act
            CreateLocationModelValidator validator = new CreateLocationModelValidator();
            var result = validator.Validate(locationOne);

            // Assert
            result.IsValid.Should().BeFalse();
        }


        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void InvalidStreetAddressesShouldFail(String streetAddress)
        {
            // Arrange
            locationOne.StreetAddress = streetAddress;

            // Act
            CreateLocationModelValidator validator = new CreateLocationModelValidator();
            var result = validator.Validate(locationOne);

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void InvalidCityShouldFail(String city)
        {
            // Arrange
            locationOne.City = city;

            // Act
            CreateLocationModelValidator validator = new CreateLocationModelValidator();
            var result = validator.Validate(locationOne);

            // Assert
            result.IsValid.Should().BeFalse();
        }


        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("WIS")]
        [InlineData("W")]
        [InlineData("WS")]
        public void InvalidStateShouldFail(String state)
        {
            // Arrange
            locationOne.State = state;

            // Act
            CreateLocationModelValidator validator = new CreateLocationModelValidator();
            var result = validator.Validate(locationOne);

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("5320")]
        [InlineData("53202-1010")]
        [InlineData("532022")]
        [InlineData("5320A")]
        [InlineData(" 53202")]
        [InlineData("53202 ")]
        [InlineData("53 202")]
        public void InvalidZipCodeShouldFail(String zipCode)
        {
            // Arrange
            locationOne.ZipCode = zipCode;

            // Act
            CreateLocationModelValidator validator = new CreateLocationModelValidator();
            var result = validator.Validate(locationOne);

            // Assert
            result.IsValid.Should().BeFalse();
        }

    }
}
