//using FoodTruckNation.Core.AppInterfaces;
//using FoodTruckNation.Core.Domain;
//using FoodTruckNationApi.FoodTrucks.Base;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Moq;
//using System;
//using System.Collections.Generic;
//using Xunit;

//namespace FoodTruckNationApi.Test
//{
//    public class FoodTrucksControllerTests
//    {

//        public Mock<ILogger<T>> GetMockLogger<T>()
//        {
//            var mockLogger = new Mock<ILogger<T>>();

//            return mockLogger;
//        }







//        public Mock<ILoggerFactory> GetMockLoggerFactory<T>(Mock<ILogger<T>> mockLogger)
//        {
//            var mockLoggerFactory = new Mock<ILoggerFactory>();

//            // We only have to setup the CreateLogger method that takes a String as the other two
//            // CreateLogger methods are actually extension methods and call the one that takes a 
//            // String under the covers
//            mockLoggerFactory.Setup(lf => lf.CreateLogger(It.IsAny<String>()))
//                .Returns(mockLogger.Object);

//            return mockLoggerFactory;
//        }

//        [Fact]
//        public void WhenNoParametersPassedToAPI_WeCallMethodToGetAllFoodTrucks()
//        {
//            // Arrange
//            var mockLogger = this.GetMockLogger<FoodTrucksController>();
//            var mockLoggerFactory = this.GetMockLoggerFactory<FoodTrucksController>(mockLogger);

//            var mock = new Mock<IFoodTruckService>();
//            mock.Setup(r => r.GetAllFoodTrucks())
//                .Returns(EntityResult<List<FoodTruck>>.Success(FoodTruckData.ALL_FOOD_TRUCKS));
//            FoodTrucksController controller = new FoodTrucksController(mockLoggerFactory.Object, mock.Object);

//            // Act
//            var response = controller.Get();

//            // Assert
//            mock.Verify(r => r.GetAllFoodTrucks(), Times.Once());
//            mock.Verify(r => r.GetFoodTrucksByTag(It.IsAny<String>()), Times.Never());
//        }


//        [Fact]
//        public void TestGetFoodTrucksWithTagParameterProvidedCallsCorrectRepositoryMethod()
//        {
//            // Arrange
//            String tag = TagData.TAG_ONE.Text;

//            var mockLogger = this.GetMockLogger<FoodTrucksController>();
//            var mockLoggerFactory = this.GetMockLoggerFactory<FoodTrucksController>(mockLogger);

//            var mock = new Mock<IFoodTruckService>();
//            mock.Setup(r => r.GetFoodTrucksByTag(tag))
//                .Returns(EntityResult<List<FoodTruck>>.Success(FoodTruckData.FOOD_TRUCKS_WITH_TAG_ONE));
//            FoodTrucksController controller = new FoodTrucksController(mockLoggerFactory.Object, mock.Object);

//            // Act
//            var response = controller.Get(tag);

//            // Assert
//            mock.Verify(r => r.GetFoodTrucksByTag(tag), Times.Once());
//            mock.Verify(r => r.GetAllFoodTrucks(), Times.Never());
//        }



//        [Fact]
//        public void TestGetFoodTruckByIdReturnsFoodTruckData()
//        {
//            // Arrange
//            FoodTruck foodTruckOne = FoodTruckData.FOOD_TRUCK_ONE();
//            int id = foodTruckOne.FoodTruckId;

//            var mockLogger = this.GetMockLogger<FoodTrucksController>();
//            var mockLoggerFactory = this.GetMockLoggerFactory<FoodTrucksController>(mockLogger);

//            var mock = new Mock<IFoodTruckService>();
//            mock.Setup(r => r.GetFoodTruck(id))
//                .Returns(EntityResult<FoodTruck>.Success(foodTruckOne));
//            FoodTrucksController controller = new FoodTrucksController(mockLoggerFactory.Object, mock.Object);

//            // Act
//            var response = controller.Get(id);

//            // Assert            
//            Assert.IsAssignableFrom<ObjectResult>(response);
//            Assert.Equal(StatusCodes.Status200OK, ((ObjectResult)response).StatusCode);

//            FoodTruckModel model = ((FoodTruckModel)((ObjectResult)response).Value);
//            Assert.Equal(foodTruckOne.FoodTruckId, model.FoodTruckId);
//            Assert.Equal(foodTruckOne.Name, model.Name);
//            Assert.Equal(foodTruckOne.Description, model.Description);
//            Assert.Equal(foodTruckOne.Website, model.Website);

//            Assert.Equal(foodTruckOne.Tags.Count, model.Tags.Count);
//            for (int i = 0; i < model.Tags.Count; i++)
//            {
//                String modelTag = model.Tags[i];
//                String expectedTag = foodTruckOne.Tags[i].Tag.Text;
//                Assert.Equal(expectedTag, modelTag);
//            }
//        }




//        [Fact]
//        public void TestPostFoodTrucksReturns201CreatedResponse()
//        {
//            // Arrange
//            CreateFoodTruckModel model = new CreateFoodTruckModel()
//            {
//                Name = "New Food Truck",
//                Description = "New Description",
//                Website = @"http://www.newfoodtruck.com",
//                Tags = new List<string>() { "Tag One", "Tag Two" }
//            };

//            var info = new CreateFoodTruckInfo()
//            {
//                Name = "New Food Truck",
//                Description = "New Description",
//                Website = @"http://www.newfoodtruck.com",
//                Tags = new List<string>() { "Tag One", "Tag Two" }
//            };

//            var foodTruck = new FoodTruck(23, "New Food Truck", "New Description", @"http://www.newfoodtruck.com");


//            var mockLogger = this.GetMockLogger<FoodTrucksController>();
//            var mockLoggerFactory = this.GetMockLoggerFactory<FoodTrucksController>(mockLogger);

//            var mockService = new Mock<IFoodTruckService>();
//            mockService.Setup(s => s.CreateFoodTruck(It.IsAny<CreateFoodTruckInfo>()))
//                .Returns(EntityResult<FoodTruck>.Success(foodTruck));

//            FoodTrucksController controller = new FoodTrucksController(mockLoggerFactory.Object, mockService.Object);



//            // Act
//            var response = controller.Post(model);

//            // Assert
//            Assert.IsAssignableFrom<ObjectResult>(response);
//            Assert.Equal(StatusCodes.Status201Created, ((ObjectResult)response).StatusCode);
//        }


//    }
//}
