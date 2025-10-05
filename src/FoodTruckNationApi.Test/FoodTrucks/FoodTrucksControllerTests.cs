using AutoMapper;
using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.Domain;
using FoodTruckNationApi.FoodTrucks;
using DavidBerry.Framework.Functional;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Test.FoodTrucks
{
    public class FoodTrucksControllerTests
    {
        public FoodTrucksControllerTests()
        {
            Locality locality = new Locality() { LocalityCode = "CHI", Name = "Chicago" };

            Tag tagOne = new Tag(1, "Tag One");
            Tag tagTwo = new Tag(2, "Tag Two");
            Tag tagThree = new Tag(3, "Tag Three");
            Tag tagFour = new Tag(4, "Tag Four");

            SocialMediaPlatform facebook = new SocialMediaPlatform(1, "Facebook", "http://facebook.com/{0}", null);
            SocialMediaPlatform twitter = new SocialMediaPlatform(2, "Twitter", "http://twitter.com/{0}", null);
            SocialMediaPlatform instagram = new SocialMediaPlatform(3, "Instagram", "http://instagram.com/{0}", null);

            _foodTruckOne = new FoodTruck(1, "Food Truck One", "Food Truck One Description", @"http://www.foodtruckone.com", locality);
            _foodTruckOne.AddTag(tagOne);
            _foodTruckOne.AddTag(tagTwo);
            _foodTruckOne.AddSocialMediaAccount(new SocialMediaAccount(facebook, _foodTruckOne, "foodTruckOne"));
            _foodTruckOne.AddSocialMediaAccount(new SocialMediaAccount(twitter, _foodTruckOne, "foodTruckOne"));

            _foodTruckTwo = new FoodTruck(2, "Food Truck Two", "Food Truck Two Description", @"http://www.foodtrucktwo.com", locality);
            _foodTruckTwo.AddTag(tagThree);
            _foodTruckOne.AddSocialMediaAccount(new SocialMediaAccount(facebook, _foodTruckTwo, "foodTruckTwo"));

            _foodTruckThree = new FoodTruck(3, "Food Truck Two", "Food Truck Three Description", @"http://www.foodtruckthree.com", locality);
            _foodTruckThree.AddTag(tagOne);
            _foodTruckThree.AddTag(tagFour);
            _foodTruckThree.AddSocialMediaAccount(new SocialMediaAccount(facebook, _foodTruckThree, "foodTruckThree"));
            _foodTruckThree.AddSocialMediaAccount(new SocialMediaAccount(twitter, _foodTruckThree, "foodTruckThree"));
            _foodTruckThree.AddSocialMediaAccount(new SocialMediaAccount(instagram, _foodTruckThree, "foodTruckThree"));

            _foodTruckList = new List<FoodTruck>()
            {
                _foodTruckOne, _foodTruckTwo, _foodTruckThree
            };


            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<FoodTruckModelAutomapperProfile>();
            });
            _mapper = new Mapper(config,
                t => FoodTrucksControllerTests.Resolve<Type, object>(t));

        }

        // Resolver method so AutoMapper will resolve the TestUrlResolver when it goes looking for the UrlResolver
        private static object Resolve<TType, TObject>(Type t)
        {
            return new TestUrlResolver();
        }



        private readonly FoodTruck _foodTruckOne;
        private readonly FoodTruck _foodTruckTwo;
        private readonly FoodTruck _foodTruckThree;

        private readonly List<FoodTruck> _foodTruckList;

        private readonly IMapper _mapper;


        [Fact]
        public void WhenNoParametersPassedToAPI_WeCallMethodToGetAllFoodTrucks()
        {
            // Arrange
            var mockLogger = GetMockLogger<FoodTrucksController>();


            var mockService = new Mock<IFoodTruckService>();
            mockService.Setup(r => r.GetAllFoodTrucksAsync())
                .Returns(Task.FromResult(Result.Success<IEnumerable<FoodTruck>>(_foodTruckList)));
            FoodTrucksController controller = new FoodTrucksController(mockLogger.Object,  _mapper, mockService.Object);

            // Act
            var response = controller.Get();

            // Assert
            mockService.Verify(r => r.GetAllFoodTrucksAsync(), Times.Once());
            mockService.Verify(r => r.GetFoodTrucksAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        }


        [Fact]
        public void WhenTagParameterPassedToAPI_SearchIsDoneByTag()
        {
            // Arrange
            var mockLogger = GetMockLogger<FoodTrucksController>();
            var searchTag = "Tacos";

            var mockService = new Mock<IFoodTruckService>();
            mockService.Setup(r => r.GetFoodTrucksAsync(searchTag, null))
                .Returns(Task.FromResult(Result.Success<IEnumerable<FoodTruck>>(_foodTruckList.Where(f => f.Tags.Any(t => t.Tag.Text == searchTag)).ToList())));
            FoodTrucksController controller = new FoodTrucksController(mockLogger.Object, _mapper, mockService.Object);

            // Act
            var response = controller.Get(searchTag);

            // Assert
            mockService.Verify(r => r.GetAllFoodTrucksAsync(), Times.Never());
            mockService.Verify(r => r.GetFoodTrucksAsync(searchTag, null), Times.Once());
        }




        public Mock<ILogger<T>> GetMockLogger<T>()
        {
            var mockLogger = new Mock<ILogger<T>>();

            return mockLogger;
        }

    }





}
