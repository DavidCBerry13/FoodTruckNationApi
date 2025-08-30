using FoodTruckNation.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruckNation.Core.DataInterfaces
{
    public interface IFoodTruckRepository
    {

        /// <summary>
        /// Gets a list of all of the food trucks in the database
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<FoodTruck>> GetAllFoodTrucksAsync();

        /// <summary>
        /// Gets the food truck with the given id
        /// </summary>
        /// <param name="foodTruckId"></param>
        /// <returns></returns>
        public Task<FoodTruck> GetFoodTruckAsync(int foodTruckId);



        public Task<IEnumerable<FoodTruck>> GetFoodTruckByTagAsync(string tag);


        public Task SaveAsync(FoodTruck foodTruck);


        public Task DeleteAsync(FoodTruck foodTruck);

    }
}
