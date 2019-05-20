using FluentAssertions;
using FoodTruckNationApi.Locations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FoodTruckNationApi.Test.Locations
{
    public class LocationModelValidatorTest
    {

        public LocationModelValidatorTest()
        {
            _locationOne = new CreateLocationModel()
            {
                Name = "US Bank Building",
                StreetAddress = "777 E Wisconsin Ave",
                City = "Milwaukee",
                State = "WI",
                ZipCode = "53202"
            };
        }


        private readonly CreateLocationModel _locationOne;


        [Theory]
        [InlineData("US Bank Center", "777 E Wisconsin Ave", "Milwaukee", "WI", "53202")]
        [InlineData("U.S. Bank Center", "777 E Wisconsin Ave", "Milwaukee", "WI", "53202")]
        [InlineData("U.S. Bank Center", "777 E. Wisconsin Ave", "Milwaukee", "WI", "53202")]
        [InlineData("L'Enfant Plaza", "429 L'Enfant Plaza SW", "Washington", "DC", "20024")]
        [InlineData("Willis Tower-Wacker Drive", "200 S Wacker Dr", "Chicago", "IL", "60606")]
    public void ValidLocationObjectsShouldPass(string name, string address, string city, 
            string state, string zipCode)
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
        public void InvalidLocationNamesShouldFail(string name)
        {
            // Arrange
            _locationOne.Name = name;

            // Act
            CreateLocationModelValidator validator = new CreateLocationModelValidator();
            var result = validator.Validate(_locationOne);

            // Assert
            result.IsValid.Should().BeFalse();
        }


        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void InvalidStreetAddressesShouldFail(string streetAddress)
        {
            // Arrange
            _locationOne.StreetAddress = streetAddress;

            // Act
            CreateLocationModelValidator validator = new CreateLocationModelValidator();
            var result = validator.Validate(_locationOne);

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void InvalidCityShouldFail(string city)
        {
            // Arrange
            _locationOne.City = city;

            // Act
            CreateLocationModelValidator validator = new CreateLocationModelValidator();
            var result = validator.Validate(_locationOne);

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
        public void InvalidStateShouldFail(string state)
        {
            // Arrange
            _locationOne.State = state;

            // Act
            CreateLocationModelValidator validator = new CreateLocationModelValidator();
            var result = validator.Validate(_locationOne);

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
        public void InvalidZipCodeShouldFail(string zipCode)
        {
            // Arrange
            _locationOne.ZipCode = zipCode;

            // Act
            CreateLocationModelValidator validator = new CreateLocationModelValidator();
            var result = validator.Validate(_locationOne);

            // Assert
            result.IsValid.Should().BeFalse();
        }

    }
}
