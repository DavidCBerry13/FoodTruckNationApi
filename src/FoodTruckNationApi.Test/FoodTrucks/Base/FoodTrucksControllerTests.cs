using AutoMapper;
using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.Domain;
using FoodTruckNationApi.FoodTrucks;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FoodTruckNationApi.Test.FoodTrucks.Base
{
    public class FoodTrucksControllerTests
    {
        public FoodTrucksControllerTests()
        {
            Tag tagOne = new Tag(1, "Tag One");
            Tag tagTwo = new Tag(2, "Tag Two");
            Tag tagThree = new Tag(3, "Tag Three");
            Tag tagFour = new Tag(4, "Tag Four");

            SocialMediaPlatform facebook = new SocialMediaPlatform(1, "Facebook", "http://facebook.com/{0}", null);
            SocialMediaPlatform twitter = new SocialMediaPlatform(2, "Facebook", "http://twitter.com/{0}", null);
            SocialMediaPlatform instagram = new SocialMediaPlatform(3, "Facebook", "http://instagram.com/{0}", null);

            foodTruckOne = new FoodTruck(1, "Food Truck One", "Food Truck One Description", @"http://www.foodtruckone.com");
            foodTruckOne.AddTag(tagOne);
            foodTruckOne.AddTag(tagTwo);
            foodTruckOne.AddSocialMediaAccount(new SocialMediaAccount(facebook, foodTruckOne, "foodTruckOne"));
            foodTruckOne.AddSocialMediaAccount(new SocialMediaAccount(twitter, foodTruckOne, "foodTruckOne"));

            foodTruckTwo = new FoodTruck(2, "Food Truck Two", "Food Truck Two Description", @"http://www.foodtrucktwo.com");
            foodTruckTwo.AddTag(tagThree);
            foodTruckOne.AddSocialMediaAccount(new SocialMediaAccount(facebook, foodTruckTwo, "foodTruckTwo"));

            foodTruckThree = new FoodTruck(3, "Food Truck Two", "Food Truck Three Description", @"http://www.foodtruckthree.com");
            foodTruckThree.AddTag(tagOne);
            foodTruckThree.AddTag(tagFour);
            foodTruckThree.AddSocialMediaAccount(new SocialMediaAccount(facebook, foodTruckThree, "foodTruckThree"));
            foodTruckThree.AddSocialMediaAccount(new SocialMediaAccount(twitter, foodTruckThree, "foodTruckThree"));
            foodTruckThree.AddSocialMediaAccount(new SocialMediaAccount(instagram, foodTruckThree, "foodTruckThree"));

            foodTruckList = new List<FoodTruck>()
            {
                foodTruckOne, foodTruckTwo, foodTruckThree
            };


            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<FoodTruckAutomapperProfile>();             
            });            
            mapper = new Mapper(config, 
                t => FoodTrucksControllerTests.Resolve<Type, object>(t));

        }

        // Resolver method so AutoMapper will resolve the TestUrlResolver when it goes looking for the UrlResolver
        private static object Resolve<Type, Object>(Type t)
        {
            return new TestUrlResolver();
        }



        private readonly FoodTruck foodTruckOne;
        private readonly FoodTruck foodTruckTwo;
        private readonly FoodTruck foodTruckThree;

        private readonly List<FoodTruck> foodTruckList;

        private readonly IMapper mapper;


        [Fact]
        public void WhenNoParametersPassedToAPI_WeCallMethodToGetAllFoodTrucks()
        {
            // Arrange
            var mockLogger = this.GetMockLogger<FoodTrucksController>();
            
            
            var mockService = new Mock<IFoodTruckService>();
            mockService.Setup(r => r.GetAllFoodTrucks())
                .Returns(foodTruckList);
            FoodTrucksController controller = new FoodTrucksController(mockLogger.Object,  mapper, mockService.Object);

            // Act
            var response = controller.Get();

            // Assert
            mockService.Verify(r => r.GetAllFoodTrucks(), Times.Once());
            mockService.Verify(r => r.GetFoodTrucksByTag(It.IsAny<String>()), Times.Never());
        }





        public Mock<ILogger<T>> GetMockLogger<T>()
        {
            var mockLogger = new Mock<ILogger<T>>();

            return mockLogger;
        }

    }





}
