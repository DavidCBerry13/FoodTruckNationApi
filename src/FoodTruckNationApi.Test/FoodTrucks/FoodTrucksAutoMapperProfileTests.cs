using AutoMapper;
using FluentAssertions;
using FoodTruckNation.Core.Domain;
using FoodTruckNationApi.FoodTrucks;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FoodTruckNationApi.Test.FoodTrucks
{
    public class FoodTrucksAutoMapperProfileTests
    {

        public FoodTrucksAutoMapperProfileTests()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<FoodTruckModelAutomapperProfile>();
            });
            mapper = new Mapper(config,
                t => FoodTrucksAutoMapperProfileTests.Resolve<Type, object>(t));
        }

        private readonly IMapper mapper;

        [Fact]
        public void FoodTruckModelCorrectlyMapsFoodTruckWithTags()
        {
            // Arrange
            Tag tagOne = new Tag(1, "Burgers");
            Tag tagTwo = new Tag(2, "Hot Dogs");
            var foodTruck = new FoodTruck(1, "All American Burger", "Burgers, Brats, Hot Dogs and More", @"http://allamericanburger.foodtruck.com");
            foodTruck.AddTag(tagOne);
            foodTruck.AddTag(tagTwo);

            // Act
            var model = mapper.Map<FoodTruckModel>(foodTruck);

            // Assert
            model.Tags.Should().HaveCount(2);
            model.Tags.Should().Contain("Burgers");
            model.Tags.Should().Contain("Hot Dogs");
        }


        [Fact]
        public void FoodTruckModelCorrectlyMapsFoodTruckWithoutTags()
        {
            // Arrange
            var foodTruck = new FoodTruck(1, "All American Burger", "Burgers, Brats, Hot Dogs and More", @"http://allamericanburger.foodtruck.com");

            // Act
            var model = mapper.Map<FoodTruckModel>(foodTruck);

            // Assert
            model.Tags.Should().BeEmpty();
        }


        // Resolver method so AutoMapper will resolve the TestUrlResolver when it goes looking for the UrlResolver
        private static object Resolve<Type, Object>(Type t)
        {
            return new TestUrlResolver();
        }

    }
}
