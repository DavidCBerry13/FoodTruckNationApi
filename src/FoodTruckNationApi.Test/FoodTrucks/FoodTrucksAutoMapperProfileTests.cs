using AutoMapper;
using Shouldly;
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
            _mapper = new Mapper(config,
                t => FoodTrucksAutoMapperProfileTests.Resolve<Type, object>(t));
        }

        private readonly IMapper _mapper;

        [Fact]
        public void FoodTruckModelCorrectlyMapsFoodTruckWithTags()
        {
            // Arrange
            Locality locality = new Locality() {  LocalityCode = "CHI", Name = "Chicago" };
            Tag tagOne = new Tag(1, "Burgers");
            Tag tagTwo = new Tag(2, "Hot Dogs");
            var foodTruck = new FoodTruck(1, "All American Burger", "Burgers, Brats, Hot Dogs and More", @"http://allamericanburger.foodtruck.com", locality);
            foodTruck.AddTag(tagOne);
            foodTruck.AddTag(tagTwo);

            // Act
            var model = _mapper.Map<FoodTruckModel>(foodTruck);

            // Assert
            model.Tags.Count.ShouldBe(2);
            model.Tags.ShouldContain("Burgers");
            model.Tags.ShouldContain("Hot Dogs");
        }


        [Fact]
        public void FoodTruckModelCorrectlyMapsFoodTruckWithoutTags()
        {
            // Arrange
            Locality locality = new Locality() { LocalityCode = "CHI", Name = "Chicago" };
            var foodTruck = new FoodTruck(1, "All American Burger", "Burgers, Brats, Hot Dogs and More", @"http://allamericanburger.foodtruck.com", locality);

            // Act
            var model = _mapper.Map<FoodTruckModel>(foodTruck);

            // Assert
            model.Tags.ShouldBeEmpty();
        }


        // Resolver method so AutoMapper will resolve the TestUrlResolver when it goes looking for the UrlResolver
        private static TestUrlResolver Resolve<TType, TObject>(TType t)
        {
            return new TestUrlResolver();
        }

    }
}
