//using System;
//using Xunit;
//using FoodTruckNation.Domain;
//using Newtonsoft.Json;
//using System.Collections.Generic;
//using Xunit.Abstractions;
//using Framework;

//namespace FoodTruckNation.Domain.Test
//{
//    public class FoodTruckTests
//    {

//        //private readonly ITestOutputHelper output;

//        //public FoodTruckTests(ITestOutputHelper output)
//        //{
//        //    this.output = output;
//        //}


//        public static readonly Tag TAG_ONE = new Tag(1, "Tag One");

//        public static readonly Tag TAG_TWO = new Tag(2, "Tag Two");



//        [Fact]
//        public void TestObjectStateIsNewForNewObjectConstructor()
//        {
//            // Arrange / Act
//            FoodTruck foodTruck = new FoodTruck("New Food Truck", "Some Description", @"http://some.website.com/");

//            // Assert
//            Assert.Equal(ObjectState.NEW, foodTruck.ObjectState);
//        }


//        [Fact]
//        public void TestObjectStateIsUnchangedForExistingObjectConstructor()
//        {
//            // Arrange / Act
//            var foodTruck = new FoodTruck(100, "Food Truck One", "Food Truck One Description", "http://foodtruckone.com");
//            foodTruck.Tags = new List<FoodTruckTag>()
//            {
//                    new FoodTruckTag(1, foodTruck, TAG_ONE),
//                    new FoodTruckTag(2, foodTruck, TAG_TWO)
//            };

//            // Assert
//            Assert.Equal(ObjectState.UNCHANGED, foodTruck.ObjectState);
//        }


//        [Fact]
//        public void TestObjectStateChangesToModifiedWhenNameChanged()
//        {
//            // Arrange
//            var foodTruck = new FoodTruck(100, "Food Truck One", "Food Truck One Description", "http://foodtruckone.com");
//            foodTruck.Tags = new List<FoodTruckTag>()
//            {
//                    new FoodTruckTag(1, foodTruck, TAG_ONE),
//                    new FoodTruckTag(2, foodTruck, TAG_TWO)
//            };

//            // Act
//            foodTruck.Name = "New Name";

//            // Assert
//            Assert.Equal(ObjectState.MODIFIED, foodTruck.ObjectState);
//        }


//        [Fact]
//        public void TestObjectStateChangesToModifiedWhenDescriptionChanged()
//        {
//            // Arrange
//            var foodTruck = new FoodTruck(100, "Food Truck One", "Food Truck One Description", "http://foodtruckone.com");
//            foodTruck.Tags = new List<FoodTruckTag>()
//            {
//                    new FoodTruckTag(1, foodTruck, TAG_ONE),
//                    new FoodTruckTag(2, foodTruck, TAG_TWO)
//            };

//            // Act
//            foodTruck.Description = "New Description";

//            // Assert
//            Assert.Equal(ObjectState.MODIFIED, foodTruck.ObjectState);
//        }


//        [Fact]
//        public void TestObjectStateChangesToModifiedWhenWebsiteChanged()
//        {
//            // Arrange
//            var foodTruck = new FoodTruck(100, "Food Truck One", "Food Truck One Description", "http://foodtruckone.com");
//            foodTruck.Tags = new List<FoodTruckTag>()
//            {
//                    new FoodTruckTag(1, foodTruck, TAG_ONE),
//                    new FoodTruckTag(2, foodTruck, TAG_TWO)
//            };

//            // Act
//            foodTruck.Website = @"http://www.google.com";

//            // Assert
//            Assert.Equal(ObjectState.MODIFIED, foodTruck.ObjectState);
//        }


//        [Fact]
//        public void TestObjectStateRemainsNewForNewObjectAfterNameChanges()
//        {
//            // Arrange
//            FoodTruck foodTruck = new FoodTruck("New Food Truck", "Some Description", @"http://some.website.com/");

//            // Act
//            foodTruck.Name = "New Name";

//            // Assert
//            Assert.Equal(ObjectState.NEW, foodTruck.ObjectState);
//        }


//        [Fact]
//        public void TestObjectStateRemainsNewForNewObjectAfterDescriptionChanges()
//        {
//            // Arrange
//            FoodTruck foodTruck = new FoodTruck("New Food Truck", "Some Description", @"http://some.website.com/");

//            // Act
//            foodTruck.Description = "New Description";

//            // Assert
//            Assert.Equal(ObjectState.NEW, foodTruck.ObjectState);
//        }


//        [Fact]
//        public void TestObjectStateRemainsNewForNewObjectAfterWebsiteChanges()
//        {
//            // Arrange
//            FoodTruck foodTruck = new FoodTruck("New Food Truck", "Some Description", @"http://some.website.com/");

//            // Act
//            foodTruck.Website = @"http://google.com";

//            // Assert
//            Assert.Equal(ObjectState.NEW, foodTruck.ObjectState);
//        }
//    }
//}
