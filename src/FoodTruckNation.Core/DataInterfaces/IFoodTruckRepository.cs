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

        /// <summary>
        /// Gets all the food trucks for the given locality
        /// </summary>
        /// <param name="locality">A Locality object of the locality to get food trucks for</param>
        /// <returns>An IEnumerable of FoodTruck objects that operate in this locality</returns>
        public Task<IEnumerable<FoodTruck>> GetFoodTrucksAsync(Locality locality);

        /// <summary>
        /// Gets all the food trucks with the given tag
        /// </summary>
        /// <param name="tag">A string of the tag used to filter food trucks with</param>
        /// <returns>An IEnumerable of FoodTruck objects with the provided tag</returns>
        public Task<IEnumerable<FoodTruck>> GetFoodTrucksAsync(string tag);

        /// <summary>
        /// Gets all the food trucks for the given locality with the given tag
        /// </summary>
        /// <param name="locality">A Locality object of the locality to get food trucks for</param>
        /// <param name="tag">A string of the tag used to filter food trucks with</param>
        /// <returns>An IEnumerable of FoodTruck objects in the given locality with the provided tag</returns>
        public Task<IEnumerable<FoodTruck>> GetFoodTrucksAsync(Locality locality, string tag);


        public Task SaveAsync(FoodTruck foodTruck);


        public Task DeleteAsync(FoodTruck foodTruck);

    }
}
