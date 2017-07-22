using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Domain.Repositories
{
    public interface IFoodTruckRepository
    {

        /// <summary>
        /// Gets a list of all of the food trucks in the database
        /// </summary>
        /// <returns></returns>
        IList<FoodTruck> GetAllFoodTrucks();

        /// <summary>
        /// Gets the food truck with the given id
        /// </summary>
        /// <param name="foodTruckId"></param>
        /// <returns></returns>
        FoodTruck GetFoodTruck(int foodTruckId);



        IList<FoodTruck> GetFoodTruckByTag(String tag);


        void Save(FoodTruck foodTruck);


        void Delete(FoodTruck foodTruck);

    }
}
