using System;
using System.Collections.Generic;
using System.Text;
using FoodTruckNation.Domain;
using FoodTruckNation.AppServices.Framework;
using FoodTruckNation.AppServices.Models;

namespace FoodTruckNation.AppServices.Interfaces
{

    /// <summary>
    /// Interface defining the FoodTruckService, mainly so the service can be mocked
    /// </summary>
    public interface IFoodTruckService
    {

        EntityResult<List<FoodTruck>> GetAllFoodTrucks();

        EntityResult<List<FoodTruck>> GetFoodTrucksByTag(String tag);

        EntityResult<FoodTruck> GetFoodTruck(int id);

        EntityResult<FoodTruck> CreateFoodTruck(CreateFoodTruckInfo foodTruckInfo);

        EntityResult<FoodTruck> UpdateFoodTruck(UpdateFoodTruckInfo foodTruckInfo);

        Result DeleteFoodTruck(int foodTruckId);
    }
}
