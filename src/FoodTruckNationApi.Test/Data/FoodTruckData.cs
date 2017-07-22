//using FoodTruckNation.Domain;
//using System;
//using System.Linq;
//using System.Collections.Generic;
//using System.Text;

//namespace FoodTruckNationApi.Test.Data
//{
//    internal static class FoodTruckData
//    {

//        public static Func<FoodTruck> FOOD_TRUCK_ONE = () =>
//        {

//            var foodTruck = new FoodTruck(100, "Food Truck One", "Food Truck One Description", "http://foodtruckone.com");

//            foodTruck.Tags = new List<FoodTruckTag>() { new FoodTruckTag(1, foodTruck, TagData.TAG_ONE) };
//            return foodTruck;

//        };


//        public static Func<FoodTruck> FOOD_TRUCK_TWO = () =>
//        {

//            var foodTruck = new FoodTruck(100, "Food Truck Two", "Food Truck Two Description", "http://foodtrucktwo.com");

//            foodTruck.Tags = new List<FoodTruckTag>()
//            {
//                new FoodTruckTag(2, foodTruck, TagData.TAG_ONE),
//                new FoodTruckTag(3, foodTruck, TagData.TAG_TWO),
//            };
//            return foodTruck;

//        };


//        public static Func<FoodTruck> FOOD_TRUCK_THREE = () =>
//        {

//            var foodTruck = new FoodTruck(100, "Food Truck Three", "Food Truck Three Description", "http://foodtruckthree.com");

//            foodTruck.Tags = new List<FoodTruckTag>()
//            {
//                new FoodTruckTag(4, foodTruck, TagData.TAG_THREE),
//                new FoodTruckTag(3, foodTruck, TagData.TAG_TWO),
//            };
//            return foodTruck;

//        };





//        public static readonly List<FoodTruck> ALL_FOOD_TRUCKS = new List<FoodTruck>()
//            {
//                FOOD_TRUCK_ONE(),
//                FOOD_TRUCK_TWO(),
//                FOOD_TRUCK_THREE()
//            };

//        public static readonly List<FoodTruck> FOOD_TRUCKS_WITH_TAG_ONE = new List<FoodTruck>()
//            {
//                FOOD_TRUCK_ONE(),
//                FOOD_TRUCK_TWO()
//            };

//    }
//}
