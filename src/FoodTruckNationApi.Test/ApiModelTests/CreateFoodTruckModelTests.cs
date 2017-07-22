using FoodTruckNationApi.ApiModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Xunit;


namespace FoodTruckNationApi.Test.ApiModelTests
{
    public class CreateFoodTruckModelTests
    {

        [Fact]
        [Trait("Category", "ApiModel-Validation")]
        public void CreateFoodTruckModelWithBasicValidObject()
        {
            // Arrange
            CreateFoodTruckModel model = new CreateFoodTruckModel()
            {
                Name = "My Food Truck",
                Description = "A simple description of this food truck",
                Website = @"http://simplefoodtruck.com",
                Tags = new List<String>() { "Burgers", "Nachos", "Wings" }
            };

            // Act
            List<ValidationResult> results = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(model, new ValidationContext(model), results, true);

            // Assert
            Assert.True(valid);
        }


        [Fact]
        [Trait("Category", "ApiModel-Validation")]
        public void CreateFoodTruckModelWithSingleQuoteInNameShouldPass()
        {
            // Arrange
            CreateFoodTruckModel model = new CreateFoodTruckModel()
            {
                Name = "David's Food Truck",
                Description = "A simple description of this food truck",
                Website = @"http://foodtruckdavid.com",
                Tags = new List<String>() { "Burgers", "Nachos", "Wings" }
            };

            // Act
            List<ValidationResult> results = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(model, new ValidationContext(model), results, true);

            // Assert
            Assert.True(valid);
        }



        [Fact]
        [Trait("Category", "ApiModel-Validation")]
        public void TestCreateFoodTruckModelWithTooLongOfName()
        {
            // Arrange
            CreateFoodTruckModel model = new CreateFoodTruckModel()
            {
                Name = "This Food Truck has a very very long name",    // 41 chars, max is 40
                Description = "A simple description of this food truck",
                Website = @"http://simplefoodtruck.com",
                Tags = new List<String>() { "Burgers", "Nachos", "Wings" }
            };

            // Act
            List<ValidationResult> results = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(model, new ValidationContext(model), results, true);

            // Assert
            Assert.False(valid);
        }


        [Fact]
        [Trait("Category", "ApiModel-Validation")]
        public void TestCreateFoodTruckModelWithInvalidUrl()
        {
            // Arrange
            CreateFoodTruckModel model = new CreateFoodTruckModel()
            {
                Name = "My Food Truck",
                Description = "A simple description of this food truck",
                Website = @"htp://simplefoodtruck.com",
                Tags = new List<String>() { "Burgers", "Nachos", "Wings" }
            };

            // Act
            List<ValidationResult> results = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(model, new ValidationContext(model), results, true);

            // Assert
            Assert.False(valid);            
        }



        [Fact]
        [Trait("Category", "ApiModel-Validation")]
        public void TestCreateFoodTruckModelWithTagNameTooLong()
        {
            // Arrange
            CreateFoodTruckModel model = new CreateFoodTruckModel()
            {
                Name = "My Food Truck",
                Description = "A simple description of this food truck",
                Website = @"http://simplefoodtruck.com",
                Tags = new List<String>() { "Tag names of 31 characters fail", "Nachos", "Wings" }
            };

            // Act
            List<ValidationResult> results = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(model, new ValidationContext(model), results, true);

            // Assert
            Assert.False(valid);
        }


        [Fact]
        [Trait("Category", "ApiModel-Validation")]
        public void CreateFoodTruckModelWithSpecialCharacterInTagNameShouldFail()
        {
            // Arrange
            CreateFoodTruckModel model = new CreateFoodTruckModel()
            {
                Name = "My Food Truck",
                Description = "A simple description of this food truck",
                Website = @"http://simplefoodtruck.com",
                Tags = new List<String>() { "Burgers@", "Nachos", "Wings" }
            };

            // Act
            List<ValidationResult> results = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(model, new ValidationContext(model), results, true);

            // Assert
            Assert.False(valid);
        }


    }
}
