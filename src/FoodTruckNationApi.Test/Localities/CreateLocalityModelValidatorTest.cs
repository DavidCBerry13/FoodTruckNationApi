using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using FoodTruckApi.Localities.Models;
using FoodTruckNationApi.Locations;
using Xunit;

namespace FoodTruckNationApi.Test.Localities
{
    public class CreateLocalityModelValidatorTest
    {


        [Theory]
        [InlineData("CHI", "Chicago")]
        [InlineData("MKE", "Milwaukee")]
        [InlineData("STL", "St. Louis")]
        public void ValidLocalityObjectsShouldPass(string code, string name)
        {
            // Arrange
            CreateLocalityModel model = new CreateLocalityModel()
            {
                Code = code,
                Name = name
            };

            // Act
            CreateLocalityModelValidator validator = new CreateLocalityModelValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeTrue();
        }



        [Theory]
        [InlineData("C", "Chicago")]
        [InlineData("M", "Milwaukee")]
        [InlineData("D", "Denver")]
        public void LocalityCodeWithOneLetterShouldFail(string code, string name)
        {
            // Arrange
            CreateLocalityModel model = new CreateLocalityModel()
            {
                Code = code,
                Name = name
            };
            
            // Act
            CreateLocalityModelValidator validator = new CreateLocalityModelValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Theory]
        [InlineData("CHICAGO", "Chicago")]
        [InlineData("MILWAUKEE", "Milwaukee")]
        [InlineData("DENVER", "Denver")]
        public void LocalityCodeWithLongCodesShouldFail(string code, string name)
        {
            // Arrange
            CreateLocalityModel model = new CreateLocalityModel()
            {
                Code = code,
                Name = name
            };

            // Act
            CreateLocalityModelValidator validator = new CreateLocalityModelValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Theory]
        [InlineData("Chi", "Chicago")]
        [InlineData("mke", "Milwaukee")]
        [InlineData("dEN", "Denver")]
        public void LocalityCodeWithLowerCaseLettersShouldFail(string code, string name)
        {
            // Arrange
            CreateLocalityModel model = new CreateLocalityModel()
            {
                Code = code,
                Name = name
            };

            // Act
            CreateLocalityModelValidator validator = new CreateLocalityModelValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }


        [Theory]
        [InlineData("CHI ", "Chicago")]
        [InlineData("N Y", "New York")]
        public void LocalityCodeWithSpacesShouldFail(string code, string name)
        {
            // Arrange
            CreateLocalityModel model = new CreateLocalityModel()
            {
                Code = code,
                Name = name
            };

            // Act
            CreateLocalityModelValidator validator = new CreateLocalityModelValidator();
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
        }

    }
}
